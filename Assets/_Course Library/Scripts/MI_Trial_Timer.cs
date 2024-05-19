using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEngine.XR.Hands;

public class MI_Trial_Timer : MonoBehaviour
{
    [SerializeField] private float mi_trial_time = 2f; // Zeit in Sekunden für den Countdown im MI-Fall (zugleich die MI-Zeit)
    [SerializeField] private GameObject targetchooser; // Eine Referenz auf das Targetchooser Objekt, um das Tag des aktuellen Targets (left_hand, right_hand) zu erfragen
    [SerializeField] private GameObject L_Wrist, R_Wrist; // Eine Referenz auf die die "Echten"-Hände Objekte

    private float currentTime = 0f;
    private bool isCounting = false;

    public void Start() // Startet den Countdown (nur im MI-Fall)
    {
        // In the MI-case, we always perform the shooting motion at the end of the mi-time
        if(PlayerPrefs.GetInt("MI") == 1)
        {
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
            L_Wrist.GetComponent<XRHandTrackingEvents>().enabled = false;
            R_Wrist.GetComponent<XRHandTrackingEvents>().enabled = false;
            L_Wrist.GetComponent<Hand_Transform_Player>().enabled = true;
        }else if(new_target_tag == "right_hand_mi")
        {
            L_Wrist.GetComponent<XRHandTrackingEvents>().enabled = false;
            R_Wrist.GetComponent<XRHandTrackingEvents>().enabled = false;
            R_Wrist.GetComponent<Hand_Transform_Player>().enabled = true;
        }
    }
}