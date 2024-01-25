using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private GameObject nestPrefab;

    [SerializeField] private Collider2D spawnNestCollider2D;
    
    private int nestCountMinimum = 5;
    private int nestCountMaximum = 10;

    private int birdCountMinimum = 2;
    private int birdCountMaximum = 5;
    
    private Collider2D nestCollider2D;
    
    private List<Vector3> nestLocations = new List<Vector3>();
    private List<Vector3> birdLocations = new List<Vector3>();
    
    private float nestSpawnPointsMinX, nestSpawnPointsMinY, nestSpawnPointsMaxX, nestSpawnPointsMaxY;

    private void Awake()
    {
        nestLocations.Add(new Vector3(0, 0, 0));
        
        nestCollider2D = GetComponent<Collider2D>();
        
        nestSpawnPointsMinX = nestCollider2D.bounds.min.x;
        nestSpawnPointsMaxX = nestCollider2D.bounds.max.x;
        nestSpawnPointsMinY = nestCollider2D.bounds.min.y;
        nestSpawnPointsMaxY = nestCollider2D.bounds.max.y;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("Nest").buildIndex)
        {
            SpawnNest();
            SpawnBirds(spawnNestCollider2D);
        }
    }

    private void SpawnNest()
    {
        int randomNestCount = Random.Range(nestCountMinimum, nestCountMaximum);
        int currentCount = 0;

        while(currentCount < randomNestCount)
        {
            float randomX = Random.Range(nestSpawnPointsMinX, nestSpawnPointsMaxX);
            float randomY = Random.Range(nestSpawnPointsMinY, nestSpawnPointsMaxY);

            Vector3 randomVector = new Vector3(randomX, randomY, 0);
            bool isNearingExisitingNest = false;

            foreach (Vector3 nestLocation in nestLocations)
            {
                float distance = Vector3.Distance(randomVector, nestLocation);

                if (distance < 25)
                {
                    isNearingExisitingNest = true;
                    break;
                }
            }
            if (!isNearingExisitingNest)
            {
                nestLocations.Add(randomVector);
                GameObject newNest = Instantiate(nestPrefab, randomVector, Quaternion.identity);
                SpawnBirds(newNest.GetComponent<Collider2D>());
                currentCount++;   
            }
        }
        
        
    }

    private void SpawnBirds(Collider2D birdCollider)
    {
        int randomBirdCount = Random.Range(birdCountMinimum, birdCountMaximum);
        int currentCount = 0;
        
        float birdSpawnPointMinX = birdCollider.bounds.min.x;
        float birdSpawnPointMaxX = birdCollider.bounds.max.x;
        float birdSpawnPointMinY = birdCollider.bounds.min.y;
        float birdSpawnPointMaxY = birdCollider.bounds.max.y;
        
        while(currentCount < randomBirdCount)
        {
            float randomX = Random.Range(birdSpawnPointMinX, birdSpawnPointMaxX);
            float randomY = Random.Range(birdSpawnPointMinY, birdSpawnPointMaxY);

            Vector3 randomVector = new Vector3(randomX, randomY, 0);
            bool isNearingExisitingBird = false;

            foreach (Vector3 birdLocation in birdLocations)
            {
                float distance = Vector3.Distance(randomVector, birdLocation);

                if (distance < 1)
                {
                    isNearingExisitingBird = true;
                    break;
                }
            }
            if (!isNearingExisitingBird)
            {
                birdLocations.Add(randomVector);
                Instantiate(birdPrefab, randomVector, Quaternion.identity);
                currentCount++;   
            }
        }
    }
}
