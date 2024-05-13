using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelManagerStart : MonoBehaviour
{
    // Both Level Manager Gameobjects are deactivated by default - they are started by this script
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    // Start is called before the first frame update
    void Awake()
    {
        int level = PlayerPrefs.GetInt("Level");

        if (level == 0)
        {
            // Level 0: Tutorial
            level0.SetActive(true);
        }
        else if(level == 1)
        {
            // Level 1: Trial 
            level1.SetActive(true);
        }else if (level == 2)
        {
            // Level 2: Small Targets (differences to Level 1)
            level2.SetActive(true);
        }
        else
        {
            level3.SetActive(true);
        }
    }

}
