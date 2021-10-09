using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int scope = 1;
    private int time = 1;
    private int money = 1;
    public static List<Employee> team = new List<Employee>();
    public Sprite icon;
    public int combatPower;

    public int risksActivated;
    public int opportunitiesTaken;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(scope <= 0 || time <= 0 || money <= 0)
        {
            //Debug.Log("Game Over");
        }
    }

    public void SetEmployees(Employee employee)
    {
        team.Add(employee);
    }

    public void IncreaseResources(int value)
    {
        scope += value;
        money += value;
        time  += value;
    }

    public void DecreaseResources(int value)
    {
        scope -= value;
        money -= value;
        time  -= value;
    }

    public void OperateScope(int value)
    {
        scope += value;
    }

    public void OperateMoney(int value)
    {
         money += value;
    }

    public void OperateTime(int value)
    {
        time += value;
    }

    public int GetResource(string resource)
    {
        if(resource == "scope") return scope;
        else if(resource == "money") return money;
        else if(resource == "time") return time;
        else return 0;
    }
}