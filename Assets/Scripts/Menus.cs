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
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu Principal");
        GameManager.currentScene = "Menu Principal";
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

    public void HideUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void ShowUI(GameObject ui)
    {
        ui.SetActive(true);
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

    public static void EnableInteractbles()
    {
        GameObject[] interactbles = GameObject.FindGameObjectsWithTag("Interactble");
        foreach (GameObject gO in interactbles)
        {
            gO.GetComponent<Button>().enabled = true;
        }
    }

    public static void DisableInteractbles()
    {
        GameObject[] interactbles = GameObject.FindGameObjectsWithTag("Interactble");
        foreach (GameObject gO in interactbles)
        {
            gO.GetComponent<Button>().enabled = false;
        }
    }

    public void EndGame()
    {
        
    }

    public void GameOver()
    {
        
    }
}
