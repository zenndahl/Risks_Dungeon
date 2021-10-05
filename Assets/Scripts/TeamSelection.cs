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
    
    private int maxRange = 12;

    private void Start()
    {
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

            ed.employee = GameManager._instance.employeesList[randNum];

            //fazer checagem se tem membros repetidos e permitir novo sorteio
            
            //need to reset the display for the new employee to be shown in the display
            ed.ResetInfos();
        }
    }

    public void SetEmployeeLists(Employee employee)
    {
        GameManager._instance.employeesList.Remove(employee);
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
    }

    public void FinishIconSelection()
    {
        iconSelectionScreen.transform.GetChild(1).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(2).gameObject.SetActive(false);
        iconSelectionScreen.transform.GetChild(4).gameObject.SetActive(false);
    }
}
