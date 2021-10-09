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
        Player player = GameObject.Find("Player").GetComponent<Player>();
        risksPrevented.text = GameManager.preventionsMade.Count.ToString();
        risksActivated.text = player.risksActivated.ToString();
        opportunitiesTaken.text = player.opportunitiesTaken.ToString();
    }
}
