using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Add : MonoBehaviour
{
    public static GameObject selected;
    public static GameObject matrixCell;
    //public GameObject list;
    public GameObject toAddList;
    public GameObject describer;

    public void AddEmployee()
    {
        if(selected != null)
        {
            TeamSelection ts = GameObject.Find("Team Selection").GetComponent<TeamSelection>();
            if(describer != null) describer.SetActive(false); //hide the description of the selected object

            //cloning the display object selected to the selected objects list
            Transform cloned = Instantiate(selected.transform, selected.transform);
            cloned.SetParent(toAddList.transform);

            //get employee from display
            Employee employee = selected.GetComponent<EmployeeDisplay>().employee;

            //set the employees available and the employees the player already picked
            ts.SetEmployeeLists(employee);
            GameObject.Find("Player").GetComponent<Player>().SetEmployees(employee);

            //randomize new employees for the player to choose
            ts.RandomizeSkills();
        }
        selected = null;
    }

    public void AddRisk()
    {
        if(selected != null)
        {
            if(describer != null) describer.SetActive(false); //hide the description of the selected object
            Identification id = GameObject.Find("Identification").GetComponent<Identification>();
            id.AddId(selected.GetComponent<RiskDisplay>().risk);
            selected.transform.SetParent(toAddList.transform);

            toAddList.GetComponent<RectTransform>().offsetMin -= new Vector2(0,20);
        }
        selected = null;
    }

    public void RemoveRisk()
    {
        if(selected != null)
        {
            if(describer != null) describer.SetActive(false); //hide the description of the selected object
            Identification id = GameObject.Find("Identification").GetComponent<Identification>();
            id.RemoveId(selected.GetComponent<RiskDisplay>().risk);
            selected.transform.SetParent(toAddList.transform);

            toAddList.GetComponent<RectTransform>().offsetMin += new Vector2(0,20);
        }
        selected = null;
    }

    public static void AddToMatrix()
    {
        Risk risk = selected.GetComponent<RiskDisplay>().risk;
        if(risk != null)
        {
            RiskDisplay riskDisplay = matrixCell.GetComponent<RiskDisplay>();
            riskDisplay.DisplayInMatrix(risk);
            Evaluation eval = GameObject.Find("Evaluation").GetComponent<Evaluation>();
            eval.Evaluate(risk, riskDisplay);
            Destroy(selected);
            selected = null;
            matrixCell = null;
        }
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

            GameObject.Find("Team Selection").GetComponent<TeamSelection>().FinishIconSelection();
        }
        selected = null;
    }

    public void AddPrevention()
    {
        Transform holder = GameObject.Find("PreventionHolder").transform;
        if(holder.childCount != 0) 
        {
            //holder.GetChild(0).GetComponent<Button>().interactable = true;
            holder.GetChild(0).transform.SetParent(toAddList.transform);
            holder.GetChild(0).GetComponent<Button>().onClick.RemoveListener(AddPrevention);
        }

        selected.transform.SetParent(gameObject.transform);
        selected.GetComponent<Button>().onClick.AddListener(AddPrevention);
    }

}
