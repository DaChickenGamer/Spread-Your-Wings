using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    private CommunismSystem communismSystem;
    private void Start()
    {
        communismSystem = GameObject.FindObjectOfType<CommunismSystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("IntrustiveThought"))
        {
            communismSystem.totalIntrusiveThoughtsDestroyed += 1;
            Destroy(other.gameObject);
        }
    }
}
