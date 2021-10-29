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
        DisplayFinalReport();
    }

    public void SubmitScore()
    {
        // GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore(new Score(Player.playerName, Player.points));
        // GameObject.Find("GameManager").GetComponent<ScoreManager>().SaveScore();
        //HighScores.UploadScore(Player.playerName, Player.points);
    }

    public void DisplayFinalReport()
    {
        int scope = GameObject.Find("Player").GetComponent<Player>().GetResource("scope");
        int money = GameObject.Find("Player").GetComponent<Player>().GetResource("money");
        int time = GameObject.Find("Player").GetComponent<Player>().GetResource("time");

        Player.points = scope + ((money + time)/2);

        float percent = (Player.preventCorrect * 100)/GameManager.risks.Count;

        //LeaderboardController.SubmitScore();
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        if(points != null) points.text = Player.points.ToString();
        risksPrevented.text = percent.ToString() + "%";
        risksActivated.text = Player.risksActivated.ToString();
        opportunitiesTaken.text = Player.opportunitiesTaken.ToString();
    }
}
