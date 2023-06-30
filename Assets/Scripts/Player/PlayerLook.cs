using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    public Camera PlayerCamera => playerCamera;
    
    public delegate void DynamicLookUpdate();
    public event DynamicLookUpdate OnLooked;

    [SerializeField]
    private Vector2 sensitivity;
    [SerializeField]
    private float MaxAngle, MinAngle;

    private float xRotation = 0;

    public void ProcessLook(Vector2 input)
    {
        xRotation -= (input.y * Time.deltaTime) * sensitivity.y;
        xRotation = Mathf.Clamp(xRotation, MinAngle, MaxAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * sensitivity.x);

        if (OnLooked != null)
        {
            OnLooked();
        }
    }

}
