using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CommunismSystem : MonoBehaviour
{
    public int followers = 0;
    public int communism = 0;

    // Wanted System
    
    public int percentToStar = 0;
    public int wantedLevel = 0;
    
    // Increase when in certain areas
    private int percentToStarIncreaseMinimum = 1;
    private int percentToStarIncreaseMaximum = 5;

    private int forgetPercent; // Percent chance to forget about the player
    // Goes down every person to talk to
    
    // Interaction Stats 
    private int influenceSuccessRate = 25;
    private float secondsToInfluence = 5f;

    private int minimumSuccessRate = 0;
    
    // Rewards From Dialogue
    private int follwersGainedMinimum = 1;
    private int followersGainedMaximum = 3;
    private int communisumGainedMinimum = 10;
    private int communismGainedMaximum = 20;

    //private int areaOfEffect = 1;

    private bool isPopulation = false;

    public void OnInteract(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started && isPopulation)
        {
            StartInfluence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = false;
        }
    }
    private void StartInfluence()
    {
        int randomSuccess = Random.Range(minimumSuccessRate, 100);

        Debug.Log(randomSuccess + " / " + influenceSuccessRate);
        
        if (randomSuccess >= influenceSuccessRate)
        {
            communism += Random.Range(communisumGainedMinimum, communismGainedMaximum);
            followers += Random.Range(follwersGainedMinimum, followersGainedMaximum);
        }
        else
        {
            //percentToStar += Random.Range(percentToStarIncreaseMinimum, percentToStarIncreaseMaximum);
        }
    }
}
