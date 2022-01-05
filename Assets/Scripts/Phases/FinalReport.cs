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
    private Player player;

    void Start()
    {
        player = Player.PlayerInstance;
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
        int scope = player.GetResource("scope");
        int money = player.GetResource("money");
        int time = player.GetResource("time");

        player.points = scope + ((money + time)/2);

        float percent = (player.preventCorrect * 100)/GameManager.Instance.risks.Count;

        //LeaderboardController.SubmitScore();
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        if(points != null) points.text = player.points.ToString();
        risksPrevented.text = percent.ToString() + "%";
        risksActivated.text = player.risksActivated.ToString();
        opportunitiesTaken.text = player.opportunitiesTaken.ToString();
    }
}
