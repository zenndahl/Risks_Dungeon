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
    public int[] risksIdentified;

    [Header("Scene Infos")]
    public string presentScene;
    public string nextScene;
    public int sceneNum;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneNum = 0;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void ERPProject()
    {
        project = 1;
        projectName = "Desenvolvimento de Sistema ERP";
        presentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    public void AppProject()
    {
        project = 2;
        projectName = "Desenvolvimento de App";
        presentScene = "Selecionar Equipe";
        SceneManager.LoadScene("Selecionar Equipe");
    }

    public void LoadNextScene()
    {
        if(presentScene == "Selecionar Equipe") SceneManager.LoadScene("Identificação");
        else
        {
            if(project == 2) SceneManager.LoadScene("SCRUM");
            else
            {
                switch (sceneNum)
                {
                    case 1:
                        SceneManager.LoadScene("Requisitos");
                        presentScene = "Requisitos";
                        nextScene = "Implementação";
                        break;
                    case 2:
                        SceneManager.LoadScene("Implementação");
                        presentScene = "Implementação";
                        nextScene = "VV";
                        break;
                    case 3:
                        SceneManager.LoadScene("VV");
                        presentScene = "VV";
                        nextScene = "Evolução";
                        break;
                    case 4:
                        SceneManager.LoadScene("Evolução");
                        presentScene = "Evolução";
                        nextScene = null;
                        break;

                    default:
                        SceneManager.LoadScene("Main Menu");
                        presentScene = "Main Menu";
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
