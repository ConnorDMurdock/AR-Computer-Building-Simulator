using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMenuBooleans : MonoBehaviour
{
    public TMP_Text beingPlacedText;
    public TMP_Text isPlacedPressedText;
    public TMP_Text placementPoseValidText;

    private void Start()
    {
        var instance = ComputerCaseManager.GetInstance();
    }

    private void Update()
    {
        bool beingPlaced = ComputerCaseManager.GetInstance().beingPlaced;
        bool isPlacedPressed = ComputerCaseManager.GetInstance().isPlacedPressed;
        bool placementPoseIsValid = ComputerCaseManager.GetInstance().placementPoseIsValid;

        if (beingPlaced)
        {
            beingPlacedText.text = "beingPlaced: True";
            beingPlacedText.color = Color.green;
        }
        else if (!beingPlaced)
        {
            beingPlacedText.text = "beingPlaced: False";
            beingPlacedText.color = Color.red;
        }

        if (isPlacedPressed)
        {
            isPlacedPressedText.text = "isPlacedPressed: True";
            isPlacedPressedText.color = Color.green;
        }
        else if (!isPlacedPressed)
        {
            isPlacedPressedText.text = "isPlacedPressed: False";
            isPlacedPressedText.color = Color.red;
        }

        if (placementPoseIsValid)
        {
            placementPoseValidText.text = "placementPoseValid: True";
            placementPoseValidText.color = Color.green;
        }
        else if (!placementPoseIsValid)
        {
            placementPoseValidText.text = "placementPoseValid: False";
            placementPoseValidText.color = Color.red;
        }
    }
}
