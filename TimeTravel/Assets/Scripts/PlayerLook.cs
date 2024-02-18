using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera cam; // assigned in inspector
    private float xRotation = 0f;

    public float xSens = 30f;
    public float ySens = 30f;

   public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // camera for looking up and down
        xRotation -= mouseY * Time.deltaTime * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //aply to camera transform
        cam.transform.localRotation= Quaternion.Euler(xRotation, 0, 0);

        //rotate player left and right
        transform.Rotate(mouseX * Time.deltaTime * xSens * Vector3.up);

    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
