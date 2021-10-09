using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayResources : MonoBehaviour
{
    public Text scopeValue;
    public Text moneyValue;
    public Text timeValue;

    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        scopeValue.text = player.GetResource("scope").ToString();
        moneyValue.text = player.GetResource("money").ToString();
        timeValue.text = player.GetResource("time").ToString();
    }
}
