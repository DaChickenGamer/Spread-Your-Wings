using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Slider slider;
    private TextMeshProUGUI progressText;
    
    public static UIManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one UI Manager in the scene.");
        }

        instance = this;
        
        progressText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetMaxProgress(int progress)
    {
        slider.maxValue = progress;
    }
    
    public void SetProgress(int progress, string percentage)
    {
        slider.value = progress;
        progressText.text = percentage + "%";
    }
}
