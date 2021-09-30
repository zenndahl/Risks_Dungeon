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
    public GameObject riskDisplayPrefab;
    public static List<Risk> risks = new List<Risk>(); 
    //risks identified by the player in the identification phase
    public static List<Risk> risksIdentified = new List<Risk>(); 

    [Header("Opportunities")]
    public Opportunity[] opportunitiesList;
    public static Opportunity[] opportunities;

    [Header("Scene Infos")]
    public static string currentScene;
    public static string nextScene;
    public static int sceneNum = 1;

    [Header("Select Team Scene")]
    public List<Employee> employeesList = new List<Employee>();
    public static List<Employee> employees = new List<Employee>();
    public EmployeeDisplay[] displays;

    // [Header("Sprites Infos")]
    // public Sprite[] icons;

    private int maxRange = 12;
    private int correctlyId = 0;
    private int correctlyEvaluated = 0;
    private int closelyEvaluated = 0;
    private GameObject feedbackScreen;
    private GameObject iconSelectionScreen;

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

    void Update()
    {

    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //when the scene "selecionar equipe" is loaded, the employees displayed will be randomized
        if(scene.name == "Selecionar Equipe")
        {
            displays = FindObjectsOfType<EmployeeDisplay>();
            RandomizeSkills();

            //getting the icon selection screen for latter and hiding it
            iconSelectionScreen = GameObject.Find("Icon Selection");
            iconSelectionScreen.SetActive(false);
        }

        //when the scene "identificação" or "avaliação" loads, get the feedback UI and hides it
        if(scene.name == "Identificação" || scene.name == "Avaliação")
        {
            //activate the feedback screen
            feedbackScreen = GameObject.Find("Feedback");
            feedbackScreen.SetActive(false);

            if(scene.name == "Avaliação")
            {
                SetUpEvaluation();
            }
        }
    }

    public void RandomizeSkills() //fazer remoção de skills já escolhidas
    {
        List<int> randomList = new List<int>(); 
        //get the employee display objects under the selection parent and randomize the employees displayed
        foreach (EmployeeDisplay ed in displays)
        {
            int randNum = Random.Range(0,maxRange);
            
            while(randomList.Contains(randNum))
    	        randNum = Random.Range(0,maxRange);
            randomList.Add(randNum);

            ed.employee = employeesList[randNum];

            //fazer checagem se tem membros repetidos e permitir novo sorteio
            
            //need to reset the display for the new employee to be shown in the display
            ed.ResetInfos();
        }
    }

    public void SetEmployeeLists(Employee employee)
    {
        employeesList.Remove(employee);
        if(Player.team.Count() == 4) FinishTeamSelection();
        maxRange--;
    }

    public void FinishTeamSelection()
    {
        GameObject.Find("Team Selection").SetActive(false);
        iconSelectionScreen.SetActive(true);
    }

    public void FinishIconSelection()
    {
        iconSelectionScreen.transform.GetChild(1).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(2).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void SetRisksList(Risk risk)
    {
        risksIdentified.Add(risk);

        Player player = GameObject.Find("Player").GetComponent<Player>();

        //check if the risk identified is general or for the selected project and adds the resources if it is
        if(risk.project == 0 || risk.project == project)
        {
            player.IncreaseResources(1);
            correctlyId++;
        }
        else //if the risk is identified incorrectly decrease the player resources
        {
            player.DecreaseResources(1);
        }
    }

    public void FinishIdentification()
    {
        //hide the risks lists
        GameObject.Find("Identification").SetActive(false);

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyId);

        risks = risksIdentified;
        
    }

    void SetUpEvaluation()
    {
        GameObject rskList = GameObject.Find("Risk List/Risks");
        foreach (Risk rsk in risksIdentified)
        {
            //instantiating and setting the parent for the risk
            GameObject rskDisplay = Instantiate(riskDisplayPrefab, rskList.transform);

            //set spacing between risks
            rskList.GetComponent<VerticalLayoutGroup>().spacing += 0.5f;

            //rskDisplay.transform.SetParent(rskList.transform);
            rskDisplay.GetComponent<RiskDisplay>().risk = rsk;

            //adding button listeners events by calling method inside RiskDisplay
            rskDisplay.GetComponent<RiskDisplay>().SetEvents(rskDisplay);
        }
    }

    public void Evaluation(Risk risk, RiskDisplay riskDisplay)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        GameObject rskList = GameObject.Find("Risks");

        if(risk.impactLevel == riskDisplay.impact && risk.probLevel == riskDisplay.prob)
        {
            player.IncreaseResources(3);
            correctlyEvaluated++;
            Debug.Log("Pontos ganhos: " + 3);
        }
        else if(risk.impactLevel - riskDisplay.impact < 2)
        {
            player.IncreaseResources(1);
            closelyEvaluated++;
            Debug.Log("Pontos ganhos: " + 1);
        } 
        else if(risk.probLevel - riskDisplay.prob < 2)
        {
            player.IncreaseResources(1);
            closelyEvaluated++;
            Debug.Log("Pontos ganhos: " + 1);
        }

        risks.Remove(risk);
        if(!risks.Any()) FinishEvaluation();
        
    }

    void FinishEvaluation()
    {
        //hide the risks lists
        GameObject.Find("Evaluation").SetActive(false);

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyEvaluated, closelyEvaluated);
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
