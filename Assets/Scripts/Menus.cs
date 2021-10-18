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
    public GameObject scoreBoard;
    public GameObject creditsScreen;
    public GameObject projects;
    public GameObject choseName;
    public InputField playerName;
    private bool isPauseMenuOn = false;

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            //toggle on/off the pause menu in scenes that you can pause
            if(GameObject.Find("Pause Menu"))
            {
                isPauseMenuOn = !isPauseMenuOn;
                if(isPauseMenuOn) Pause();
                else Continue();
            }
        }
    }

    public void LoadMainMenu()
    {
        //GameManager.currentScene = "Menu Principal";
        SceneManager.LoadScene("Menu Principal");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        DisableInteractbles();
        GameObject.Find("Pause Menu").transform.SetAsLastSibling();
    }

    public void Continue()
    {
        EnableInteractbles();
        isPauseMenuOn = false;
        GameObject.Find("Pause Menu").transform.SetSiblingIndex(0);
    }

    //Main Menu Methods
    public void StartGame()
    {
        ActivateChoseProjectUI();
        creditsScreen.SetActive(false);
        scoreBoard.SetActive(false);
    }
 
    public void SetPlayerName()
    {
        Player.playerName = playerName.text;
    }

    public void Scoreboard()
    {
        scoreBoard.SetActive(true);
        choseProject.SetActive(false);
    }

    public void Options()
    {
        //TODO
    }

    public void Credits()
    {
        creditsScreen.SetActive(true);
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
        scoreBoard.SetActive(false);
    }

    public void Select()
    {
        Add.selected = this.gameObject;
    }

    public void NextLevel()
    {
        GameManager.LoadNextScene();
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
        GameManager.nextScene = "Final";
        GameManager.LoadNextScene();
    }

    public void GameOver()
    {
        GameObject.FindGameObjectsWithTag("Phase")[0].SetActive(false);
        GameObject.Find("Game Over").transform.SetAsLastSibling();
        GameObject.Find("Game Over").GetComponent<FinalReport>().DisplayFinalReport();
    }
}
