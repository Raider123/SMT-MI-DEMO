using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand_Transform_Player : MonoBehaviour
{
    public GameObject spawner_obj;
    public GameObject start_countdown;
    public GameObject wrist; // Specifies the "Wrist" GameObject
    public string fileName;

    private string folderName = "Hand_Transform_Runtime_Data";
    private TransformData[] recordedData;
    private Dictionary<string, Transform> nameToTransformMap;
    private int currentIndex = 0;

    private Vector3 offset_vector;

    void Start()
    {
        //Determine the offset coefficients (determined by observation and comparison of the XR Rig and the L_Wrist and R_Wrist.TXT File)
        int level = PlayerPrefs.GetInt("Level");
        if (level == 2)
        {
            offset_vector = new Vector3(0.5f - 2f, 0.374f, 0.12f);
        }
        else
        {
            offset_vector = new Vector3(0.5f, 0.374f, 0.12f);
        }

        if (wrist == null)
        {
            Debug.LogError("Wrist GameObject is not assigned.");
            return;
        }

        nameToTransformMap = new Dictionary<string, Transform>();
        CacheTransforms(wrist.transform);

        LoadData();

        if (recordedData.Length > 0)
        {
            StartCoroutine(PlayTransformData());
        }
        else
        {
            Debug.LogError("No data loaded.");
        }
    }

    void CacheTransforms(Transform parent)
    {
        var stack = new Stack<Transform>();
        stack.Push(parent);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            nameToTransformMap[current.name] = current;

            foreach (Transform child in current)
            {
                stack.Push(child);
            }
        }
    }

    void LoadData()
    {
        string path = Path.Combine(folderName, fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"File not found: {path}");
            return;
        }

        var lines = File.ReadAllLines(path);
        var tempData = new List<TransformData>(lines.Length);
        var warnings = new List<string>();

        foreach (var line in lines)
        {
            string[] parts = line.Split(';');

            if (parts.Length == 8)
            {
                string name = parts[0];
                if (float.TryParse(parts[1], out float posX) && float.TryParse(parts[2], out float posY) &&
                    float.TryParse(parts[3], out float posZ) && float.TryParse(parts[4], out float rotX) &&
                    float.TryParse(parts[5], out float rotY) && float.TryParse(parts[6], out float rotZ) &&
                    float.TryParse(parts[7], out float rotW))
                {
                    Vector3 position = new Vector3(posX, posY, posZ);
                    Quaternion rotation = new Quaternion(rotX, rotY, rotZ, rotW);
                    tempData.Add(new TransformData(name, position, rotation));
                }
                else
                {
                    warnings.Add($"Incorrect data format: {line}");
                }
            }
            else
            {
                warnings.Add($"Incorrect data format: {line}");
            }
        }

        if (warnings.Count > 0)
        {
            Debug.LogWarning(string.Join("\n", warnings));
        }

        recordedData = tempData.ToArray();
    }

    IEnumerator PlayTransformData()
    {
        while (currentIndex < recordedData.Length)
        {
            for (int i = 0; i < 10 && currentIndex < recordedData.Length; i++)
            {
                ApplyTransformData(recordedData[currentIndex]);
                currentIndex++;
            }
            yield return null;
        }

        float classification = Random.value * 100;
        Debug.Log("Classification: " + classification);

        spawner_obj.GetComponent<BulletShooter>().Mi_shoot(classification);
        // Starts the countdown in the MI-case
        start_countdown.GetComponent<Start_Trial_Timer>().StartCountdown();
    }

    void ApplyTransformData(TransformData data)
    {
        if (nameToTransformMap.TryGetValue(data.Name, out Transform targetTransform))
        {
            targetTransform.position = data.Position - offset_vector;
            targetTransform.rotation = data.Rotation;
            
        }
        else
        {
            //Debug.LogWarning($"Transform not found: {data.Name}");
        }
    }

    private struct TransformData
    {
        public string Name { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public TransformData(string name, Vector3 position, Quaternion rotation)
        {
            Name = name;
            Position = position;
            Rotation = rotation;
        }
    }
}

