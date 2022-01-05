using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prevention", menuName = "Risks Dungeon/Prevention", order = 0)]
public class Prevention : ScriptableObject
{
    public string preventionName;
    public string description;

    public void Prevent()
    {
        GameManager.Instance.preventionsMade.Add(this);
    }
}
