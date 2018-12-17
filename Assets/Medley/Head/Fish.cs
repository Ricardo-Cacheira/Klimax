using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    public static Fish thisObj;

    void Start()
    {
        thisObj = this.GetComponent<Fish>();
        controller = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Water"))
       // {

        //}
    }

    private void OnTriggerExit(Collider other)
    {
       // if (other.gameObject.CompareTag("Water"))
        //{

       // }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Boid"))
        {
            Debug.Log("It's a Boid");
            Destroy(collision.gameObject);
        }
    }

    

    void LateUpdate()
    {
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");
        float axisY = 0;


        if (Input.GetButton("Ascend"))
            axisY = 1;
        if (Input.GetButton("Descend"))
            axisY = -1;

        //axisY = -axisY;
        moveDirection = new Vector3(axisH, axisY, axisV) * speed;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        moveDirection.x = axisH * speed;
        moveDirection.z = axisV * speed;
        moveDirection.y = axisY * speed;

        moveDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * moveDirection;

        if (new Vector3(moveDirection.x, 0, moveDirection.z).magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));

        controller.Move(moveDirection * Time.deltaTime);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
     

        // if (hit.gameObject.name.StartsWith("Player"))
        // {
        //     RotateCam.isFish = false;
        //     RotateCam.player = hit.gameObject;
        //     this.GetComponent<Fish>().enabled = false;
        //     hit.gameObject.GetComponent<player>().enabled = true;
        //     //player.thisObj.enabled = true;
        // }
        // if (hit.gameObject.name.StartsWith("Mouse"))
        // {
        //     RotateCam.isFish = false;
        //     RotateCam.player = hit.gameObject;
        //     this.GetComponent<Fish>().enabled = false;
        //     hit.gameObject.GetComponent<Mouse>().enabled = true;
        // }
        // if (hit.gameObject.name.StartsWith("Bird"))
        // {
        //     RotateCam.isFish = true;
        //     RotateCam.player = hit.gameObject;
        //     this.GetComponent<Fish>().enabled = false;
        //     hit.gameObject.GetComponent<Bird>().enabled = true;
        // }



    }
}

