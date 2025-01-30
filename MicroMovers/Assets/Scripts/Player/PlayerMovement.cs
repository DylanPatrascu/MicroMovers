using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool direction = false;

    public bool Direction { get => direction; }
    public Vector2 Movement { get => movement; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
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

        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }

}
