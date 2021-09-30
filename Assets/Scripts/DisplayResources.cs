using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayResources : MonoBehaviour
{
    public Text value;
    public string resource;
    // Start is called before the first frame update
    void Start()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        value.text = player.GetResource(resource).ToString();
    }
}
