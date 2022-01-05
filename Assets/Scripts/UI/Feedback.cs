using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public Text feedbackText_1;
    public Text feedbackText_2;

    void DisplayResources()
    {
        Player player = Player.PlayerInstance;
        feedbackText_2.text = "Seus recursos:" + 
                                            "\nEscopo: " + player.GetResource("scope").ToString() +
                                            "\n Orçamento: " + player.GetResource("money").ToString() +
                                            "\n Cronograma: " + player.GetResource("time").ToString();
    }

    public void DisplayFeedback(string phaseText, int correctsNumber, int closeNumbers = 0, int assigned = 0)
    {
        if(feedbackText_1 != null)
        {
            feedbackText_1.text = "Você " + phaseText + " " + correctsNumber + " riscos corretamente";
        }

        if(closeNumbers != 0)
        {
            if(phaseText == "planejou")
            {
                feedbackText_1.text += ", atribuiu " + assigned + " riscos";
                feedbackText_1.text += " e acertou o tipo de " + closeNumbers + " riscos";
            }
            else feedbackText_1.text += " e " + closeNumbers + " próximos do correto";
        }
        
        DisplayResources();
    }

    public void PhaseFeedback()
    {
        DisplayResources();
    }

}
