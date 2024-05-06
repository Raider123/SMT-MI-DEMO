using UnityEngine;

public class CollVis : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob die Kollision mit Objekt A stattfindet
        if (collision.gameObject.CompareTag("UNFILLED"))
        {
            // Deaktiviere Objekt B
            gameObject.SetActive(false);
            // Deaktiviere auch Objekt A
            collision.gameObject.SetActive(false);
        }
    }
}
