using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public static GameManager _instance;
    // public static GameManager Instance { get { return _instance; } }

    [Header("Game Infos")]
    public static int project;
    public static string projectName;
    public static int difficulty;

    [Header("Risks Infos")]
    public List<Risk> allRisks = new List<Risk>();
    public List<Risk> project1Risks = new List<Risk>();
    public List<Risk> project2Risks = new List<Risk>();
    public static List<Risk> risks = new List<Risk>();
    public static List<Risk> risksAux = new List<Risk>();
    public static List<Risk> risksIdentified = new List<Risk>();
    public static List<Risk> risksAssigned = new List<Risk>();
    public static List<Risk> risksCorrectlyIdentified = new List<Risk>();
    public static List<Risk> risksCorrectlyEvaluated = new List<Risk>();
    public static List<Risk> risksCorrectlyPlanned = new List<Risk>();

    [Header("Preventions Infos")]
    public static List<Prevention> preventionsMade = new List<Prevention>();
    public List<Prevention> preventionsList = new List<Prevention>();
    public static List<Prevention> preventions = new List<Prevention>();

    [Header("Opportunities Infos")]
    public List<Opportunity> opportunitiesList = new List<Opportunity>();
    public static List<Opportunity> opportunities = new List<Opportunity>();
    public static List<Opportunity> opportunitiesPossible = new List<Opportunity>();

    [Header("Scene Infos")]
    public static string currentScene;
    public static string nextScene;

    [Header("Select Team Infos")]
    public List<Employee> employeesList = new List<Employee>();
    public static List<Employee> employees = new List<Employee>();

    [Header("Rooms Infos")]
    public static Room currentRoom;
    public static int risksInSequence;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //HighScores.UploadScore("Gian", 100);
        currentScene = "Menu Principal";
        nextScene = "Selecionar Equipe";
        SceneManager.sceneLoaded += OnSceneLoaded;
        opportunities = opportunitiesList;
        opportunitiesPossible = opportunitiesList;
        employees = employeesList;
        preventions = preventionsList;
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
        foreach (Opportunity opportunity in opportunities)
        {
            opportunity.scopeBonus = opportunity.baseScopeBonus;
            opportunity.moneyBonus = opportunity.baseMoneyBonus;
            opportunity.timeBonus = opportunity.baseTimeBonus;
        }

        foreach (Employee emp in employees)
        {
            emp.morale = 0.5f;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameObject.Find("Token/Icon"))
        {
            GameObject.Find("Token/Icon").GetComponent<Image>().sprite = Player.icon;
            GameObject.Find("Room 0").GetComponent<Room>().SetRooms();
        }

        //when a new scene is loaded, set the next and reset the current
        SetScenes(scene);
    }
    
    //will be called by the button on the main menu screen to setup the ERP project
    public void ERPProject()
    {
        project = 1;
        projectName = "Implantação de Sistema ERP";
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
        projectName = "Desenvolvimento de App";
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

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
