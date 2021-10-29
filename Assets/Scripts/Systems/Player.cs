using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static string playerName;
    [SerializeField] private static int scope;
    [SerializeField] private static int time ;
    [SerializeField] private static int money;
    public static int points;
    public static List<Employee> team = new List<Employee>();
    public static Sprite icon;
    public static int combatPower = 1;
    public static bool entusiasm;
    public static bool disciplin;
    public static bool organized;

    public static int preventCorrect;
    public static int risksActivated;
    public static int opportunitiesTaken;

    private void Awake()
    {
        ResetVariables();
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if(scope <= 0 || time <= 0 || money <= 0)
        {
            Menus.GameOver();
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

    void ResetVariables()
    {
        scope = 1000;
        money = 1000;
        time = 1000;
        combatPower = 1;
        entusiasm = false;
        disciplin = false;
        organized = false;
        preventCorrect = 0;
        risksActivated = 0;
        opportunitiesTaken = 0;
        team.Clear();
        points = 0;
    }
}