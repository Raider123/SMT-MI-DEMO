using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallCollideTarget : MonoBehaviour
{

    [SerializeField] private AudioSource low_point_audio; // Audioquelle 1 bei einem niedrigen Ergebnis

    [SerializeField] private AudioSource high_point_audio; // Audioquelle 2 bei einem hohen Ergebnis

    [SerializeField] private AudioSource max_point_audio; // Audioquelle 3 beim höchsten Ergebnis

    [SerializeField] private GameObject selfreference; // Zugabe des Tags des Zielscheibenobjekts (left hand or right hand MI)

    [SerializeField] private GameObject[] objectsToReplace; // Array der vorhandenen GameObjekte (zum Schrumpfen/Löschen)

    [SerializeField] private float shrink_duration = 0.5f; // Schrumpfen - Animationsdauer

    [SerializeField] private XRSimpleInteractable eye_gaze_interactable; // Instanz des Eye Gaze Interactable Objekts, um die Blickrichtung zu erfassen

    [SerializeField] private GameObject countDown; // Instanz eines Timerskripts zum Ansprechen der StartCountdown() Funktion nach erfolgter Kollision

    [SerializeField] private GameObject gesture_detection; // Instanz eines Gesture-Detection Objekts, um die Schussmöglichkeiten nach der Kollision einzuschränken

    [SerializeField] private GameObject Point_display; // Nach dem erfolgreichen Abschuss einer Zielscheibe direkt die Punktzahl anzeigen

    static float achievement_score; // Zählt den aktuellen Punktestand auf globaler Weise


    private void OnCollisionEnter(Collision collision)
    {
        // Compare the tag of both collision partners
        // If the tag is left_hand_mi, an event is only triggered if both collision objects share the same tag and there is a "Hovered" Event
        if ((selfreference.tag == collision.gameObject.tag) && eye_gaze_interactable.isHovered)
        {
            // Deaktiviere weitere Schussmöglichkeiten
            gesture_detection.gameObject.SetActive(false);

            // Kalkuliere die Punktzahl basierend auf der Entfernung vom Mittelpunkt (x-Koordinate)
            float game_points = calculatePoints(selfreference, collision.gameObject);

            // Erhöhe den erreichten Punktestand
            achievement_score += game_points;
            PlayerPrefs.SetFloat("achievement_score", achievement_score);

            // Speichere die Szenenübergreifenden Variablen
            PlayerPrefs.Save();

            // Play Collision Sound depending on the achieved points within the game
            if (game_points < 7)
            {
                low_point_audio.Play();

            }
            else if(game_points == 8)
            {
                high_point_audio.Play();
            }
            else
            {
                max_point_audio.Play();
            }
    
            // Shrink the target object upon collision
            StartCoroutine(ShrinkOverTime(selfreference));
            // Display the reached points immediately with a growing animation
            GameObject new_point_display = Instantiate(Point_display, selfreference.transform.position, Point_display.transform.rotation);
            new_point_display.GetComponent<TextMeshPro>().text = game_points.ToString();
            StartCoroutine(GrowOverTime(new_point_display));

            //Shrinking all the objects upon collision
            for (int i = 0; i < objectsToReplace.Length; i++)
            {
                StartCoroutine(ShrinkOverTime(objectsToReplace[i]));
            }
           
            // Starte den Countdown Timer zum Laden der nächsten Szene
            countDown.GetComponent<Start_Trial_Timer>().StartCountdown();

            // Destroy the object, if needed
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ShrinkOverTime(GameObject selfreference)
    {
        // Startskalierung des Objekts
        Vector3 startScale = selfreference.transform.localScale;

        // Ziel-Skalierung des Objekts (z. B. unsichtbar klein)
        Vector3 targetScale = Vector3.zero;

        // Zeit, die seit Beginn der Animation vergangen ist
        float elapsedTime = 0.0f;

        while (elapsedTime < shrink_duration)
        {
            // Lerp (lineare Interpolation) zwischen Start- und Ziel-Skalierung basierend auf der aktuellen Zeit
            selfreference.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / shrink_duration);

            // Aktualisiere die vergangene Zeit
            elapsedTime += Time.deltaTime;

            // Warte eine Frame, bevor die nächste Aktualisierung durchgeführt wird
            yield return null;
        }

        // Stelle sicher, dass die Skalierung am Ende genau auf das Ziel gesetzt wird
        selfreference.transform.localScale = targetScale;
    }

    IEnumerator GrowOverTime(GameObject selfreference)
    {
        float grow_duration = 1.0f;
        // Startskalierung des Objekts
        Vector3 startScale = Vector3.zero;

        // Ziel-Skalierung des Objekts (z. B. unsichtbar klein)
        Vector3 targetScale = selfreference.transform.localScale;

        // Zeit, die seit Beginn der Animation vergangen ist
        float elapsedTime = 0.0f;

        while (elapsedTime < grow_duration)
        {
            // Lerp (lineare Interpolation) zwischen Start- und Ziel-Skalierung basierend auf der aktuellen Zeit
            selfreference.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / grow_duration);

            // Aktualisiere die vergangene Zeit
            elapsedTime += Time.deltaTime;

            // Warte eine Frame, bevor die nächste Aktualisierung durchgeführt wird
            yield return null;
        }

        // Stelle sicher, dass die Skalierung am Ende genau auf das Ziel gesetzt wird
        selfreference.transform.localScale = targetScale;
    }

    private float calculatePoints(GameObject object1, GameObject object2)
    {
        Vector3 colliderSize = object1.GetComponent<MeshCollider>().bounds.size;
        float radius = colliderSize.y * 0.5f; // Found through an empirical process

        // Berechne den y-Abstand
        float yDistance = Mathf.Abs(object1.transform.position.y - object2.transform.position.y);

        // Berechne den z-Abstand
        float zDistance = Mathf.Abs(object1.transform.position.z - object2.transform.position.z);

        // Berechne den Gesamtabstand zu Radiusverhältnis
        float accuracy = Mathf.Abs(radius - Mathf.Sqrt(Mathf.Pow(yDistance, 2) + Mathf.Pow(zDistance, 2)));

        float round_acc = 100 - (float)System.Math.Round(accuracy, 4) * 100;

        if (round_acc > 95)
        {
            return 4f;

        }else if(round_acc <= 95 && round_acc > 90)
        {
            return 6f;

        }else if(round_acc <= 90 && round_acc > 85)
        {
            return 8f;

        }else 
        {
            return 10f;
        }
    }
}
