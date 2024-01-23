using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FameSystem : MonoBehaviour
{
    private int _fame = 1;
    
    public int fameDecreaseMinimum = 1;
    public int fameDecreaseMaximum = 5;
    
    public int timeToWait = 60;
    
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(DecreaseFame());
        StartCoroutine(FameCheck());
    }

    private IEnumerator DecreaseFame()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToWait);
            _fame -= UnityEngine.Random.Range(fameDecreaseMinimum, fameDecreaseMaximum);
        }
    }

    public IEnumerator FameCheck()
    {
        while (true)
        {
            if (_fame <= 0)
                gameManager.GameOver("I see you're not a social butterfly");
            yield return new WaitForSeconds(1f);
        }
    }

    public void GainFame(int amountToIncrease)
    {
        if(_fame < 100)
            _fame += amountToIncrease;
    }
    
    public int GetFame()
    {
        return _fame;
    }
}
