using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CommunismSystem : MonoBehaviour
{
    private int communsimRequiredForLevelingUp = 200;
    //[SerializeField] private GameObject levelUpPrefab;
    
    // Add a instance later
    
    private int followers = 0;
    private int communism = 500;
    
    // Follow Stats
    public int followerMultiplier = 1;
    private bool isUpdatingFollowers = false;
    
    // Fame Stats
    public int fameIncreaseMinimum = 1;
    public int fameIncreaseMaximum = 5;
    
    // Intrusive Thought Minigame
    [Header("Intrusive Thought Minigame Prefabs")]
    [SerializeField] private GameObject followersGainedPrefab;
    [SerializeField] private GameObject communismGainedPrefab;
    
    private bool isMinigameStarted = false;
    
    private Transform thoughtSpawnPoint;
    public GameObject intrusiveThoughtBackgroundPrefab;
    public List<GameObject> intrusiveThoughtsPrefabs;
    
    private GameObject minigameBoard;
    
    [Header("Intrusive Thought Minigame Random Values")]
    
    public float intrusiveThoughtSecondsTillSpawn = .1f; 
    public int intrusiveThoughtsMinimum = 20;
    public int intrusiveThoughtsMaximum = 30;

    [Header("Intrusive Thought Minigame Tracker")]
    
    public int totalIntrusiveThoughts = -1;
    public int totalIntrusiveThoughtsLeft;
    
    public float chanceToSucceed = 100;

    public int totalIntrusiveThoughtsDestroyed = -1;
    
    private bool doneSpawningIntrusiveThoughts = false;
    
    [Header("Wanted System")]
    
    public int percentToStar = 0;
    public int wantedLevel = 0;
    
    // Increase when in certain areas
    //private int percentToStarIncreaseMinimum = 1;
    //private int percentToStarIncreaseMaximum = 5;
    
    // Interaction Stats 
    private float secondsToInfluence = 2f;
    
    // Rewards From Dialogue
    private int followersGainedMinimum = 1;
    private int followersGainedMaximum = 3;
    private int communismGainedMinimum = 10;
    private int communismGainedMaximum = 20;
    
    private Collider2D _populationCollider;

    private PlayerMovement _playerMovement;
    private FameSystem _fameSystem;
    
    private bool isPopulation = false;
    
    [SerializeField] private GameObject interactionSliderPrefab;

    public GameObject middleOfScreenPoint;
    
    public static CommunismSystem instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Communist System in the scene.");
        }

        instance = this;
        
        _playerMovement = GetComponent<PlayerMovement>();
        chanceToSucceed = 100;
        _fameSystem = FindObjectOfType<FameSystem>();
    }
    private void Update()
    {
        UpdateEnemyCount();
        UpdatePercentToSucceed();
        CheckForLevelUp();
        UpdateLevelUp();
        if (!isUpdatingFollowers)
            StartCoroutine(FollowerIncome());
        if (((totalIntrusiveThoughtsDestroyed >= totalIntrusiveThoughts && doneSpawningIntrusiveThoughts) || chanceToSucceed <= 0) && isMinigameStarted)
        {
            EndIntrusiveThoughtMinigame();
        }
    }

    private void UpdateLevelUp()
    {
        string communismPercentFilled = Mathf.Ceil((float)communism / (float)communsimRequiredForLevelingUp * 100).ToString();
        UIManager.instance.SetProgress(communism, communismPercentFilled);
        UIManager.instance.SetMaxProgress(communsimRequiredForLevelingUp);
    }
    private void CheckForLevelUp()
    {
        if (communism < communsimRequiredForLevelingUp) return;
        UIManager.instance.ShowThanksForPlayingPanel();
    }
    public int GetCommunism()
    {
        return communism;
    }
    public void RemoveCommunism(int communismToRemove)
    {
        communism -= communismToRemove;
    }
    public int GetFollowers()
    {
        return followers;
    }
    public void OnLeftClick(InputAction.CallbackContext ctxt)
    {
        if (!ctxt.started) return;
        
        var intrusiveThoughtMask = LayerMask.GetMask("Intrusive Thought");
        
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, intrusiveThoughtMask);

        
        if (hit.collider == null) return;
        if (hit.collider.CompareTag("IntrusiveThought"))
        {
            totalIntrusiveThoughtsDestroyed += 1;
            totalIntrusiveThoughtsLeft -= 1;
            Destroy(hit.collider.gameObject);
        }
    }

    public void OnInteract(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started && isPopulation && !isMinigameStarted && _populationCollider.CompareTag("Population"))
        {
            StartIntrusiveThoughtMinigame();
            isMinigameStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = true;
            thoughtSpawnPoint = other.gameObject.transform.GetComponentInChildren<Transform>();
            _populationCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Population"))
        {
            isPopulation = false;
        }
    }

    private IEnumerator FollowerIncome()
    {
        isUpdatingFollowers = true;
        yield return new WaitForSeconds(60);
        communism += followers * followerMultiplier;
        isUpdatingFollowers = false;
    }
    
    private void UpdateEnemyCount()
    {
        if (GameObject.Find("Enemy Counter") == null) return;
        GameObject enemiesLeftText = GameObject.Find("Enemies Left");
        
        enemiesLeftText.GetComponent<TextMeshProUGUI>().text =  totalIntrusiveThoughtsLeft.ToString();
    }

    private void UpdatePercentToSucceed()
    {
        if (GameObject.Find("Percent Text") == null) return;
        GameObject percentText = GameObject.Find("Percent Text");
        
        percentText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(chanceToSucceed) + "%";
    }
    
    private void StartIntrusiveThoughtMinigame()
    {
        minigameBoard = Instantiate(intrusiveThoughtBackgroundPrefab, thoughtSpawnPoint);
        
        _playerMovement.shouldMove = false;

        var position = middleOfScreenPoint.transform.position;
        float distanceToMoveX = (position.x - position.x) / 100;
        float distanceToMoveY = (position.y - position.y) / 100;
        
        StartCoroutine(WaitToMove(minigameBoard, distanceToMoveX, distanceToMoveY));
    }
    private void EndIntrusiveThoughtMinigame()
    {
        
        Destroy(minigameBoard);
        _playerMovement.shouldMove = true;
        doneSpawningIntrusiveThoughts = false;
        
        totalIntrusiveThoughtsDestroyed = 0;
        isMinigameStarted = false;

        if (chanceToSucceed <= 0)
        {
            chanceToSucceed = 100;
            return;
        }
        
        int didPlayerSucceed = Random.Range(0, 100);
        
        if (didPlayerSucceed <= chanceToSucceed)
        {
            thoughtSpawnPoint.tag = "Untagged";
            
            var followersGained = Random.Range(followersGainedMinimum, followersGainedMaximum);
            var communismGained = Random.Range(communismGainedMinimum, communismGainedMaximum);
            followers += followersGained;
            communism += communismGained;

            _fameSystem.GainFame(Random.Range(fameIncreaseMinimum, fameIncreaseMaximum));
            
            StartCoroutine(ShowResourcesGained(followersGained, communismGained));
            
            thoughtSpawnPoint.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.red;
            Destroy(thoughtSpawnPoint.transform.GetChild(0).gameObject);
        }
        else
        {
            Debug.Log("Wanted System Not Implemented Yet"); // Temp till the system gets added
            /*
            var amountToIncreaseStar = Random.Range(percentToStarIncreaseMinimum, percentToStarIncreaseMaximum);
            
            percentToStar += amountToIncreaseStar;

            if (percentToStar < 100) return;
            wantedLevel += 1;
            percentToStar = 0;
            */
        }
        chanceToSucceed = 100;
    }
    private IEnumerator SpawnIntrusiveThoughts(GameObject minigameBoard)
    {
        totalIntrusiveThoughts = Random.Range(intrusiveThoughtsMinimum, intrusiveThoughtsMaximum);
        totalIntrusiveThoughtsLeft = totalIntrusiveThoughts;
        Transform spawnPoints = minigameBoard.transform.Find("Spawn Points");
        List<Transform> intrusiveThoughtsSpawnpoints = new List<Transform>();
        
        foreach (Transform intrusiveThoughtSpawnPoint in spawnPoints.transform.GetComponentInChildren<Transform>())
        {
            intrusiveThoughtsSpawnpoints.Add(intrusiveThoughtSpawnPoint);
        }
        for (int i = 0; i < totalIntrusiveThoughts; i++)
        {
            int randomSpawnPoint = Random.Range(0, intrusiveThoughtsSpawnpoints.Count);
            int randomIntrusiveThought = Random.Range(0, intrusiveThoughtsPrefabs.Count);
            
            GameObject intrusiveThought = Instantiate(intrusiveThoughtsPrefabs[randomIntrusiveThought], intrusiveThoughtsSpawnpoints[randomSpawnPoint]);
            
            if (chanceToSucceed <= 0) break;
            
            yield return new WaitForSeconds(intrusiveThoughtSecondsTillSpawn);
        }
        
        doneSpawningIntrusiveThoughts = true;
    }

    private IEnumerator WaitToMove(GameObject minigameBoard, float distanceToMoveX, float distanceToMoveY)
    {
        for (int i = 0; i < 100; i++)
        {
            minigameBoard.transform.position += new Vector3(distanceToMoveX, distanceToMoveY, 0);
            minigameBoard.transform.localScale += new Vector3(.03f, .03f, 0); // Tweak the x and y to change the size it scales to
            yield return new WaitForSeconds(secondsToInfluence / 100);
        }
        StartCoroutine(SpawnIntrusiveThoughts(minigameBoard));
    }

    private IEnumerator ShowResourcesGained(int followersGained, int communismGained)
    { ;
        GameObject followersGainedText = Instantiate(followersGainedPrefab, 
            new Vector3(
                RandomRangeExcludeValue(_populationCollider.bounds.min.x * .87f, _populationCollider.bounds.min.x * .95f, _populationCollider.bounds.max.x * 1.2f, _populationCollider.bounds.max.x * .95f), 
                Random.Range(_populationCollider.bounds.min.y + (.5f * (_populationCollider.bounds.max.y - _populationCollider.bounds.min.y)), _populationCollider.bounds.max.y + (-.2f*(_populationCollider.bounds.max.y - _populationCollider.bounds.min.y))),
                0),
            Quaternion.Euler(0, 0, Random.Range(-30, 30)));
        
        
        GameObject communismGainedText = Instantiate(communismGainedPrefab, 
            new Vector3(
                RandomRangeExcludeValue(_populationCollider.bounds.min.x * .87f, _populationCollider.bounds.min.x * .95f, _populationCollider.bounds.max.x * 1.2f, _populationCollider.bounds.max.x * .95f), 
                Random.Range(_populationCollider.bounds.min.y + (.5f * (_populationCollider.bounds.max.y - _populationCollider.bounds.min.y)), _populationCollider.bounds.max.y + (-.2f*(_populationCollider.bounds.max.y - _populationCollider.bounds.min.y))),
                0),
            Quaternion.Euler(0, 0, Random.Range(-30, 30)));
        
        followersGainedText.GetComponentInChildren<TextMeshProUGUI>().text = "+" + followersGained;
        communismGainedText.GetComponentInChildren<TextMeshProUGUI>().text = "+" + communismGained;
        yield return new WaitForSeconds(5);
        Destroy(followersGainedText);
        Destroy(communismGainedText);
    }

    private float RandomRangeExcludeValue(float lowestValue1, float highestValue1, float lowestValue2, float highestValue2)
    {
        float randomRange1 = Random.Range(lowestValue1, highestValue1);
        float randomRange2 = Random.Range(lowestValue2, highestValue2);
        
        int randomNum = Random.Range(0, 2);
        
        if (randomNum == 0)
        {
            return randomRange1;
        }
        else
        {
            return randomRange2;
        }
    }
}
