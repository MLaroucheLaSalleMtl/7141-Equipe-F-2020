using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float RotationClampLeft = -90f; //How wide can you look on the left,Serialized for Testing and might add it to player Settings as FOV
    [SerializeField] private float RotationClampRight = 90f;
    public float MouseSensitivity = 10f; //public cuz of furure setting menu, 10 is natural
    private float CurrentSpeed;
    public Transform PlayerBody;
    private float XRotation = 0f;

    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity /** Time.deltaTime*/;
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity /** Time.deltaTime*/;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);

    }
}
