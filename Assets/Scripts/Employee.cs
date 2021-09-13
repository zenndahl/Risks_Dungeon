using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    public Skill[] skills;
    public int morale;
    public int combatPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void ManageCombatPower(int modifier){
        combatPower += modifier;
    }

    public void ManageMorale(int penalty){
        morale -= penalty;
    }
}
