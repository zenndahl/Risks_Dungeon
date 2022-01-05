using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //public static string playerName;
    private static Player _playerInstance;
    public static Player PlayerInstance
    {
        get
        {
            return _playerInstance;
        }
    }
    [SerializeField] private int scope;
    [SerializeField] private int time ;
    [SerializeField] private int money;
    public int points;
    public List<Employee> team = new List<Employee>();
    public Sprite icon;
    public int combatPower = 1;
    public bool entusiasm;
    public bool disciplin;
    public bool organized;
    public int preventCorrect;
    public int risksActivated;
    public int opportunitiesTaken;

    private void Awake()
    {
        _playerInstance = this;
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

    void ResetVariables()
    {
        scope = 10;
        money = 10;
        time = 10;
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