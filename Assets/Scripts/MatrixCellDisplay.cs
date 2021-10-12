using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MatrixCellDisplay : MonoBehaviour
{
    public GameObject cell;
    public Text probText;
    public Text impactText;
    public Text risksText;
    public GameObject risksDisplayer;
    public Button show;
    public Button hide;

    public void SetCellDisplay()
    {
        // int prob = cell.GetComponent<RiskDisplay>().prob;
        // int impact = cell.GetComponent<RiskDisplay>().impact;

        // //set the probability text
        // switch (prob)
        // {
        //     case 1:
        //         probText.text = "Muito Baixa";
        //         break;
        //     case 2:
        //         probText.text = "Baixa";
        //         break;
        //     case 3:
        //         probText.text = "Moderada";
        //         break;
        //     case 4:
        //         probText.text = "Alta";
        //         break;
        //     case 5:
        //         probText.text = "Muito Alta";
        //         break;
        //     default:
        //         break;
        // }

        // //set the impact text
        // switch (impact)
        // {
        //     case 1:
        //         impactText.text = "Muito Baixo";
        //         break;
        //     case 2:
        //         impactText.text = "Baixo";
        //         break;
        //     case 3:
        //         impactText.text = "Moderado";
        //         break;
        //     case 4:
        //         impactText.text = "Alto";
        //         break;
        //     case 5:
        //         impactText.text = "Muito Alto";
        //         break;
        //     default:
        //         break;
        // }

        // foreach (Risk rsk in cell.GetComponent<MatrixRiskDisplay>().cellRisks)
        // {
        //     risksText.text += rsk.riskName + "\n";
        //     risksDisplayer.GetComponent<RectTransform>().offsetMin -= new Vector2(0,20);
        // }

    }

    public void ShowCellRisks()
    {
        // gameObject.SetActive(true);
        // show.gameObject.SetActive(false);
        // hide.gameObject.SetActive(true);
        //SetCellDisplay();
    }

    public void HideCellRisks()
    {
        //gameObject.SetActive(false);
        //hide.gameObject.SetActive(false);
    }

    public void SetCell()
    {
        //cell = Add.matrixCell;
        //Debug.Log(cell.GetComponent<Risk>().riskName);
        //show.gameObject.SetActive(true);
    }
}
