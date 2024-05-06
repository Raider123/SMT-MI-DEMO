using UnityEngine;

public class RandomArrowPose : MonoBehaviour
{
    public Transform position1; // Erste vordefinierte Position
    public Transform position2; // Zweite vordefinierte Position

    void Start()
    {
        // Zufällig eine der beiden vordefinierten Positionen auswählen
        if (Random.value < 0.5f)
        {
            transform.position = position1.position;
            transform.rotation = position1.rotation;
        }
        else
        {
            transform.position = position2.position;
            transform.rotation = position2.rotation;
        }
    }
}
