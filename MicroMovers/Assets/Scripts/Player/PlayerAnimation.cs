using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement movement;
    SpriteRenderer sprite;
    Animator animator;
    public AudioSource footsteps;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.flipX = movement.Direction;
        if (movement.Movement != Vector2.zero)
        {
            animator.Play("Run");
            if(!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            animator.Play("Idle");
            footsteps.Stop();
        }
    }

}
