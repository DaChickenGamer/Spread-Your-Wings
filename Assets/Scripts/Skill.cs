using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}\nCost: {skillTree.SkillCost[id]} Communism"; // Add cost system later

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow : //Checks if maxed
            CommunismSystem.instance.GetCommunism() > skillTree.SkillCost[id] ? Color.green : new Color(149, 149, 149); // Checks if can be bought

        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        if (CommunismSystem.instance.GetCommunism() < skillTree.SkillCost[id] || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        
        CommunismSystem.instance.RemoveCommunism(skillTree.SkillCost[id]);
        skillTree.skillTreeComunismCost -= 1; // Change to cost later
        skillTree.SkillLevels[id] += 1;
        skillTree.UpdateAllSkillUI();
    }
}
