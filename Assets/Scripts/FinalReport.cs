using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalReport : MonoBehaviour
{
    public Text points;
    public Text risksPrevented;
    public Text risksActivated;
    public Text opportunitiesTaken;

    void Start()
    {
        int scope = GameObject.Find("Player").GetComponent<Player>().GetResource("scope");
        int money = GameObject.Find("Player").GetComponent<Player>().GetResource("money");
        int time = GameObject.Find("Player").GetComponent<Player>().GetResource("time");

        Player.points = scope + ((money + time)/2);

        LeaderboardController.SubmitScore();
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        points.text = Player.points.ToString();
        risksPrevented.text = GameManager.preventionsMade.Count.ToString();
        risksActivated.text = Player.risksActivated.ToString();
        opportunitiesTaken.text = Player.opportunitiesTaken.ToString();
    }
}
