using System;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private int eatenboids = 0;
    private Vector3 snakebodysize;
    private float snakebodysizez;
    //public GameObject body;
    private float currentamount;
    public GameObject numberofboids;
    public GameObject particle;

    public float speed = 10f;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    public static Fish thisObj;
    public GameObject boidsbar;
    public Text sizetxt;
    public Text timertxt;
    float Timer = 0;

    void Start()
    {
        thisObj = this.GetComponent<Fish>();
        controller = GetComponent<CharacterController>();
        //snakebodysize = body.GetComponent<Renderer>().bounds.size;
        //snakebodysizez = snakebodysize.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.CompareTag("Boid"))
        {
            eatenboids += 1;
           
            Debug.Log("It's a Boid");

            GetComponent<AudioSource>().Play();
            Instantiate(particle, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // if (other.gameObject.CompareTag("Water"))
        //{

       // }
    }

    private void Update()
    {
        if(numberofboids.transform.childCount != 0)
        {
            Timer += Time.deltaTime;
        }
        if(currentamount < 100)
        {
            currentamount = eatenboids;
        }
        
        sizetxt.text = "Snake Size: " + snakebodysizez.ToString("F0");
        timertxt.text = "Time: " + Timer.ToString("F0") + "s";
    }

    void LateUpdate()
    {
        boidsbar.GetComponent<Image>().fillAmount = currentamount / 100;
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
        //     RotateCam.is<Fish = false;
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

