using System.Collections;
using System.Collections.Generic;
//using System.Serializable;
using UnityEngine;

[System.Serializable]
public class Score : MonoBehaviour
{
    public string playerName;
    public float score;

    public Score(string name, float score)
    {
        this.name = name;
        this.score = score;
    }
}
