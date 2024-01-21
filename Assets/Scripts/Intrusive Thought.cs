using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        if (brain == null)
        {
            Destroy(gameObject);
            yield break;
        }

        for (int i = 0; i < 1000; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, brain.transform.position, 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
