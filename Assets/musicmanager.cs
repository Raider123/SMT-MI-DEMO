using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
    [SerializeField] private AudioSource musik;

    [SerializeField] private float anstiegsgeschwindigkeit = 0.15f; // Die Geschwindigkeit, mit der die Lautstärke ansteigt
    [SerializeField] private float zielLautstärke = 1.0f; // Die gewünschte Endlautstärke

    void Start()
    {
        // Beginne mit einer Lautstärke von 0
        musik.volume = 0f;

        // Starte die Funktion zur Lautstärkeerhöhung
        StartCoroutine(ErhöheLautstärke());
    }

    IEnumerator ErhöheLautstärke()
    {
        // Schrittweise Erhöhung der Lautstärke
        while (musik.volume < zielLautstärke)
        {
            musik.volume += anstiegsgeschwindigkeit * Time.deltaTime;

            // Stelle sicher, dass die Lautstärke nicht über das Ziel hinausgeht
            musik.volume = Mathf.Min(musik.volume, zielLautstärke);

            yield return null;
        }
    }
}
