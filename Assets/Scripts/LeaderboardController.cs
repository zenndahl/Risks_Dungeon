using UnityEngine.UI;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardController : MonoBehaviour
{
    public InputField playerScore;
    public GameObject inputField;
    public static int id = 595;
    int MaxScores = 10;
    public Text[] entries;

    private void Start()
    {
        LootLockerSDKManager.StartSession(Player.playerName, (response) => 
        {
            if(response.success)
            {
                Debug.Log("Sucesso");
            }
            else
            {
                {
                    Debug.Log("Falhou");
                }
            }
        });    
    }

    public void GetPlayerName()
    {
        Player.playerName = inputField.GetComponent<Text>().text;
    }

    public static void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(Player.playerName, int.Parse(Player.points.ToString()), id, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Sucesso");
            }
            else
            {
                {
                    Debug.Log("Falhou");
                }
            }
        });
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(id, MaxScores, (response) => 
        {
            if(response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for(int i = 0; i< scores.Length; i++)
                {
                    entries[i].text = (scores[i].rank + "..." + scores[i].score);
                }

                if(scores.Length < MaxScores)
                {
                    for(int i = scores.Length; i <MaxScores; i++)
                    {
                        entries[i].text = (i + 1).ToString() + ".   none";
                    }
                }
            }
            else
            {
                {
                    Debug.Log("Falhou");
                }
            }
        });
    }
}
