using UnityEngine;

public class AveragePosition : MonoBehaviour
{
    public Transform[] targetTransforms; // Array, das die 5 Ziel-Transforms enthält

    void Update()
    {
        // Überprüfe, ob mindestens ein Transform im Array vorhanden ist
        if (targetTransforms.Length > 0)
        {
            Vector3 averagePosition = Vector3.zero;
            Quaternion averageRotation = Quaternion.identity;

            // Berechne den Mittelwert der Positionen und Rotationen der Ziel-Transforms
            foreach (Transform targetTransform in targetTransforms)
            {
                averagePosition += targetTransform.position;
                averageRotation *= targetTransform.rotation;
            }

            averagePosition /= targetTransforms.Length;
            averageRotation = Quaternion.SlerpUnclamped(Quaternion.identity, averageRotation, 1f / targetTransforms.Length);

            // Setze die Position und Rotation des aktuellen GameObjects auf den Mittelwert
            transform.position = averagePosition;
            transform.rotation = averageRotation;
        }
        else
        {
            Debug.LogError("No target transforms assigned to AveragePosition script.");
        }
    }
}
