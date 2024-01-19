using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CommunismSystem : MonoBehaviour
{
    public int followers = 0;

    public int communism = 0;
    
    // Intrusive Thought Minigame
    private Transform thoughtSpawnPoint;
    public GameObject intrusiveThoughtBoard;
    
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

    [SerializeField] private GameObject interactionSliderPrefab;

    public GameObject middleOfScreenPoint;

    public void OnInteract(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started && isPopulation)
        {
            StartIntrusiveThoughtMinigame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = true;
            thoughtSpawnPoint = other.gameObject.transform.GetComponentInChildren<Transform>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = false;
            thoughtSpawnPoint = null;
        }
    }

    private void StartIntrusiveThoughtMinigame()
    {
        GameObject minigameBoard = Instantiate(intrusiveThoughtBoard, thoughtSpawnPoint);
        
        float distanceToMoveX = (middleOfScreenPoint.transform.position.x - minigameBoard.transform.position.x) / 100;
        float distanceToMoveY = (middleOfScreenPoint.transform.position.y - minigameBoard.transform.position.y) / 100;
        
        StartCoroutine(WaitToMove(minigameBoard, distanceToMoveX, distanceToMoveY));
    }

    private IEnumerator WaitToMove(GameObject minigameBoard, float distanceToMoveX, float distanceToMoveY)
    {
        for (int i = 0; i < 100; i++)
        {
            minigameBoard.transform.position += new Vector3(distanceToMoveX, distanceToMoveY, 0);
            minigameBoard.transform.localScale += new Vector3(.1f, .04f, 0);
            yield return new WaitForSeconds(.05f);
        }
    }
}
