using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class End_Timer : MonoBehaviour
{
    public float countdownTime = 60f; // Zeit in Sekunden für den Countdown
    public TMP_Text timeText; // Referenz auf das Text-Objekt, das den Countdown anzeigt
    public int next_sceneIndex;

    private float currentTime = 0f;
    private bool isCounting = false;

    void Update()
    {
        if (isCounting)
        {
            currentTime += Time.deltaTime;
            float remainingTime = countdownTime - currentTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isCounting = false;

                // Load the next scene
                SceneManager.LoadScene(next_sceneIndex);
            }

            UpdateCountdownUI(remainingTime);
        }
    }

    public void StartCountdown() // Startet den Countdown
    {
        isCounting = true;
        currentTime = 0f;
    }

    void UpdateCountdownUI(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = seconds.ToString();
    }
}