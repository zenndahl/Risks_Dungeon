using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [Header("UI")]
    public GameObject chooseProject;


    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Quit(){
        Application.Quit();
    }

    //Main Menu Methods
    public void StartGame(){
        Debug.Log("Iniciando Jogo");
        ActivateChooseProjectUI();
    }

    public void Scoreboard(){

    }

    public void Options(){

    }

    //Choose Project Methods
    public void ActivateChooseProjectUI(){
        chooseProject.SetActive(true);
    }
}
