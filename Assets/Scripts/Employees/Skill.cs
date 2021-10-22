using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Risks Dungeon/Skill", order = 0)]
public class Skill : ScriptableObject
{
    public int id;
    public string skillName;
    public string skillDescription;
    public List<Risk> combat = new List<Risk>();

    public void ActivateSkill()
    {
        if(skillName == "Dedicação")
        {
            Player.combatPower++;
        }
        if(skillName == "Inovação")
        {
            foreach (Opportunity opportunity in GameManager.opportunities)
            {
                opportunity.Inovate();
            }
        }
        if(skillName == "Organização")
        {
            Player.organized = true;
        }
        if(skillName == "Disciplina")
        {
            Player.disciplin = true;
        }

        if(skillName == "Entusiasmo")
        {
            Player.entusiasm = true;
        }
    }
}
