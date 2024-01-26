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

    public GameObject timerPanel;
    
    [SerializeField] private TextMeshProUGUI communismText;
    [SerializeField] private TextMeshProUGUI followersText;
    
    public static UIManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one UI Manager in the scene.");
        }

        instance = this;
        
        progressText = GetComponentInChildren<TextMeshProUGUI>(); }

    private void Start()
    {
        StartCoroutine(GameTimer());
    }
    
    void Update()
    {
        UpdateCommunismText();
        UpdateFollowersText();
    }

    private void UpdateCommunismText()
    {
        communismText.text = AbbreviateNumber(CommunismSystem.instance.GetCommunism());
    }
    private void UpdateFollowersText()
    {
        followersText.text = AbbreviateNumber(CommunismSystem.instance.GetFollowers());
    }

    private string AbbreviateNumber(int numberToAbbreviate)
    {
        string[] suffixes = { "", "K", "M", "B", "T"};
        int index = 0;

        while (numberToAbbreviate >= 1000 && index < suffixes.Length - 1)
        {
            numberToAbbreviate /= 1000;
            index++;
        }

        if (index > 0)
        {
            return numberToAbbreviate.ToString() + suffixes[index];
        }
        else
        {
            return numberToAbbreviate.ToString();
        }
    }

    public IEnumerator GameTimer()
    {
        TextMeshProUGUI timerText = timerPanel.GetComponentInChildren<TextMeshProUGUI>();

        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        
        bool timeRunning = true;
        
        while (timeRunning)
        {
            seconds++;
            
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes >= 60)
            {
                hours++;
                minutes = 0;
            }

            timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            
            yield return new WaitForSeconds(1);
        }
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
