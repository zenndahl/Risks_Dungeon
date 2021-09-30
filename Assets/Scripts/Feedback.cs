using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public Text feedbackText_1;
    public Text feedbackText_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayFeedback(string phaseText, int correctsNumber, int closeNumbers = 0)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if(feedbackText_1 != null)
        {
            feedbackText_1.text = "Você " + phaseText + " " + correctsNumber + " riscos corretamente";
        }

        if(closeNumbers != 0)
        {
            feedbackText_1.text += " e " + closeNumbers + " próximos do correto";
        }
        
        feedbackText_2.text = "Seus recursos:" + 
                                            "\nEscopo: " + player.GetResource("scope").ToString() +
                                            "\n Orçamento: " + player.GetResource("money").ToString() +
                                            "\n Tempo: " + player.GetResource("time").ToString();
    }
}
