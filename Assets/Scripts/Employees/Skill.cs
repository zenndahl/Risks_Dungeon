using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Risks Dungeon/Skill", order = 0)]
public class Skill : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public List<Risk> combat = new List<Risk>();

    public void ActivateSkill()
    {
        if(skillName == "Dedicação")
        {
            Player.PlayerInstance.combatPower++;
        }
        if(skillName == "Inovação")
        {
            foreach (Opportunity opportunity in GameManager.Instance.opportunitiesAux)
            {
                opportunity.Inovate();
            }
        }
        if(skillName == "Organização")
        {
            Player.PlayerInstance.organized = true;
        }
        if(skillName == "Disciplina")
        {
            Player.PlayerInstance.disciplin = true;
        }

        if(skillName == "Entusiasmo")
        {
            Player.PlayerInstance.entusiasm = true;
        }
    }
}
