using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI communismText;
    [SerializeField] private TextMeshProUGUI followersText;
    
    private CommunismSystem communismSystem;

    private void Start()
    {
        communismSystem = FindObjectOfType<CommunismSystem>();
    }

    void Update()
    {
        UpdateCommunismText();
        UpdateFollowersText();
    }

    private void UpdateCommunismText()
    {
        communismText.text = AbbreviateNumber(communismSystem.GetCommunism());
    }
    private void UpdateFollowersText()
    {
        followersText.text = AbbreviateNumber(communismSystem.GetFollowers());
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
}
