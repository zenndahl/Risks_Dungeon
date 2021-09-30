using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Add : MonoBehaviour
{
    public static GameObject selected;
    public static GameObject matrixCell;
    public GameObject toAddList;
    public GameObject describer;

    public void AddEmployee()
    {
        if(selected != null)
        {
            if(describer != null) describer.SetActive(false); //hide the description of the selected object

            //cloning the display object selected to the selected objects list
            Transform cloned = Instantiate(selected.transform, selected.transform);
            cloned.SetParent(toAddList.transform);

            //get employee from display
            Employee employee = selected.GetComponent<EmployeeDisplay>().employee;

            //set the employees available and the employees the player already picked
            GameManager._instance.SetEmployeeLists(employee);
            GameObject.Find("Player").GetComponent<Player>().SetEmployees(employee);

            //randomize new employees for the player to choose
            GameManager._instance.RandomizeSkills();
        }
        selected = null;
    }

    public void AddRisk()
    {
        if(selected != null)
        {
            if(describer != null) describer.SetActive(false); //hide the description of the selected object
            GameManager._instance.SetRisksList(selected.GetComponent<RiskDisplay>().risk);
            selected.transform.SetParent(toAddList.transform);
        }
        selected = null;
    }

    public static void AddToMatrix()
    {
        Risk risk = selected.GetComponent<RiskDisplay>().risk;
        RiskDisplay riskDisplay = matrixCell.GetComponent<RiskDisplay>();
        riskDisplay.DisplayInMatrix(risk);
        GameManager._instance.Evaluation(risk, riskDisplay);
        Destroy(selected);
        selected = null;
        matrixCell = null;
    }

    public void AddIcon()
    {
        if(selected != null)
        {
            if(describer != null) describer.SetActive(true); //show the description plate
            Sprite icon = selected.transform.GetComponent<Image>().sprite;
            GameObject.Find("Player").GetComponent<Player>().icon = icon;

            //disable the button
            selected.GetComponent<Button>().enabled = false;
            //set describer plate to show the icon
            selected.transform.SetParent(describer.transform);

            //Destroy(selected);

            GameManager._instance.FinishIconSelection();
        }
        selected = null;
    }

}
