using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }
    
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference nestMusic {get; private set; }
    [field: SerializeField] public EventReference menuMusic {get; private set; }
    
    [field: Header("Player SFX")]
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
