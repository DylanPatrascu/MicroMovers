using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public int numObjects = 0;
    public Sprite halfFull;
    public Sprite full;
    public void OnCollisionEnter2D(Collision2D collider)
    {
        numObjects += collider.gameObject.GetComponent<PlayerMovement>().numObjects;
        collider.gameObject.GetComponent<PlayerMovement>().numObjects = 0;
        Debug.Log("Unloaded in Truck" + numObjects);
        if (numObjects > 0 && numObjects < 3)
        {
            sprite.sprite = halfFull;
        }
        else if(numObjects >= 3)
        {
            sprite.sprite = full;
        } 

        if(numObjects == 11)
        {
            Debug.Log("You Win");
        }
        
    }
}
