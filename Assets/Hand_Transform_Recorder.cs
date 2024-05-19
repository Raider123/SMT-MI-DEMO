using UnityEngine;
using System.IO;
using System.Text;

public class Hand_Transform_Recorder : MonoBehaviour
{
    public GameObject handobject;
    private string folderName = "Hand_Transform_Runtime_Data";
    private StringBuilder data;

    void Start()
    {
        data = new StringBuilder();

        // Ensure the directory exists
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }
    }

    void Update()
    {
        // Record the position and rotation of the handobject and its children
        RecordTransform(handobject.transform);
    }

    void RecordTransform(Transform objTransform)
    {
        // Record the position and rotation of the current transform
        Vector3 position = objTransform.position;
        Quaternion rotation = objTransform.rotation;
        string line = $"{objTransform.name};{position.x};{position.y};{position.z};{rotation.x};{rotation.y};{rotation.z};{rotation.w}";
        data.AppendLine(line);

        // Recursively record the position and rotation of all child transforms
        foreach (Transform child in objTransform)
        {
            RecordTransform(child);
        }
    }

    void OnDisable()
    {
        // Write the recorded data to the file when the application quits
        string file1 = Path.Combine(folderName, "NEW " + handobject.name + $"{System.DateTime.Now:yyyyMMdd_HHmmss}.txt");

        File.WriteAllText(file1, data.ToString());
    }
}
