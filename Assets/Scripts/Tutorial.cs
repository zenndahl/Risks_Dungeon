using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadNextTutorial()
    {
        //remove last tutorial
        parts[0].SetActive(false);
        parts.RemoveAt(0);
        
        if(parts.Any()) parts[0].SetActive(true); //show next tutorial if there is any
        else
        {
            //if there are no more tutorials, hide the tutorials game object and proceed to the phase gameplay
            gameObject.GetComponent<Menus>().HideUI();
        }

        
    }

}
