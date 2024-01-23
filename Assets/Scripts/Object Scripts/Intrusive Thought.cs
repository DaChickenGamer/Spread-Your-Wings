using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class IntrusiveThought : MonoBehaviour
{
    private GameObject brain;
    private void Start()
    {
        brain = GameObject.Find("Brain");
        StartCoroutine(GoToBrain());
        
    }
    
    private IEnumerator GoToBrain()
    {
        Random random = new Random();

        if (brain == null)
        {
            Destroy(gameObject);
            yield break;
        }

        for (int i = 0; i < 1000; i++)
        {
            float randomNum = random.Next(1, 5);
            transform.position = Vector3.MoveTowards(transform.position, brain.transform.position,randomNum/100);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
