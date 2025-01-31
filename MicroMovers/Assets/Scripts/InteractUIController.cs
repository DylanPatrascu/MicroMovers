using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractUIController : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite key;
    public Sprite keyPressed;

    public float timeLength = 1.0f;
    public float timer = 0;

    public void Update()
    {
        if(timer < timeLength)
        {
            timer += Time.deltaTime;
        } else
        {
            timer = 0;
            if(spriteRenderer.sprite == key)
            {
                spriteRenderer.sprite = keyPressed;
            } else
            {
                spriteRenderer.sprite = key;
            }
        }
    }
}
