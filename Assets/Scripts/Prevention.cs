using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prevention", menuName = "Risks Dungeon/Prevention", order = 0)]
public class Prevention : ScriptableObject
{
    public string description;
    public Risk[] prevent;

    public void Prevent()
    {
        foreach (Risk risk in prevent)
        {
            risk.probability -= 0.2f;
            risk.timesPrevented++;
        }
    }
}
