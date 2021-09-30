using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [Header("UI")]
    public GameObject choseProject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Debug.Log("Saindo do Jogo");
        Application.Quit();
    }

    //Main Menu Methods
    public void StartGame()
    {
        Debug.Log("Iniciando Jogo");
        ActivateChoseProjectUI();
    }

    public void Scoreboard()
    {

    }

    public void Options()
    {

    }

    //Choose Project Methods
    public void ActivateChoseProjectUI()
    {
        choseProject.SetActive(true);
    }

    public void Select(){
        Add.selected = this.gameObject;
    }

    public void NextLevel()
    {
        GameManager._instance.LoadNextScene();
    }

    public void Reshuffle()
    {
        GameManager._instance.RandomizeSkills();
    }

    public void FinishId()
    {
        GameManager._instance.FinishIdentification();
    }
}
