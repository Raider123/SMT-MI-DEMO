using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRRigMover : MonoBehaviour
{
    [Tooltip("Link to the XR Rig object. WASD to move, Q+E for rotation and R+F for UP+DOWN.")]
    [SerializeField] private Transform xrRig;
    [Tooltip("Speed of movement WASD + RF.")]
    [SerializeField] private float moveSpeed = 1f;
    [Tooltip("Speed of rotation QE.")]
    [SerializeField] private float rotationSpeed = 1f;
    [Tooltip("Select wether movement should be based ion the world or self space.")]
    [SerializeField] private Space space = Space.World;

    private Vector2 wasd;
    private float rotationAxis;
    private float upDownAxis;

    private void Update()
    {
        wasd = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rotationAxis = Input.GetAxis("Rotate");
        upDownAxis = Input.GetAxis("UpDown");

        Vector3 move = new Vector3(wasd.x, upDownAxis, wasd.y) * moveSpeed * Time.deltaTime;
        xrRig.Translate(move, Space.World);

        Vector3 rotate = new Vector3(0, rotationAxis, 0) * rotationSpeed * Time.deltaTime;
        xrRig.Rotate(rotate, Space.World);
    }
}
