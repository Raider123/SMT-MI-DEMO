using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTable : MonoBehaviour
{

    [SerializeField] private CollisionColorChange sign_left, sign_right;
    [SerializeField] private GameObject start_countdown;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (sign_left != null && sign_right != null)
        {
            if (sign_left.IsColliding && sign_right.IsColliding)
            {
                // Initiate the Trial Timer (or next scene) as soon as both hands rest on a table
                start_countdown.GetComponent<Start_Trial_Timer>().StartCountdown();
                // Immediately stop the script after starting the timer
                this.enabled = false;
            }
        }
    }
}
