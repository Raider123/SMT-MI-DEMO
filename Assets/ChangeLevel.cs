using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public void switchlevel()
    {
        int level = PlayerPrefs.GetInt("Level");
        // The switch from Level 0 to Level 1 happens already in the first break session!
        if (level == 1)
        {
            change_level(2);
        }
        if (level == 2)
        {
            change_level(3);
        }
    }

    private void change_level(int next_level)
    {
        PlayerPrefs.SetInt("Level", next_level);
        PlayerPrefs.Save();
    }
}
