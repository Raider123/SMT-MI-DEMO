using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ChangeCameraOffset : MonoBehaviour
{
    public GameObject XR_RIG;
    // Start is called before the first frame update
    void Start()
    {
        float camera_offset = PlayerPrefs.GetFloat("camera");
        Debug.Log("CAMERA_OFFSET: " + camera_offset);

        XR_RIG.GetComponent<XROrigin>().CameraYOffset = camera_offset;

    }


}
