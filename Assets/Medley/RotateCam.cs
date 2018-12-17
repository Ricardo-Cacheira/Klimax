using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{

    // Use this for initialization
    public GameObject player;
    public static Quaternion CameraRotation;
    public Transform initialCheckpoint;
    public static Transform currentCheckpoint;
    public static bool isFish;

    void Start()
    {
        
        currentCheckpoint = initialCheckpoint;
    }

    private void LateUpdate()
    {
        float mouseY = Input.GetAxis("RightV");
        float mouseX = Input.GetAxis("RightH");
        CameraRotation = transform.rotation;
        transform.position = player.transform.position;
        transform.RotateAround(transform.position, Vector3.up, mouseX * 2);
        if (mouseY > 0.4f || mouseY < -0.4f)
        {
            transform.Rotate(new Vector3(mouseY, 0, 0));
            if (transform.eulerAngles.x >= 30 && transform.eulerAngles.x <= 300)
                transform.eulerAngles = new Vector3(30, transform.eulerAngles.y, transform.eulerAngles.z);
            else if (transform.eulerAngles.x <= 330 && transform.eulerAngles.x >= 310)
                transform.eulerAngles = new Vector3(330, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
