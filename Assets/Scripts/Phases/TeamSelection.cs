using System;
using System.Collections;
using Random=UnityEngine.Random;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelection : MonoBehaviour
{
    public GameObject iconSelectionScreen;
    public EmployeeDisplay[] displays;
    private List<Employee> employees = new List<Employee>();
    private int maxRange = 12;

    private void Start()
    {
        employees = GameManager.employees;
        RandomizeSkills();
    }

    public void RandomizeSkills() //fazer remoção de skills já escolhidas
    {
        List<int> randomList = new List<int>();
        //get the employee display objects under the selection parent and randomize the employees displayed
        foreach (EmployeeDisplay ed in displays)
        {
            int randNum = Random.Range(0,maxRange);
            
            while(randomList.Contains(randNum))
    	        randNum = Random.Range(0,maxRange);
            randomList.Add(randNum);

            ed.employee = employees[randNum];
            
            //need to reset the display for the new employee to be shown in the display
            ed.ResetInfos();
        }
    }

    public void SetEmployeeLists(Employee employee)
    {
        employees.Remove(employee);
        if(Player.team.Count() == 4) FinishTeamSelection();
        maxRange--;
    }

    public void FinishTeamSelection()
    {
        //gameObject.SetActive(false);
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        iconSelectionScreen.SetActive(true);

        foreach (Employee employee in Player.team)
        {
            //activate the employee skills
            employee.skill.ActivateSkill();
            employee.Subscribe();
        }
    }

    public void FinishIconSelection()
    {
        iconSelectionScreen.transform.GetChild(1).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(2).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(4).gameObject.SetActive(false);
    }
}
