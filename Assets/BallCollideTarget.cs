using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BallCollideTarget : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource; // Zugabe des Audioobjekts des Zielscheibenobjekts (andere Audio je Zielscheibe möglich)

    [SerializeField] private GameObject selfreference; // Zugabe des Tags des Zielscheibenobjekts (left hand or right hand MI)

    [SerializeField] private GameObject[] objectsToReplace; // Array der vorhandenen GameObjekte (zum Schrumpfen/Löschen)

    [SerializeField] private float shrink_duration = 0.5f; // Schrumpfen - Animationsdauer

    [SerializeField] private XRSimpleInteractable eye_gaze_interactable; // Instanz des Eye Gaze Interactable Objekts, um die Blickrichtung zu erfassen

    [SerializeField] private GameObject countDown; // Instanz eines Timerskripts zum Ansprechen der StartCountdown() Funktion nach erfolgter Kollision

    [SerializeField] private GameObject gesture_detection; // Instanz eines Gesture-Detection Objekts, um die Schussmöglichkeiten nach der Kollision einzuschränken

    static int achievement_score; // Zählt den aktuellen Punktestand auf globaler Weise
    static float total_accuracy; // Berechnet die Genauigkeit der Schussversuche

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        eye_gaze_interactable = GetComponent<XRSimpleInteractable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Compare the tag of both collision partners
        // If the tag is left_hand_mi, an event is only triggered if both collision objects share the same tag and there is a "Hovered" Event
        if ((selfreference.tag == collision.gameObject.tag) && eye_gaze_interactable.isHovered)
        {
            // Deaktiviere weitere Schussmöglichkeiten
            gesture_detection.gameObject.SetActive(false);

            // Kalkuliere die Punktzahl basierend auf der Entfernung vom Mittelpunkt (x-Koordinate)
            float accuracy = calculateAccuracy(selfreference, collision.gameObject);

            // Play Collision Sound
            audioSource.Play();

            // Destroy the object, if needed
            Destroy(collision.gameObject);

            // Shrink all the objects upon collision
            StartCoroutine(ShrinkOverTime(selfreference));
            for (int i = 0; i < objectsToReplace.Length; i++)
            {
                StartCoroutine(ShrinkOverTime(objectsToReplace[i]));
            }

            // Erhöhe den erreichten Punktestand
            achievement_score += 1;
            PlayerPrefs.SetInt("achievement_score", achievement_score);
            // Stelle die aktuelle Genauigkeit dar
            PlayerPrefs.SetFloat("accuracy", accuracy);
            // Berechne fortlaufend die gesamte Genauigkeit (später durch max_trial_num die Mittelung!)
            total_accuracy += accuracy;
            PlayerPrefs.SetFloat("Total_accuracy", total_accuracy);
            // Speichere die Szenenübergreifenden Variablen
            PlayerPrefs.Save();

            // Starte den Countdown Timer zum Laden der nächsten Szene
            countDown.GetComponent<Start_Timer>().StartCountdown();
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

    private float calculateAccuracy(GameObject object1, GameObject object2)
    {
        //Vector3 colliderSize = object1.GetComponent<MeshCollider>().bounds.size;
        float radius = 0.25f; // Found through an empirical process

        // Berechne den y-Abstand
        float yDistance = Mathf.Abs(object1.transform.position.y - object2.transform.position.y);

        // Berechne den z-Abstand
        float zDistance = Mathf.Abs(object1.transform.position.z - object2.transform.position.z);

        // Berechne den Gesamtabstand zu Radiusverhältnis
        float accuracy = Mathf.Abs(radius - Mathf.Sqrt(Mathf.Pow(yDistance, 2) + Mathf.Pow(zDistance, 2)));

        float round_acc = (float)System.Math.Round(accuracy, 4);
        return 100 - round_acc * 100;
    }
}
