using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class TransformPlayer : MonoBehaviour
{
    public string fileName = "TransformData.txt";
    private List<TransformData> recordedData;
    private int currentIndex = 0;

    void Start()
    {
        recordedData = new List<TransformData>();
        LoadData();
        StartCoroutine(PlayTransformData());
    }

    void LoadData()
    {
        string[] lines = File.ReadAllLines(fileName);

        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

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
