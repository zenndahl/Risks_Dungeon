using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add : MonoBehaviour
{
    public static GameObject riskSelected;
    public GameObject identifiedList;
    public GameObject describer;

    public void AddRisk(){
        riskSelected.transform.SetParent(identifiedList.transform);
        describer.SetActive(false);
    }
}
