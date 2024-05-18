using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor.Recorder;

public class TransformRecorder : MonoBehaviour
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
        // Record the position and rotation in each frame
        Vector3 position = handobject.transform.position;
        Quaternion rotation = handobject.transform.rotation;

        // Format the data as a string
        string line = $"{position.x};{position.y};{position.z};{rotation.x};{rotation.y};{rotation.z};{rotation.w}";
        data.AppendLine(line);
    }

    void OnDestroy()
    {
        // Write the recorded data to the file when the application quits
        string file1 = Path.Combine(folderName, handobject.name + $"{System.DateTime.Now:yyyyMMdd_HHmmss}.txt");

        File.WriteAllText(file1, data.ToString());
    }
}
