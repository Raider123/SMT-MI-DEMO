
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordAnimation : MonoBehaviour
{
    public float recordInterval = 0.1f; // Zeitintervall für die Aufnahme
    private List<KeyframeData> keyframes = new List<KeyframeData>();
    private bool isRecording = false;
    private float timer = 0f;

    [System.Serializable]
    public class KeyframeData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;
    }

    void Start()
    {
        StartRecording(); // Aufnahme automatisch starten
    }

    void Update()
    {
        if (isRecording)
        {
            timer += Time.deltaTime;

            if (timer >= recordInterval)
            {
                keyframes.Add(new KeyframeData
                {
                    position = transform.position,
                    rotation = transform.rotation,
                    time = Time.time
                });

                timer = 0f;
            }
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        keyframes.Clear();
    }

    public void OnDisable()
    {
        isRecording = false;
        SaveAnimation();
    }

    private void SaveAnimation()
    {
        string folderPath = "Hand_Transform_Runtime_Data";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Verzeichnis erstellt: " + folderPath);
        }

        string filePath = Path.Combine(folderPath, "animation.json");
        string json = JsonUtility.ToJson(new KeyframeList { keyframes = this.keyframes });

        File.WriteAllText(filePath, json);

        Debug.Log("Animation aufgenommen und gespeichert unter: " + filePath);
    }

    [System.Serializable]
    public class KeyframeList
    {
        public List<KeyframeData> keyframes;
    }
}
