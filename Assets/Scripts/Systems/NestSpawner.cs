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

    [SerializeField] private Collider2D spawnNestCollider2D;
    
    private int birdCountMinimum = 2;
    private int birdCountMaximum = 5;
    
    private List<Vector3> birdLocations = new List<Vector3>();
    

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("Nest").buildIndex)
        {
            SpawnBirds(spawnNestCollider2D);
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
