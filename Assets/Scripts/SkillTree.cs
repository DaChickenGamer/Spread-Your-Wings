using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillTree : MonoBehaviour
{
    
    /*
     * MANUALLY HAVE TO CONNECT SKILLS IN THE INSPECTOR
     * NEED LOTS OF TRIG ELSEWHERE TO MAKE IT WORK
     */
    public static SkillTree skillTree { get; private set; }

    public int[] SkillLevels;
    public int[] SkillCaps;
    public int[] SkillCost;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder; // Maybe make it a scriptable object

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;
    
    [FormerlySerializedAs("SkillPoints")] public double skillTreeComunismCost; // Probably won't need | This is just communism change it later
    // Add cost of skill later
    
    private void Awake()
    {
        if (skillTree != null)
        {
            Debug.LogError("Found more than one Skill Tree in the scene.");
        }
        
        skillTree = this;
    }
    private void Start()
    {
        skillTreeComunismCost = 20;

        SkillLevels = new int[6];
        // Maybe make it into a SO and load it from there later
        SkillCaps = new[] {1, 5, 5, 2, 10, 10};

        SkillCost = new[] { 20, 10, 5, 20, 10, 5 };
            
        SkillNames = new[] {"Strength", "Intelligence", "Charisma", "Luck", "Speed", "Endurance"};
        
        SkillDescriptions = new[]
        {
            "Strength increases your damage and health.",
            "Intelligence increases your damage and health.",
            "Charisma increases your damage and health.",
            "Luck increases your damage and health.",
            "Speed increases your damage and health.",
            "Endurance increases your damage and health."
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);
        
        for( var i=0; i < SkillLevels.Length; i++) SkillList[i].id = i;
        
        // Add the skill than the ones it goes to
        SkillList[4].ConnectedSkills = new[] {1, 3, 0}; 
        SkillList[0].ConnectedSkills = new[] {2, 5};
        
        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }
}
