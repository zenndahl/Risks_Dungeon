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
    public int combatPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManageCombatPower(int modifier)
    {
        combatPower += modifier;
    }

    public void ManageMorale(int penalty)
    {
        morale -= penalty;
    }
}
