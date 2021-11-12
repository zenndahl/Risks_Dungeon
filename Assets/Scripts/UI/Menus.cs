using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [Header("UI")]
    public GameObject chooseProject;
    public GameObject difficultyUI;
    public GameObject warningScreen;
    public GameObject scoreBoard;
    public GameObject creditsScreen;
    public GameObject projects;
    public GameObject chooseName;
    public InputField playerName;
    private bool gameIsPaused = false;

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            //toggle on/off the pause menu in scenes that you can pause
            if(GameObject.Find("Pause Menu"))
            {
                gameIsPaused = !gameIsPaused;
                PauseGame();
            }
        }
    }

    public void LoadMainMenu()
    {
        Destroy(GameObject.Find("Game Manager"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("Beholder"));
        SceneManager.LoadScene("Menu Principal");
    }

    public void Quit()
    {
        //GameObject.Find("GameManager").GetComponent<ScoreManager>().SaveScore();
        Application.Quit();
    }

    public void QuitMenu()
    {
        warningScreen.SetActive(true);
    }

    public void ExitQuitMenu()
    {
        warningScreen.SetActive(false);
    }

    bool TogglePause()
    {
        if(gameIsPaused) return false;
        else return true;
    }

    public void PauseGame()
    {
        if(gameIsPaused)
        {
            DisableInteractbles();
            GameObject.Find("Pause Menu").transform.SetAsLastSibling();
        }
        else 
        {
            EnableInteractbles();
            GameObject.Find("Pause Menu").transform.SetSiblingIndex(0);
        }
    }

    public void Continue()
    {
        EnableInteractbles();
        gameIsPaused = TogglePause();
        GameObject.Find("Pause Menu").transform.SetSiblingIndex(0);
    }

    //Main Menu Methods
    public void StartGame()
    {
        ActivateChooseProjectUI();
        creditsScreen.SetActive(false);
        //scoreBoard.SetActive(false);
    }
 
    // public void SetPlayerName()
    // {
    //     Player.playerName = playerName.text;
    // }

    // public void Scoreboard()
    // {
    //     scoreBoard.SetActive(true);
    //     difficultyUI.SetActive(false);
    //     chooseProject.SetActive(false);
    // }

    public void Options()
    {
        //TODO
    }

    public void Credits()
    {
        chooseProject.SetActive(false);
        difficultyUI.SetActive(false);
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
    public void ActivateChooseProjectUI()
    {
        chooseProject.SetActive(true);
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

    public static void GameOver()
    {
        
        GameObject.Find("Risk Game Display").GetComponent<RiskGameDisplay>().DismissRiskDisplay();
        GameObject.Find("Opportunity Display").GetComponent<OpportunityDisplay>().ResetDisplay();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Phase"))
        {
            obj.SetActive(false);
        }
        GameObject.Find("Game Over").transform.SetAsLastSibling();
        GameObject.Find("Game Over").GetComponent<FinalReport>().DisplayFinalReport();
    }
}
