using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float Sensetivity = 0f;
    float XRot = 0f;
    public Transform Player;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse Y") * Sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse X") * Sensetivity * Time.deltaTime;

        XRot -= mouseX;
        XRot = Mathf.Clamp(XRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRot, 0, 0);

        Player.Rotate(Vector3.up * mouseY);
    }
}
