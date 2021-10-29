using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PreventionDisplay : MonoBehaviour
{
    public Prevention prevention;
    public Text nameText;
    public Text descriptionText;

    private void Start()
    {
        SetInfos();
    }

    public void ShowDescription()
    {
        GameObject.Find("Describer/Text").GetComponent<Text>().text = prevention.description;
    }

    public void Select()
    {
        Add.selected = this.gameObject;
    }

    public void SetInfos()
    {
        if(prevention != null) nameText.text = prevention.preventionName;
    }

    // public void ResetInfos()
    // {
    //     nameText.text = prevention.preventionName;
    // }
}
