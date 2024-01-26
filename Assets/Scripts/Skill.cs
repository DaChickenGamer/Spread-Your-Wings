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
        TitleText.text = $"{skillTree.SkillNames[id]}/{skillTree.SkillCaps[id]}\n\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}\nCost: {skillTree.skillTreeComunismCost} Communism"; // Add cost system later

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow : //Checks if maxed
            skillTree.skillTreeComunismCost > 0 ? Color.green : Color.white; // Checks if can be bought

        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        if (skillTree.skillTreeComunismCost < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        
        skillTree.skillTreeComunismCost -= 1; // Change to cost later
        skillTree.SkillLevels[id] += 1;
        skillTree.UpdateAllSkillUI();
    }
}
