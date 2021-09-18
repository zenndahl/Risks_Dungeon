using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Risks Dungeon/Player", order = 0)]
public class Player : ScriptableObject
{
    public static int scope;
    public static int time;
    public static int money;
    public Skill skill;
    public Employee[] team;

    // Update is called once per frame
    void Update()
    {
        if(scope <= 0 || time <= 0 || money <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}