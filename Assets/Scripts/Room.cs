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
    public bool isPlayerHere = false;

    private Color allowed = new Color(102, 231, 99, 200);
    //private Color notAllowed = new Color(251, 60, 63, 200);

    // Start is called before the first frame update
    void Start()
    {
        SetRisksProb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasPlayer()
    {
        return isPlayerHere;
    }

    public void ChangeHoverColor(int op)
    {
        gameObject.GetComponent<Image>().color = allowed;
    }

    public void SetButton(Button button, int op)
    {
        Debug.Log(button);
        Debug.Log(op);
        if(op == 1) button.onClick.AddListener(MoveToken);
        else button.onClick.RemoveAllListeners();  
    }

    //will calculate probabilities and decide if a risk will happen
    public void SetRisksProb()
    {
        foreach (Risk risk in risksPossible)
        {
            
            if(GameManager.preventionsMade.Intersect<Prevention>(risk.preventions) != null)
            {
                risk.probability -= 0.2f;
                risk.probLevel -= 1;
                Debug.Log(risk.riskName);
            }
        }
    }

    public void MoveToken()
    {
        //set the previous room to not have the player
        GameObject.Find("Token").transform.parent.GetComponent<Room>().isPlayerHere = false;
        //move the token next to the selected room and set to have the player
        isPlayerHere = true;
        GameObject.Find("Token").transform.SetParent(gameObject.transform);
        GameObject.Find("Token").transform.position = new Vector3(gameObject.transform.position.x,
                                                                  gameObject.transform.position.y,
                                                                  gameObject.transform.position.z    
                                                            );
        SetRooms();
        RskOpp();                                      
    }

    public void SetRooms()
    {
        GameObject rooms = GameObject.Find("Rooms");

        foreach (Transform child in rooms.transform)
        {
            child.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            child.gameObject.GetComponent<Button>().enabled = false;
        }

        foreach (Button button in nextRooms)
        {
            button.GetComponent<Room>().ChangeHoverColor(0);
            button.enabled = true;
        }

    }

    public void RskOpp()
    {
        Risk rsk = GameManager._instance.activeRisk;
        if(rsk != null) rsk.ActivateRisk();

        foreach (Risk risk in risksPossible)
        {
            float rand = Random.Range(0f,1f);
            if(rand <= risk.probability)
            {
                GameManager._instance.activeRisk = risk;
                break;
            }
            
        }
    }
}
