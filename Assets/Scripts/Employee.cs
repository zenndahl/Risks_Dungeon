using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Employee", menuName = "Risks Dungeon/Employee", order = 0)]
public class Employee : ScriptableObject
{
    [Header("Employee Infos")]
    public Sprite sprite;
    public Skill[] skills;

    [Header("Employee Stats")]
    public int morale;

    public void ManageCombatPower(int modifier)
    {
        //combatPower += modifier;
    }

    public void ManageMorale(int penalty)
    {
        //morale -= penalty;
    }
}
