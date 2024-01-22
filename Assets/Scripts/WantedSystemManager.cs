using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WantedSystemManager : MonoBehaviour
{
    public int percentToStarIncreaseMinimum;
    public int percentToStarIncreaseMaximum;
    
    public int firstWaveMinimumEnemyCount = 1;
    public int firstWaveMaximumEnemyCount = 5;

    public float enemySpawnIncreaseMinimum = 1.25;
    public float enemySpawnIncreaseMaximum = 2;

    private int currentWave = 1;

    public int enemiesKilled = 0;
    
    private int percentToStar;

    [SerializeField] private List<GameObject> antiCommunistList;

    public void WantedLevelCalculation()
    {
        int percentToStarIncreaseAmount = Random.Range(percentToStarIncreaseMinimum, percentToStarIncreaseMaximum);
        
        percentToStar += percentToStarIncreaseAmount;

        if (percentToStar >= 100)
        {
            StartHunt();
        }
    }

    public int CurrentEnemyCount;
    private void StartHunt()
    {
        int randomAntiCommunistCount = Random.Range(firstWaveMinimumEnemyCount, firstWaveMaximumEnemyCount);

        CurrentEnemyCount = randomAntiCommunistCount;
        
        for (int currentEnemyCount = 0; currentEnemyCount < randomAntiCommunistCount; currentEnemyCount++)
        {
            int randomAntiCommunist = Random.Range(0, antiCommunistList.Count);

            float randomX = InclusiveRandom(1.5f, 2.5f);
            float randomY = InclusiveRandom(1.5f, 2.5f);
            
            Vector2 outsideCameraSpawnpoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0f));
            
            Instantiate(antiCommunistList[randomAntiCommunist], outsideCameraSpawnpoint, Quaternion.identity);
        }
    }

    public void CheckEnemiesKilled()
    {
        if (enemiesKilled >= CurrentEnemyCount)
        {
            WaveIncrease(CurrentEnemyCount);
        }
    }
    private int WaveIncrease(int currentEnemyCount)
    {
        currentWave += 1;
        enemiesKilled = 0;
        
        float randomIncreaseAmount = Random.Range(enemySpawnIncreaseMinimum, enemySpawnIncreaseMaximum);

        return currentEnemyCount * (int) Mathf.Ceil(randomIncreaseAmount);
    }
    
    private float InclusiveRandom(float min, float max)
    {
        float random = Random.Range(min, max);
        float negativeRandom = Random.Range(-min, -max) + 1;
        
        int randomPick = Random.Range(0, 2);
        
        if (randomPick == 0)
        {
            return random;
        }
        else
        {
            return negativeRandom;
        }
    }
}
