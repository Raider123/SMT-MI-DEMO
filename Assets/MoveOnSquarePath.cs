using UnityEngine;

public class MoveOnSquarePath : MonoBehaviour
{
    public Transform[] waypoints; // Die Eckpunkte des viereckigen Pfads
    public float speed = 2f; // Geschwindigkeit der Bewegung
    private int currentWaypointIndex = 0; // Index des aktuellen Eckpunkts

    void Update()
    {
        // Überprüfen, ob es mindestens einen Wegpunkt gibt
        if (waypoints.Length == 0)
        {
            Debug.LogError("Keine Wegpunkte definiert!");
            return;
        }

        // Bewegung zu dem aktuellen Wegpunkt
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Wenn das Objekt den aktuellen Wegpunkt erreicht hat, zum nächsten wechseln
        if (transform.position == targetPosition)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
