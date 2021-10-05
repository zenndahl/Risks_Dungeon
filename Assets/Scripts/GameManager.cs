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
    public static GameManager Instance { get { return _instance; } }

    [Header("Game Infos")]
    public int project;
    public static string projectName; 

    [Header("Risks Infos")]
    public List<Risk> risks = new List<Risk>(); 
    //risks identified by the player in the identification phase
    public List<Risk> risksIdentified = new List<Risk>();
    public Risk activeRisk;
    public static List<Prevention> preventionsMade = new List<Prevention>();
    public static List<Risk> risksAssigned = new List<Risk>();

    [Header("Opportunities")]
    public Opportunity[] opportunitiesList;
    public static Opportunity[] opportunities;

    [Header("Scene Infos")]
    public static string currentScene;
    public static string nextScene;
    public static int sceneNum = 1;

    [Header("Select Team Infos")]
    public List<Employee> employeesList = new List<Employee>();
    public static List<Employee> employees = new List<Employee>();

    [Header("Rooms Infos")]
    public Room currentRoom;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        } 
        else 
        {
            _instance = this;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        opportunities = opportunitiesList;
        employees = employeesList;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
    
    //will be called by the button on the main menu screen to setup the ERP project
    public void ERPProject()
    {
        project = 1;
        projectName = "Desenvolvimento de Sistema ERP";
        currentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    //will be called by the button on the main menu screen to setup the App project
    public void AppProject()
    {
        project = 2;
        projectName = "Desenvolvimento de App";
        currentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    //script to load the scenes in order
    public void LoadNextScene()
    {
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
                        nextScene = null;
                        break;

                    default:
                        SceneManager.LoadScene("Main Menu");
                        currentScene = "Main Menu";
                        nextScene = "Implementação";
                        //tratar possíveis erros(?)
                        Debug.Log("ERRO!! VOCÊ FOI RETORNADO AO MENU!!");
                        break;
                }
                sceneNum++;
            }
        }
    }
}
