using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;

    private void Start()
    {
        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rankUI.text = (i+1).ToString();
            row.playerNameUI.text = scores[i].playerName;
            row.scoreUI.text = scores[i].score.ToString();
        }
    }
}
