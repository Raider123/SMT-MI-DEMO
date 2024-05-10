using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelManagerStart : MonoBehaviour
{
    // Both Level Manager Gameobjects are deactivated by default - they are started by this script
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject level1;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.GetInt("achievement_score");

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            // If the level is set to 0, then start the level scene
            level0.SetActive(true);
        }
        else
        {
            // Otherwise start the normal trial scene
            level1.SetActive(true);
        }
    }

}
