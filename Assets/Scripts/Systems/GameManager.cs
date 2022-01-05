using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get{
            return _instance;
        }
    }

    [Header("Game Infos")]
    public int project;
    public int difficulty;

    [Header("Risks Infos")]
    public List<Risk> allRisks = new List<Risk>();
    public List<Risk> project1Risks = new List<Risk>();
    public List<Risk> project2Risks = new List<Risk>();
    public List<Risk> risks = new List<Risk>();
    public List<Risk> risksAux = new List<Risk>();
    public List<Risk> risksIdentified = new List<Risk>();
    public List<Risk> risksAssigned = new List<Risk>();
    // public static List<Risk> risksCorrectlyIdentified = new List<Risk>();
    // public static List<Risk> risksCorrectlyEvaluated = new List<Risk>();
    // public static List<Risk> risksCorrectlyPlanned = new List<Risk>();

    [Header("Preventions Infos")]
    public List<Prevention> preventionsList = new List<Prevention>();
    //public static List<Prevention> preventions = new List<Prevention>();
    public List<Prevention> preventionsMade = new List<Prevention>();

    [Header("Opportunities Infos")]
    public List<Opportunity> opportunitiesList = new List<Opportunity>();
    public List<Opportunity> opportunitiesAux = new List<Opportunity>();

    [Header("Select Team Infos")]
    public List<Employee> employeesList = new List<Employee>();
    public List<Employee> employeesAux = new List<Employee>();

    [Header("Scene Infos")]
    public string currentScene;
    public string nextScene;

    [Header("Rooms Infos")]
    public Room currentRoom;
    public int risksInSequence;

    private Player player;


    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        player = Player.PlayerInstance;
        //HighScores.UploadScore("Gian", 100);
        currentScene = "Menu Principal";
        nextScene = "Selecionar Equipe";
        SceneManager.sceneLoaded += OnSceneLoaded;
        opportunitiesAux = opportunitiesList;
        employeesAux = employeesList;
        //preventions = preventionsList;
        preventionsMade.Clear();
        risksIdentified.Clear();
        risksAssigned.Clear();
        risksAux.Clear();
        risks.Clear();
        risksInSequence = 0;
        ResetScripts();
    }

    //reset the scripts that gets modified through the game
    void ResetScripts()
    {
        //reset risks
        foreach (Risk risk in allRisks)
        {
            risk.probability = risk.baseProbability;
            risk.reaction = 0;
            risk.prevented = false;
            risk.timesHappened = 0;
        }

        //reset opportunities bonuses
        foreach (Opportunity opportunity in opportunitiesAux)
        {
            opportunity.scopeBonus = opportunity.baseScopeBonus;
            opportunity.moneyBonus = opportunity.baseMoneyBonus;
            opportunity.timeBonus = opportunity.baseTimeBonus;
        }

        foreach (Employee emp in employeesAux)
        {
            emp.morale = 0.5f;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameObject.Find("Token/Icon"))
        {
            GameObject.Find("Token/Icon").GetComponent<Image>().sprite = player.icon;
            GameObject.Find("Room 0").GetComponent<Room>().SetRooms();
        }

        //when a new scene is loaded, set the next and reset the current
        SetScenes(scene);

        //set initial resources based on the difficulty
        if(scene.name == "Selecionar Equipe")
        {
            if(difficulty == 1)
            {
                player.OperateScope(10);
                player.OperateMoney(10);
                player.OperateTime(10);
            }
            if(difficulty == 2)
            {
                player.OperateScope(20);
                player.OperateMoney(20);
                player.OperateTime(20);
            }
            if(difficulty == 3)
            {
                player.OperateScope(30);
                player.OperateMoney(30);
                player.OperateTime(30);
            }
        }
    }
    
    //will be called by the button on the main menu screen to setup the ERP project
    public void ERPProject()
    {
        project = 1;
        // gameObject.GetComponent<Menus>().choseName.SetActive(true);
        // gameObject.GetComponent<Menus>().projects.SetActive(false);
        risks = project1Risks.ToList();
        GameObject.Find("Choose Project").SetActive(false);
        gameObject.GetComponent<Menus>().difficultyUI.SetActive(true);
    }

    //will be called by the button on the main menu screen to setup the App project
    public void AppProject()
    {
        project = 2;
        // gameObject.GetComponent<Menus>().choseName.SetActive(true);
        // gameObject.GetComponent<Menus>().projects.SetActive(false);
        risks = project2Risks.ToList();
        GameObject.Find("Choose Project").SetActive(false);
        gameObject.GetComponent<Menus>().difficultyUI.SetActive(true);
    }

    public void Hard()
    {
        difficulty = 1;
        LoadNextScene();
    }

    public void Normal()
    {
        difficulty = 2;
        LoadNextScene();
    }

    public void Easy()
    {
        difficulty = 3;
        LoadNextScene();
    }

    public List<Employee> GetEmployeesList()
    {
        return employeesList;
    }

    public List<Risk> GetAllRisks()
    {
        return allRisks;
    }

    public List<Risk> GetProject1Risks()
    {
        return project1Risks;
    }

    public List<Risk> GetProject2Risks()
    {
        return project2Risks;
    }

    void SetScenes(Scene scene)
    {
        //set the current scene
        currentScene = scene.name;

        //setting the next scenes to be loaded
        if(scene.name == "Menu Principal") nextScene = "Selecionar Equipe";
        if(scene.name == "Selecionar Equipe") nextScene = "Identificação";
        if(scene.name == "Identificação") nextScene = "Avaliação";
        if(scene.name == "Avaliação") nextScene = "Planejamento";
        if(scene.name == "Planejamento")
        {
            //the last phase is loaded based on the project type
            if(project == 2) nextScene = "SCRUM";
            else nextScene = "Requisitos";
        }
        if(scene.name == "Requisitos") nextScene = "Implementação";
        if(scene.name == "Implementação") nextScene = "VV";
        if(scene.name == "VV") nextScene = "Evolução";
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
