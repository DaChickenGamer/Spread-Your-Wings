using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillCategory
{
    Communism,
    Combat,
    Survivability
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillStats : ScriptableObject
{
    public SkillCategory skillCategory;
    public int SkillID = 0;

    public int SkillConnections;

    public string SkillName;
    public string SkillDescription;
    
    public int SkillCurrentLevel;
    public int SkillLevelCap;
    public int SkillCost;
}


