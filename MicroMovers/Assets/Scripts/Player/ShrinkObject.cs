using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObject : MonoBehaviour
{

    private bool objectInTrigger = false;
    private bool shrinkable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectInTrigger = true;
    }
}
