using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class TransformPlayer : MonoBehaviour
{
    public GameObject spawner_obj; // Specifies the spawner_object to shoot the bullet
    public GameObject hand_gesture; // Specifies the gesture to be deactivated after the first (possible) shot
    public GameObject start_countdown;

    public string fileName = "L_Wrist20240518_164009.txt"; // Specify the exact file name here or use a file dialog to select
    private string folderName = "Hand_Transform_Runtime_Data";
    
    private List<TransformData> recordedData;
    private int currentIndex = 0;

    void Start()
    {
        recordedData = new List<TransformData>();
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

    void LoadData()
    {
        // Combine the folder name and file name to get the full path
        string path = Path.Combine(folderName, fileName);

        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.LogError($"File not found: {path}");
            return;
        }

        // Read all lines from the file
        string[] lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            string[] parts = line.Split(';');

            if (parts.Length == 7)
            {
                Vector3 position = new Vector3(
                    float.Parse(parts[0]),
                    float.Parse(parts[1]),
                    float.Parse(parts[2])
                );

                Quaternion rotation = new Quaternion(
                    float.Parse(parts[3]),
                    float.Parse(parts[4]),
                    float.Parse(parts[5]),
                    float.Parse(parts[6])
                );

                recordedData.Add(new TransformData(position, rotation));
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
            transform.position = recordedData[currentIndex].Position;
            transform.rotation = recordedData[currentIndex].Rotation;
            currentIndex++;
            yield return null; // Wait for the next frame
        }

        // Transformationsbewegung abgeschlossen, jetzt werden die Dinge ausgeführt, die normalerweise von der Gestensteuerung übernommen werden
        spawner_obj.GetComponent<BulletShooter>().ShootBullet();
        hand_gesture.SetActive(false);
        start_countdown.GetComponent<Start_Trial_Timer>().StartCountdown();
    }

    private struct TransformData
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public TransformData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}

