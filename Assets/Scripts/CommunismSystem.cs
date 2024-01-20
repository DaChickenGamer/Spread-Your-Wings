using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CommunismSystem : MonoBehaviour
{
    public int followers = 0;

    public int communism = 0;
    
    // Intrusive Thought Minigame
    private Transform thoughtSpawnPoint;
    public GameObject intrusiveThoughtBackgroundPrefab;
    public List<GameObject> intrusiveThoughtsPrefabs;
    
    private GameObject minigameBoard;
    
    public float intrusiveThoughtSecondsTillSpawn = .1f; 
    public int intrusiveThoughtsMinimum = 20;
    public int intrusiveThoughtsMaximum = 30;

    private int totalIntrusiveThoughts = -1;

    public int intrusiveThoughtsFailedToClick = 0;
    
    public int chanceToSucceed = 100;

    public int totalIntrusiveThoughtsDestroyed = -1;
    
    private bool doneSpawningIntrusiveThoughts = false;
    
    // Wanted System
    
    public int percentToStar = 0;
    public int wantedLevel = 0;
    
    // Increase when in certain areas
    private int percentToStarIncreaseMinimum = 1;
    private int percentToStarIncreaseMaximum = 5;

    private int forgetPercent; // Percent chance to forget about the player
    // Goes down every person to talk to
    
    // Interaction Stats 
    private float secondsToInfluence = 2f;
    
    // Rewards From Dialogue
    private int followersGainedMinimum = 1;
    private int followersGainedMaximum = 3;
    private int communisumGainedMinimum = 10;
    private int communismGainedMaximum = 20;

    private PlayerMovement _playerMovement;
    
    private bool isPopulation = false;
    
    [SerializeField] private GameObject interactionSliderPrefab;

    public GameObject middleOfScreenPoint;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (totalIntrusiveThoughtsDestroyed >= totalIntrusiveThoughts && doneSpawningIntrusiveThoughts)
        {
            EndIntrusiveThoughtMinigame();
        }
    }

    public void OnLeftClick(InputAction.CallbackContext ctxt)
    {
        if (!ctxt.started) return;
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider == null) return;
        if (hit.collider.CompareTag("IntrusiveThought"))
        {
            totalIntrusiveThoughtsDestroyed += 1;
            Destroy(hit.collider.gameObject);
        }
        else
        {
            return;
        }
    }

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
        minigameBoard = Instantiate(intrusiveThoughtBackgroundPrefab, thoughtSpawnPoint);
        
        _playerMovement.shouldMove = false;
        
        float distanceToMoveX = (middleOfScreenPoint.transform.position.x - minigameBoard.transform.position.x) / 100;
        float distanceToMoveY = (middleOfScreenPoint.transform.position.y - minigameBoard.transform.position.y) / 100;
        
        StartCoroutine(WaitToMove(minigameBoard, distanceToMoveX, distanceToMoveY));
    }
    private void EndIntrusiveThoughtMinigame()
    {
        int didPlayerSucceed = Random.Range(0, 100);
        
        if (didPlayerSucceed <= chanceToSucceed)
        {
            followers += Random.Range(followersGainedMinimum, followersGainedMaximum);
            communism += Random.Range(communisumGainedMinimum, communismGainedMaximum);
            Destroy(minigameBoard);
            _playerMovement.shouldMove = true;
            doneSpawningIntrusiveThoughts = false;
        }
        else
        {
            // Add the wanted system here
        }
    }
    
    private IEnumerator SpawnIntrusiveThoughts(GameObject minigameBoard)
    {
        int totalIntrusiveThoughts = Random.Range(intrusiveThoughtsMinimum, intrusiveThoughtsMaximum);
        Transform spawnPoints = minigameBoard.transform.Find("Spawn Points");
        List<Transform> intrusiveThoughts = new List<Transform>();
        foreach (Transform intrusiveThoughtSpawnPoint in spawnPoints.transform.GetComponentInChildren<Transform>())
        {
            intrusiveThoughts.Add(intrusiveThoughtSpawnPoint);
        }
        for (int i = 0; i < totalIntrusiveThoughts; i++)
        {
            int randomSpawnPoint = Random.Range(0, intrusiveThoughts.Count);
            int randomIntrusiveThought = Random.Range(0, intrusiveThoughtsPrefabs.Count);
            GameObject intrusiveThought = Instantiate(intrusiveThoughtsPrefabs[randomIntrusiveThought], intrusiveThoughts[randomSpawnPoint]);
            yield return new WaitForSeconds(intrusiveThoughtSecondsTillSpawn);
        }
        
        doneSpawningIntrusiveThoughts = true;
    }

    private IEnumerator WaitToMove(GameObject minigameBoard, float distanceToMoveX, float distanceToMoveY)
    {
        for (int i = 0; i < 100; i++)
        {
            minigameBoard.transform.position += new Vector3(distanceToMoveX, distanceToMoveY, 0);
            minigameBoard.transform.localScale += new Vector3(.12f, .05f, 0); // Tweak the x and y to change the size it scales to
            yield return new WaitForSeconds(secondsToInfluence / 100);
        }
        StartCoroutine(SpawnIntrusiveThoughts(minigameBoard));
    }
}
