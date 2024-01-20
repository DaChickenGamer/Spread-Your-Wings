using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    private CommunismSystem communismSystem;
    private void Start()
    { 
        communismSystem = FindObjectOfType<CommunismSystem>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (!other.gameObject.CompareTag("IntrusiveThought")) return;
        communismSystem.chanceToSucceed -= (100 / (communismSystem.totalIntrusiveThoughts * .25f));
        communismSystem.totalIntrusiveThoughtsDestroyed += 1;
        communismSystem.totalIntrusiveThoughtsLeft -= 1;
        Destroy(other.gameObject);
    }
}
