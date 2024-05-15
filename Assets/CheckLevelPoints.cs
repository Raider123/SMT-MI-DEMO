using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckLevelPoints : MonoBehaviour
{
    [SerializeField] private GameObject closed_ui; // Here we want to update the image of the UI for the next level button

    [SerializeField] private GameObject next_level_ui; // We want to activate the next level gameobject if sufficient points were collected

    [SerializeField] private GameObject next_level_gesture; // We want to activate the gesture if the next level is possible

    [SerializeField] private TMP_Text repeat_button_text; // Show the actual button text to repeat the preceding level

    [SerializeField] private TMP_Text next_level_text; // Show the actual button text to begin the following level

    [SerializeField] private TMP_Text locked_button_text; // Show the locked button with the required points

    [SerializeField] private int min_points_lvl2 = 60;
    [SerializeField] private int min_points_lvl3 = 120;

    // Compares the reached points with the required points for the next level
    void Awake()
    {
        float achieved_points = PlayerPrefs.GetFloat("achievement_score");
        int level = PlayerPrefs.GetInt("Level");
        Debug.Log(level);

        if ((level == 1 && achieved_points > min_points_lvl2) || 
            (level == 2 && achieved_points > min_points_lvl3)) 
        {
            closed_ui.SetActive(false);
            next_level_ui.SetActive(true);
            next_level_gesture.SetActive(true);

            repeat_button_text.text = "Wiederhole" + "\nLevel " + level.ToString();

            next_level_text.text = "Level " + (level + 1).ToString();
        }
        else
        {
            closed_ui.SetActive(true);

            repeat_button_text.text = "Wiederhole" + "\nLevel " + level.ToString();

            if (level == 1)
            {
                locked_button_text.text = "Punkte\n" + achieved_points.ToString() + " / " + min_points_lvl2.ToString();
            }
            else if(level == 2)
            {
                locked_button_text.text = "Punkte\n" + achieved_points.ToString() + " / " + min_points_lvl3.ToString();
            }
            else
            {
                locked_button_text.text = "Coming \nSoon";
            }
            
        }
    }

}
