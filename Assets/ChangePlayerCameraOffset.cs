using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerCameraOffset : MonoBehaviour
{
    [SerializeField] private float CameraYOffset;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("camera", CameraYOffset);
        PlayerPrefs.Save();
    }

}
