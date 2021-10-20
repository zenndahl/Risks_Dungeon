using System;
using Random=UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Risk[] risksPossible;
    public Button[] nextRooms;
    public bool explored = false;
    public bool isPlayerHere = false;
    public bool isLast;
    public bool isCheckpoint;
    public bool isBreakpoint;
    private int roomCost = 2;
    public GameObject feedback;

    private Color allowed = new Color(102, 231, 99, 200);
    //private Color notAllowed = new Color(251, 60, 63, 200);

    public void ChangeHoverColor(int op)
    {
        gameObject.GetComponent<Image>().color = allowed;
    }

    //add or remove the event MoveToken based on the neighborhood of the room the player is in
    public void SetButton(Button button, int op)
    {
        if(op == 1) button.onClick.AddListener(MoveToken);
        else button.onClick.RemoveAllListeners();  
    }

    //to get to a room, a random value between 1 and 4 will be charged from the player resources
    void MoveCost()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();

        for(int i = 0; i < 3; i++)
        {
            //there will be a random value for each resource individually
            // int rand = Random.Range(1,4);
            // //if the player has the skill "Entusiasmo" the cost is decreased to a minimum of 1
            if(Player.entusiasm && !explored)
            {

                // rand--;
                // if(rand <= 0) rand++;
                if(i == 0) Player.OperateScope(-(roomCost-1));
                if(i == 1) Player.OperateMoney(-(roomCost-1));
                if(i == 2) Player.OperateTime(-(roomCost-1));
            }
            else
            {
                if(i == 0) Player.OperateScope(-roomCost);
                if(i == 1) Player.OperateMoney(-roomCost);
                if(i == 2) Player.OperateTime(-roomCost);
            }

        }
    }

    public void MoveToken()
    {
        MoveCost();

        if(isCheckpoint) SCRUM.sprintLoops++;
        if(isBreakpoint)
        {
            SCRUM.sprintLoops = 0;
            SCRUM.sprints++;
            GameObject rooms = GameObject.Find("Rooms");

            //reset the explored rooms from the previous sprint
            foreach (Transform child in rooms.transform)
            {
                child.GetComponent<Room>().explored = false;
            }
        }

        //if a risk was randomized last room, when it enters a new room, it is activated
        GameManager.currentRoom = this;

        explored = true;

        //set the previous room to not have the player
        GameObject.Find("Token").transform.parent.GetComponent<Room>().isPlayerHere = false;
        //move the token next to the selected room and set to have the player
        isPlayerHere = true;
        GameObject.Find("Token").transform.SetParent(gameObject.transform);
        GameObject.Find("Token").transform.position = new Vector3(gameObject.transform.position.x,
                                                                  gameObject.transform.position.y,
                                                                  gameObject.transform.position.z    
                                                                );

        if(!isLast)
        {
            SetRooms();
            RskOpp();
        }

        if(isLast) 
        {
            feedback.SetActive(true);
            feedback.GetComponent<Feedback>().PhaseFeedback();
        }
    }

    public void SetRooms()
    {
        GameObject rooms = GameObject.Find("Rooms");

        //first disable every room button
        foreach (Transform child in rooms.transform)
        {
            child.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            child.gameObject.GetComponent<Button>().interactable = false;
        }

        foreach (Button button in nextRooms)
        {
            button.GetComponent<Room>().ChangeHoverColor(0);
            button.interactable = true;
            //the scrum project has its own settings
            if(GameManager.project == 2)
            {
                SCRUM.SetSprintRooms(this);
            }
        }
    }

    public void RskOpp()
    {
        //will randomly chose one of the risks possible
        int randIndex = Random.Range(0,risksPossible.Length);

        //will draw the probability
        float randProb = Random.Range(0f,1f);

        //if the probability draw is less than the probability of the risk to happen, it happens
        if(randProb <= risksPossible[randIndex].probability)
        {
            risksPossible[randIndex].ActivateRisk();
        }
        else
        {
            Room.DrawOpportunity();
        }
    }

    public static void DrawOpportunity()
    {
        //will randomly chose one of the opportunities
            int randOpp = Random.Range(0,GameManager.opportunities.Count);
            GameObject oppDisplay = GameObject.Find("Opportunity Display");
            oppDisplay.GetComponent<OpportunityDisplay>().opportunity = GameManager.opportunities[randOpp];
            oppDisplay.GetComponent<OpportunityDisplay>().ShowDescription();
    }
}
