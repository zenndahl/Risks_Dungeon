using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Risk", menuName = "Risks Dungeon/Risk", order = 0)]
public class Risk : ScriptableObject 
{
    [Header("Risk Infos")]
    public int id;
    public int type;
    public int project;
    public string riskName;
    public string riskDescription;
    //[HideInInspector]
    public int impactLevel;
    public int probLevel;

    [Header("Risk Probability")]
    public float probability;
    public float reincidence;

    [Header("Risk Effect")]
    public int scopeCost;
    public int moneyCost;
    public int timeCost;
    
    public Consequence[] consequences;
    public int timesPrevented;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        //text.text = riskName;
    }
}
