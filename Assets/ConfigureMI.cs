using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureMI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // If MI_active, then set to 1, otherwise to 0
        PlayerPrefs.SetInt("MI", 1);
        PlayerPrefs.Save();
    }

}
