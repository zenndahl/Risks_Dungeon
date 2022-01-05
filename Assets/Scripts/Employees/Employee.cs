using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Employee", menuName = "Risks Dungeon/Employee", order = 0)]
public class Employee : ScriptableObject
{
    [Header("Employee Infos")]
    public Sprite sprite;
    public Skill skill;

    [Header("Employee Stats")]
    public float morale = 0.5f;

    private Player player;

    private void Start()
    {
        //Risk.OnActivateRisk += MoraleCheck;  
        player = Player.PlayerInstance; 
    }

    public void Subscribe()
    {
        Risk.OnActivateRisk += MoraleCheck;
    }

    void MoraleCheck(Risk risk)
    {
        //if the risk was prevented, the morale increases, if not, it decreases
        if(risk.prevented) ManageMorale(0.1f);
        else ManageMorale(-0.1f);

        //if the risk is a team risk and the player have the skill "Liderança", the morale increseas, otherwise decreases
        if(risk.riskClass == "Equipe" && player.team.Find(x => x.skill.skillName == "Liderança"))
        {
            ManageMorale(0.05f);
        }
        else /*if(risk.riskClass == "Equipe" && !Player.team.Find(x => x.skill.skillName == "Liderança"))*/
        {
            ManageMorale(-0.05f);
        }

        //if the employee combat the risk, its morale increases
        if(skill.combat.Contains(risk)) ManageMorale(0.1f);

        //if the player encountered 4 or more risks in sequence, the morale decreases
        if(GameManager.Instance.risksInSequence >= 4) ManageMorale(-0.1f);

        //OnMoraleChange(this);
        SetTeamMorale();
    }


    void SetTeamMorale()
    {
        GameObject.Find("Beholder").GetComponent<TeamAgent>().SetTeamMorales(this);
    }

    public void ManageMorale(float mod)
    {
        morale += mod;
    }
}
