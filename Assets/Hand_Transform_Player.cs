
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Hand_Transform_Player : MonoBehaviour
{
    //public GameObject spawner_obj;
    //public GameObject start_countdown;
    public GameObject wrist; // Specifies the "Wrist" GameObject
    public string fileName ;

    private string folderName = "Hand_Transform_Runtime_Data";
    private List<TransformData> recordedData;
    private Dictionary<string, Transform> nameToTransformMap;
    private int currentIndex = 0;

    void Start()
    {
        if (wrist == null)
        {
            Debug.LogError("Wrist GameObject is not assigned.");
            return;
        }

        recordedData = new List<TransformData>();
        nameToTransformMap = new Dictionary<string, Transform>();

        CacheTransforms(wrist.transform);
        LoadData();

        if (recordedData.Count > 0)
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
        // Cache the parent itself
        nameToTransformMap[parent.name] = parent;

        // Cache all children recursively
        foreach (Transform child in parent)
        {
            nameToTransformMap[child.name] = child;
            CacheTransforms(child);
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

        string[] lines = File.ReadAllLines(path);

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
                    recordedData.Add(new TransformData(name, position, rotation));
                }
                else
                {
                    Debug.LogWarning($"Incorrect data format: {line}");
                }
            }
            else
            {
                Debug.LogWarning($"Incorrect data format: {line}");
            }
        }
    }

    IEnumerator PlayTransformData()
    {
        while (currentIndex < recordedData.Count)
        {
            ApplyTransformData(recordedData[currentIndex]);
            currentIndex++;
            yield return null;
        }

        //spawner_obj.GetComponent<BulletShooter>().ShootBullet();
        //start_countdown.GetComponent<Start_Trial_Timer>().StartCountdown();
    }

    void ApplyTransformData(TransformData data)
    {
        if (nameToTransformMap.TryGetValue(data.Name, out Transform targetTransform))
        {
            targetTransform.position = data.Position;
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