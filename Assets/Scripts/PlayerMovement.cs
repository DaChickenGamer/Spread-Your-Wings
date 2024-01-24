using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;

    // Add a instance here later
    
    public bool shouldMove = true;

    private Vector2 direction;
    private Rigidbody2D rb; // Change to Rigibody if in 3D
    private Animator animator;
    
    // Music Loop Test ( DELETE LATER )
    private EventInstance testLoop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Change to Rigibody if in 3D
        //animator = GetComponent<Animator>();
    }
    public void OnMovement(InputAction.CallbackContext ctxt)
    {
        direction = ctxt.ReadValue<Vector2>();

       /*if (direction.x != 0 || direction.y != 0)
       {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            animator.SetBool("isWalking", true);
       }
       else
       {
            animator.SetBool("isWalking", false);
       }*/
    }

    private void Start()
    {
        testLoop = AudioManager.instance.CreateInstance(FMODEvents.instance.testLoop);
    }

    // TEST DELETE LATER
    private void UpdateSound()
    {
        // start the test loop event if the player has an x velocity
        if (rb.velocity.x != 0)
        {
            // get the playback state
            PLAYBACK_STATE playerbackState;
            testLoop.getPlaybackState(out playerbackState);

            if (playerbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                testLoop.start();
            }

        }
        // other stop the foot steps event
        else
        {
            testLoop.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void Update()
    {
        UpdateSound();
        
        if (shouldMove)
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

}