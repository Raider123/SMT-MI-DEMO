using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Break_Timer : MonoBehaviour
{
    public float countdownTime = 60f; // Zeit in Sekunden für den Countdown
    public TMP_Text timeText; // Referenz auf das Text-Objekt, das den Countdown anzeigt

    public int max_num_of_trials; // Anzahl an Durchläufen mit Pausen pro Spielstart
    public int trial_scene_index; // Wird die Durchlaufanzahl unterschritten, kehren wir zum Spiel zurück
    public int end_scene_index; // Wird die Durchlaufanzahl überschritten, dann fahren wir mit dem Endbildschirm fort

    static int actual_trial_num; // Interner Zähler, dass die Durchlaufszahl angibt.
    private float currentTime = 0f;
    private bool isCounting = false;

    void Start() // Startet den Countdown
    {
        isCounting = true;
        currentTime = 0f;

        PlayerPrefs.SetInt("Max_num_of_trials", max_num_of_trials);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (isCounting)
        {
            currentTime += Time.deltaTime;
            float remainingTime = countdownTime - currentTime;
            if (remainingTime <= 0)
            {
                // Realisierung des Szenenwechsels
                if (actual_trial_num == max_num_of_trials - 1)
                {
                    // Reset the actual trial num for the next playthrough
                    actual_trial_num = 0;
                    // Load the end scene
                    SceneManager.LoadScene(end_scene_index);
                }
                else
                {
                    // Check whether the last trial was a tutorial
                    if (PlayerPrefs.GetInt("Level") == 0)
                    {
                        // If the preceding trial was a tutorial, switch to a normal trial (Level 1) (We only show the tutorial in the very first trial)
                        PlayerPrefs.SetInt("Level", 1);
                    }
                    // Increase the actual trial num
                    actual_trial_num += 1;
                    // Load the trial scene again
                    SceneManager.LoadScene(trial_scene_index);               
                } 
                
                remainingTime = 0;
                isCounting = false;                     
            }
            UpdateCountdownUI(remainingTime);
        }
    }

    void UpdateCountdownUI(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = seconds.ToString();
    }
}