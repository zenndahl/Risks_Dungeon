using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SprintsDisplay : MonoBehaviour
{
    public Text sprintDisplay;
    public Text loopsDisplay;

    public void ShowSprints()
    {
        sprintDisplay.text = SCRUM.sprints.ToString();
        loopsDisplay.text = SCRUM.sprintLoops.ToString();
    }

    private void Update() 
    {
        ShowSprints();
    }
}
