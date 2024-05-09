using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform raycastInteractor; // Der Raycast-Interaktor, der den Pfad definiert
    public float speed = 5f; // Geschwindigkeit des GameObjects

    private void Update()
    {
        // Erzeuge einen Raycast vom Raycast-Interaktor aus
        RaycastHit hit;
        if (Physics.Raycast(raycastInteractor.position, raycastInteractor.forward, out hit))
        {
            // Bewege das GameObject in Richtung des getroffenen Punktes auf dem Pfad
            Vector3 targetPosition = hit.point;
            Vector3 moveDirection = targetPosition - transform.position;
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
        }
    }
}
