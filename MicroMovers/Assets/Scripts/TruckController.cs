using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public InventoryController inventoryController;
    public SpriteRenderer sprite;
    public int numObjects = 0;
    public int maxObjects = 11;
    public bool win = false;
    public Sprite halfFull;
    public Sprite full;
    public AudioSource dropNoise;
    public AudioSource winNoise;

    public void OnCollisionEnter2D(Collision2D collider)
    {
        numObjects += collider.gameObject.GetComponent<PlayerMovement>().numObjects;
        if (numObjects != maxObjects || numObjects == 0)
        {
            dropNoise.Play();
        }
        collider.gameObject.GetComponent<PlayerMovement>().numObjects = 0;

        inventoryController.ClearInventory();

        Debug.Log("Unloaded in Truck" + numObjects);
        if (numObjects > 0 && numObjects < 4)
        {
            sprite.sprite = halfFull;
        }
        else if(numObjects >= 6)
        {
            sprite.sprite = full;
        } 

        if(numObjects == maxObjects)
        {
            win = true;
            Debug.Log("You Win");
            winNoise.Play();
            Time.timeScale = 0;
        }
        
    }
}
