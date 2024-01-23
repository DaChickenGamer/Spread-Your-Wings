using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubbleChecker : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("IntrusiveThought"))
            Debug.Log(other.gameObject.name);
    }
}
