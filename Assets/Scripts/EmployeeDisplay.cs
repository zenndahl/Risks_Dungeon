using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EmployeeDisplay : MonoBehaviour
{
    public Employee employee;

    public Text skillNameText;

    public Text descriptionText;

    public GameObject describer;

    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        skillNameText.text = employee.skill.skillName;
        image.GetComponent<Image>().sprite = employee.sprite;
    }

    public void ShowDescription()
    {
        describer.SetActive(true);
        descriptionText.text = employee.skill.skillDescription;
    }

    public void Select()
    {
        Add.selected = this.gameObject;
    }

    public void ResetInfos()
    {
        skillNameText.text = employee.skill.skillName;
        image.GetComponent<Image>().sprite = employee.sprite;
    }
}
