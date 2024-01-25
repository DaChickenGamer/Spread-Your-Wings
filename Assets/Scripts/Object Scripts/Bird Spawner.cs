using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;

    private int birdCountMinimum = 5;
    private int birdCountMaximum = 10;
    
    private Collider2D collider2D;
    
    private List<Vector3> birdLocations = new List<Vector3>();
    
    private float minX, minY, maxX, maxY;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        
        minX = collider2D.bounds.min.x;
        maxX = collider2D.bounds.max.x;
        minY = collider2D.bounds.min.y;
        maxY = collider2D.bounds.max.y;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("Nest").buildIndex)
        {
            SpawnBirds();
        }
    }

    private void SpawnBirds()
    {
        int randomBirdCount = Random.Range(birdCountMinimum, birdCountMaximum);
        int currentCount = 0;
        
        while(currentCount < randomBirdCount)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 randomVector = new Vector3(randomX, randomY, 0);
            bool isNearingExisitingBird = false;

            foreach (Vector3 birdLocation in birdLocations)
            {
                float distance = Vector3.Distance(randomVector, birdLocation);

                if (distance < 10)
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
