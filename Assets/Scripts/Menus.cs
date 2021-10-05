using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [Header("UI")]
    public GameObject choseProject;
    public GameObject warningScreen;


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
        Application.Quit();
    }

    //Main Menu Methods
    public void StartGame()
    {
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
        //GameManager._instance.RandomizeSkills();
    }

    public void FinishId()
    {
        GameObject.Find("Identification").GetComponent<Identification>().FinishIdentification();
    }

    public void PlanningWarningDismiss()
    {
        warningScreen.SetActive(false);
    }

    public void EndGame()
    {
        
    }
}
