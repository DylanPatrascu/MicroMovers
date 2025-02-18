using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 3f;
    [SerializeField] InventoryController inventoryController;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool direction = false;

    public bool Direction { get => direction; }
    public Vector2 Movement { get => movement; }

    public GameObject outsideCamera;
    public GameObject insideCamera;
    public int numObjects = 0;

    public AudioSource pickupNoise;
    public AudioSource doorNoise;
    public AudioSource shrinkNoise;
    public int maxZaps = 3;
    public int zaps = 0;

    //public GameObject zapUI;
    //public GameObject grabUI;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        insideCamera.SetActive(false);
        outsideCamera.SetActive(true);

    }


    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal")  > 0)
        {
            direction = false;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = true;
        }
        rb.velocity = movement * movementSpeed * Time.deltaTime;
        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name == "House")
        {
            transform.position = new Vector3(-13.0f, 0.7f, -0.2f);
            insideCamera.SetActive(true);
            outsideCamera.SetActive(false);
            doorNoise.Play();

        }
        else if(collision.gameObject.name == "FrontDoor")
        {
            transform.position = new Vector3(-27.5f, 1.9f, -0.2f);
            insideCamera.transform.position = new Vector3(-13.0f, 0.7f, -10.0f);

            insideCamera.SetActive(false);
            outsideCamera.SetActive(true);
            doorNoise.Play();


        }
        /*
        if(collision.tag == "Item")
        {
            grabUI.SetActive(true);
            if(collision.gameObject.GetComponent<Item>().shrinkable)
            {
                zapUI.SetActive(true);
            }
        }*/

        if (collision.tag == "Item" && Input.GetKeyDown(KeyCode.Space))
        {

            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null) 
            {
                inventoryController.CreateItem(item.itemID);
                pickupNoise.Play();

            }

            collision.gameObject.SetActive(false);
            //numObjects++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Item" && Input.GetKeyDown(KeyCode.Space))
        {

            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                Debug.Log(item.itemID);
                inventoryController.CreateItem(item.itemID);
                pickupNoise.Play();
            }

            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Item" && Input.GetKeyDown(KeyCode.E) && zaps < maxZaps)
        {

            Item item = collision.gameObject.GetComponent<Item>();
            if(item.shrinkable)
            {
                item.zItem.transform.gameObject.SetActive(true);
                item.transform.gameObject.SetActive(false);
                shrinkNoise.Play();
                zaps++;
            }

        }


    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        grabUI.SetActive(false);
        zapUI.SetActive(false);
    }*/

}
