using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Infos")]
    public int project;
    public string projectName;

    [Header("Risks Infos")]
    public Risk[] generalRisks;
    public Risk[] risksIdentified; //lembrar de fazer a união desses riscos no array abaixo
    //public Risk[] risksList;

    [Header("Opportunities")]
    public Opportunity opportunitiesList;

    [Header("Scene Infos")]
    public string currentScene;
    public string nextScene;
    public int sceneNum;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update(){
        if(currentScene == "Selecionar Equipe")
        {

        }
    }

    public void ERPProject()
    {
        project = 1;
        projectName = "Desenvolvimento de Sistema ERP";
        currentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    public void AppProject()
    {
        project = 2;
        projectName = "Desenvolvimento de App";
        currentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    public void LoadNextScene()
    {
        if(currentScene == "Selecionar Equipe") SceneManager.LoadScene("Identificação");
        else
        {
            if(project == 2) SceneManager.LoadScene("SCRUM");
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
