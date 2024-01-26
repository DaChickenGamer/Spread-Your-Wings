using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Test Loop")]
    [field: SerializeField] public EventReference testLoop { get; private set; }
    
    [field: Header("Egg Hatching SFX")] 
    [field: SerializeField] public EventReference eggHatching { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
