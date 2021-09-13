using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prevention : MonoBehaviour
{
    public string description;
    public Risk[] prevent;

    public void Prevent(){
        foreach (Risk risk in prevent)
        {
            risk.probability -= 1;
        }
    }
}
