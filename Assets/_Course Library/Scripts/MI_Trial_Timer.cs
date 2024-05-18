using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class MI_Trial_Timer : MonoBehaviour
{
    public float mi_trial_time = 2f; // Zeit in Sekunden für den Countdown im MI-Fall (zugleich die MI-Zeit)

    public GameObject targetchooser; // Eine Instanz des Targetchooser Objekts, um das Tag des aktuellen Targets (left_hand, right_hand) zu erfragen
    
    public GameObject L_Wrist; // Eine Instanz der Position des linken Handgelenks
    public GameObject R_Wrist; // Eine Instanz der Position des rechten Handgelenks

    public GameObject gesture_detection; // Eine Instanz der Gestenerkennung, die ausgeschaltet werden muss

    private float currentTime = 0f;
    private bool isCounting = false;

    public void Start() // Startet den Countdown (nur im MI-Fall)
    {
        // In the MI-case, we always perform the shooting motion at the end of the mi-time
        if(PlayerPrefs.GetInt("MI") == 1)
        {
            gesture_detection.SetActive(false);

            isCounting = true;
            currentTime = 0f;
        }   
    }

    void Update()
    {
        if (isCounting)
        {
            currentTime += Time.deltaTime;
            float remainingTime = mi_trial_time - currentTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isCounting = false;

                if (PlayerPrefs.GetInt("MI_active") == 1)
                {
                    PerformMImotion();
                }
            }

        }
    }


    void PerformMImotion()
    {
        string new_target_tag = targetchooser.GetComponent<OnSceneLoad>().getActualTarget().tag;

        if(new_target_tag == "left_hand_mi")
        {
            L_Wrist.GetComponent<TransformPlayer>().enabled = true;
        }else if(new_target_tag == "right_hand_mi")
        {
            R_Wrist.GetComponent<TransformPlayer>().enabled = true;
        }
    }
}