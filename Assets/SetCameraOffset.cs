using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraOffset : MonoBehaviour
{
    public float customOffsetY;
    // Start is called before the first frame update
    void Start()
    {
        //Set the CameraOffset
        PlayerPrefs.SetFloat("camera", customOffsetY);
        PlayerPrefs.Save();
    }
}
