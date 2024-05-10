using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// When the scene is played, run some specific functionality
/// </summary>
public class OnSceneLoad : MonoBehaviour
{
    // When scene is loaded and play begins
    public UnityEvent OnLoad = new UnityEvent();

    [SerializeField] private GameObject[] objectsToReplace; // Array der vorhandenen GameObjekte
    [SerializeField] private GameObject[] replacementObjects; // Das Objekt, das ein vorhandenes ersetzen soll

    [SerializeField] private float grow_duration = 1.0f; // Animationsdauer zum Wachsen
    [SerializeField] private float shrink_duration = 1.0f; // Schrumpfen - Animationsdauer

    private GameObject new_target; // The final target that is created at the exchanged position 

    public void Start()
    {
        // Eine kurze Verzögerung, um sicherzustellen, dass alle Objekte initialisiert sind
        objectsToReplace = new GameObject[9];
        replacementObjects = new GameObject[2];
    }

    public void Awake()
    { 
        SceneManager.sceneLoaded += PlayEvent;

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

        // Grow all the targets over time
        StartCoroutine(GrowOverTime(new_target));
        for (int i = 0; i < objectsToReplace.Length; i++)
        {
            StartCoroutine(GrowOverTime(objectsToReplace[i]));
        }
    }

    private void PlayEvent(Scene scene, LoadSceneMode mode)
    {
        OnLoad.Invoke();
    }

    IEnumerator GrowOverTime(GameObject selfreference)
    {
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
}

