using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateBadges : MonoBehaviour
{
    public GameObject[] badges;
    public TMP_Text progressText;
    public TMP_Text playerName;

    private SaveDataManager SDMinstance;

    private void OnEnable()
    {
        foreach (GameObject badge in badges)
        {
            badge.SetActive(false);
        }

        SDMinstance = SaveDataManager.GetInstance();

        SetBadgesActive();

        playerName.text = SDMinstance.GetFileName();

        if (SDMinstance.GetNumberOfBadges() == 10)
        {
            progressText.text = "Congratulations!";
        }
        else
        {
            progressText.text = "Keep it up!";
        }
    }

    private void SetBadgesActive()
    {
        if (SDMinstance.GetFlagData("Case", 1)){
            badges[0].SetActive(true);
        }
        if (SDMinstance.GetFlagData("CPU", 1))
        {
            badges[1].SetActive(true);
        }
        if (SDMinstance.GetFlagData("RAM", 1))
        {
            badges[2].SetActive(true);
        }
        if (SDMinstance.GetFlagData("HardDrive", 1))
        {
            badges[3].SetActive(true);
        }
        if (SDMinstance.GetFlagData("USB", 1))
        {
            badges[4].SetActive(true);
        }
        if (SDMinstance.GetFlagData("SSD", 1))
        {
            badges[5].SetActive(true);
        }
        if (SDMinstance.GetFlagData("GraphicsCard", 1))
        {
            badges[6].SetActive(true);
        }
        if (SDMinstance.GetFlagData("Cooling", 1))
        {
            badges[7].SetActive(true);
        }
        if (SDMinstance.GetFlagData("PowerSupply", 1))
        {
            badges[8].SetActive(true);
        }
        if (SDMinstance.GetFlagData("PluggingIn", 1))
        {
            badges[9].SetActive(true);
        }

    }
}
