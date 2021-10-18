using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static string playerName;
    private static int scope = 1;
    private static int time = 1;
    private static int money = 1;
    public static int points;
    public static List<Employee> team = new List<Employee>();
    public static Sprite icon;
    public static int combatPower;
    public static bool entusiasm;
    public static bool disciplin;
    public static bool organized;

    public static int preventCorrect;
    public static int risksActivated;
    public static int opportunitiesTaken;

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

    public static void SetEmployees(Employee employee)
    {
        team.Add(employee);
    }

    public static void IncreaseResources(int value)
    {
        scope += value;
        money += value;
        time  += value;
    }

    public static void DecreaseResources(int value)
    {
        scope -= value;
        money -= value;
        time  -= value;
    }

    public static void OperateScope(int value)
    {
        scope += value;
    }

    public static void OperateMoney(int value)
    {
         money += value;
    }

    public static void OperateTime(int value)
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