using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MatrixRiskDisplay : RiskDisplay
{
    public List<Risk> cellRisks = new List<Risk>();

    public void SelectMatrixCell()
    {
        Add.matrixCell = this.gameObject;
    }

    public void SetMatrixCell(Risk rsk)
    {
        cellRisks.Add(risk);
        if(nameText.text == "") nameText.text += rsk.id.ToString();
        else nameText.text += ", " + rsk.id.ToString();
    }
}
