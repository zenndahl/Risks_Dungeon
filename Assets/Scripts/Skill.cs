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
    public List<Risk> prevent = new List<Risk>();

    public void PreventRisks(){
        foreach (Risk risk in prevent)
        {
            risk.DecreaseProb(1);
        }
    }
}
