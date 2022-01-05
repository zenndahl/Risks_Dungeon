using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Identification : MonoBehaviour
{
    public GameObject feedbackScreen;
    public GameObject riskDisplayPrefab;
    private int correctlyId = 0;
    private Player player;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.PlayerInstance;
        SetRisksToId();
    }

    public void SetRisksToId()
    {
        foreach (Risk risk in gameManager.GetAllRisks())
        {
            //instantiating and setting the parent for the risk
            GameObject rskDisplay = Instantiate(riskDisplayPrefab, transform.position, transform.rotation);
            GameObject rskList = GameObject.Find("Risk List/Risks");
            rskDisplay.transform.SetParent(rskList.transform);

            //set spacing between risks
            //rskList.GetComponent<RectTransform>().offsetMin -= new Vector2(0,100);

            //assigning the risk to the display
            rskDisplay.GetComponent<RiskDisplay>().risk = risk;

            //adding button listeners events by calling method inside RiskDisplay
            rskDisplay.GetComponent<RiskDisplay>().SetEvents(rskDisplay);
        }
    }

    public void AddId(Risk risk)
    {
        gameManager.risksIdentified.Add(risk);
    }

    public void RemoveId(Risk risk)
    {
        gameManager.risksIdentified.Remove(risk);
    }

    public void FinishIdentification()
    {
        //hide the risks lists
        GameObject.Find("Identification").SetActive(false);
        //Player player = GameObject.Find("Player").GetComponent<Player>();

        foreach (Risk risk in gameManager.risksIdentified)
        {
            //check if the risk identified is general or for the selected project and adds the resources if it is
            if(risk.project == 0 || risk.project == gameManager.project)
            {
                player.IncreaseResources(5);
                //gameManager.risksCorrectlyIdentified.Add(risk);
                correctlyId++;
            }
            else //if the risk is identified incorrectly decrease the player resources
            {
                player.DecreaseResources(1);
            }
        }

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyId);

        gameManager.risksAux = gameManager.risksIdentified.ToList();
        
    }
}
