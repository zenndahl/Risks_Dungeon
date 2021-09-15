using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add : MonoBehaviour
{
    public static GameObject selected;
    public GameObject toAddList;
    public GameObject describer;

    public void AddRisk(){
        selected.transform.SetParent(toAddList.transform);
        if(describer != null) describer.SetActive(false);
    }
}
