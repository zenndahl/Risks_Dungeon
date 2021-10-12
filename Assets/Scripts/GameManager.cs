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

    [Header("Risks Infos")]
    public List<Risk> project1Risks = new List<Risk>();
    public List<Risk> project2Risks = new List<Risk>();
    public static List<Risk> risks = new List<Risk>();
    public static List<Risk> risksIdentified = new List<Risk>();
    public static List<Prevention> preventionsMade = new List<Prevention>();
    public static List<Risk> risksAssigned = new List<Risk>();
    public static List<Risk> risksCorrectlyIdentified = new List<Risk>();
    public static List<Risk> risksCorrectlyEvaluated = new List<Risk>();
    public static List<Risk> risksCorrectlyPlanned = new List<Risk>();


    [Header("Opportunities")]
    public List<Opportunity> opportunitiesList = new List<Opportunity>();
    public static List<Opportunity> opportunities = new List<Opportunity>();

    [Header("Scene Infos")]
    public static string currentScene;
    public static string nextScene;
    public static int sceneNum = 1;

    [Header("Select Team Infos")]
    public List<Employee> employeesList = new List<Employee>();
    public static List<Employee> employees = new List<Employee>();

    [Header("Rooms Infos")]
    public static Room currentRoom;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // if (_instance != null && _instance != this)
        // {
        //     Destroy(this);
        // } 
        // else 
        // {
        //     _instance = this;
        // }
    }

    void Start()
    {
        currentScene = "Menu Principal";
        SceneManager.sceneLoaded += OnSceneLoaded;
        opportunities = opportunitiesList;
        employees = employeesList;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameObject.Find("Token/Icon"))
        {
            GameObject.Find("Token/Icon").GetComponent<Image>().sprite = Player.icon;
            GameObject.Find("Room 0").GetComponent<Room>().SetRooms();
        }
    }
    
    //will be called by the button on the main menu screen to setup the ERP project
    public void ERPProject()
    {
        project = 1;
        projectName = "Desenvolvimento de Sistema ERP";
        gameObject.GetComponent<Menus>().choseName.SetActive(true);
        gameObject.GetComponent<Menus>().projects.SetActive(false);
        //currentScene = "Selecionar Equipe";
        //SceneManager.LoadScene("Selecionar Equipe");
    }

    //will be called by the button on the main menu screen to setup the App project
    public void AppProject()
    {
        project = 2;
        projectName = "Desenvolvimento de App";
        gameObject.GetComponent<Menus>().choseName.SetActive(true);
        gameObject.GetComponent<Menus>().projects.SetActive(false);
        //nextScene = "Selecionar Equipe";
        //SceneManager.LoadScene("Selecionar Equipe");
    }

    public List<Employee> GetEmployeesList()
    {
        return employeesList;
    }

    public List<Risk> GetProject1Risks()
    {
        return project1Risks;
    }

    public List<Risk> GetProject2Risks()
    {
        return project2Risks;
    }

    //script to load the scenes in order
    public static void LoadNextScene()
    {
        if(currentScene == "Menu Principal")
        {
            currentScene = "Selecionar Equipe";
            nextScene = "Identificação";
            SceneManager.LoadScene("Selecionar Equipe");
        }
        if(currentScene == "Selecionar Equipe") 
        {
            currentScene = "Identificação";
            nextScene = "Avaliação";
            SceneManager.LoadScene("Identificação");
        }
        else if(currentScene == "Identificação")
        {
            currentScene = "Avaliação";
            SceneManager.LoadScene("Avaliação");
        }
        else if(currentScene == "Avaliação")
        {
            currentScene = "Planejamento";
            SceneManager.LoadScene("Planejamento");
        }
        else
        {
            if(project == 2)
            {
                currentScene = "SCRUM";
                SceneManager.LoadScene("SCRUM");
                nextScene = null;
            } 
            else
            {
                switch (sceneNum)
                {
                    case 1:
                        SceneManager.LoadScene("Requisitos");
                        currentScene = "Requisitos";
                        nextScene = "Implementação";
                        break;
                    case 2:
                        SceneManager.LoadScene("Implementação");
                        currentScene = "Implementação";
                        nextScene = "VV";
                        break;
                    case 3:
                        SceneManager.LoadScene("VV");
                        currentScene = "VV";
                        nextScene = "Evolução";
                        break;
                    case 4:
                        SceneManager.LoadScene("Evolução");
                        currentScene = "Evolução";
                        nextScene = "Final";
                        break;

                    default:
                        SceneManager.LoadScene("Menu Principal");
                        currentScene = "Menu Principal";
                        //tratar possíveis erros(?)
                        Debug.Log("ERRO!! VOCÊ FOI RETORNADO AO MENU!!");
                        break;
                }
                sceneNum++;
            }
        }
    }
}
