using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnBreakSceneStart : MonoBehaviour
{
    [SerializeField] private GameObject XR_Rig;
    [SerializeField] private GameObject table_object;
    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt("Level");
        if (level == 2)
        {
            // Increase the distance to the objects by moving the XR Rig further back
            XR_Rig.transform.localPosition = new Vector3(XR_Rig.transform.position.x + 2f, XR_Rig.transform.position.y, XR_Rig.transform.position.z);
            // Move the table back too
           table_object.transform.localPosition = new Vector3(table_object.transform.position.x + 2f, table_object.transform.position.y, table_object.transform.position.z);
        }
    }
 
}
