using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// When the scene is played, run some specific functionality
/// </summary>
public class OnSceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToReplace; // Array der vorhandenen GameObjekte
    [SerializeField] private GameObject[] replacementObjects; // Das Objekt, das ein vorhandenes ersetzen soll

    [SerializeField] private float grow_duration = 1.0f; // Animationsdauer zum Wachsen

    [SerializeField] private GameObject left_hand_gesture_detection; // Gestikfunktion der linken Hand
    [SerializeField] private GameObject right_hand_gesture_detection; // Gestikfunktion der rechten Hand

    [SerializeField] private GameObject XR_Rig; // Kameraposition des Kopfes

    [SerializeField] private GameObject L_Wrist, R_Wrist;

    private GameObject new_target; // The final target that is created at the exchanged position 

    public void Start()
    {
        // Eine kurze Verzögerung, um sicherzustellen, dass alle Objekte initialisiert sind
        objectsToReplace = new GameObject[9];
        replacementObjects = new GameObject[2];
    }

    public GameObject getActualTarget()
    {
        return new_target;
    }

    public void Awake()
    { 
        // Wähle zufällig ein vorhandenes Objekt aus
        int randomIndex = Random.Range(0, objectsToReplace.Length);

        // Deaktiviere das ausgewählte Objekt
        objectsToReplace[randomIndex].SetActive(false);
        
        // Wähle zufällig ein Ersatzobjekt aus
        int randomReplacementIndex = Random.Range(0, replacementObjects.Length);

        // Aktiviere das ausgewählte Ersatzobjekt an der Position des ausgewählten Objekts
        GameObject selectedReplacementObject = replacementObjects[randomReplacementIndex];

        // Aktiviere alle unsichtbaren Objekte (d.h. alle Zielscheiben)
        selectedReplacementObject.SetActive(true);

        // Erstelle eine neues Objekt an der Position des alten Objekts
        new_target = Instantiate(selectedReplacementObject, objectsToReplace[randomIndex].transform.position, objectsToReplace[randomIndex].transform.rotation);

        // Deaktiviere das ersetzte Objekt
        objectsToReplace[randomIndex].SetActive(false);
        // Deaktiviere das Ursprungsobjekt, mit dem ersetzt wurde
        selectedReplacementObject.SetActive(false);

        // Here we specifiy changes of higher levels in relation to lower levels (for example, adjusting the scale of the targets in level 2)
        int level = PlayerPrefs.GetInt("Level");
        if (level == 2)
        {
            // Decrease the target size in the appropriate level
            //new_target.transform.localScale = Vector3.one * 1.25f;
            // Increase the distance to the objects by moving the XR Rig further back
            XR_Rig.transform.localPosition = new Vector3(2f, 0f , 0.176f);

        }else if (level == 3)
        {
            new_target.GetComponent<MoveOnSquarePath>().enabled = true;
        }

        // Grow all the targets over time
        StartCoroutine(GrowOverTime(new_target));
        /* We have disabled the visual of the game objects, thus we can save some processing power
        for (int i = 0; i < objectsToReplace.Length; i++)
        {
            StartCoroutine(GrowOverTime(objectsToReplace[i]));
        }
        */

        // Zeige das Textmeshpro Objekt in der Farbe der Zielscheibe an
        if (new_target.tag == "left_hand_mi")
        {
            right_hand_gesture_detection.SetActive(false); // the correct gesture starts the countdown timer
           
        }else if(new_target.tag == "right_hand_mi")
        {
            left_hand_gesture_detection.SetActive(false); // the correct gesture starts the countdown timer
        }
    }

    IEnumerator GrowOverTime(GameObject selfreference)
    {
        // Startskalierung des Objekts
        Vector3 startScale = Vector3.zero; 

        // Ziel-Skalierung des Objekts 
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

}

