using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionGameState : MonoBehaviour
{
    public TextAsset introduction1;
    public TextAsset introduction2;

    private bool isDialoguePlaying;

    public GameObject optionsMenu;
    public GameObject partsMenu;

    private void Update()
    {
        isDialoguePlaying = DialogueController.GetInstance().dialogueIsPlaying;
    }

    private void Start()
    {
        if(SaveDataManager.GetInstance().GetFlagData("Case", 1))
        {
            ComputerCaseManager.GetInstance().StartPlacementProcess();
            return;
        }
        else
        {
            StartCoroutine(IntroductionStart());
        }
    }

    IEnumerator IntroductionStart()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Dialogue 1 start");
        DialogueController.GetInstance().EnterDialogueMode(introduction1);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !isDialoguePlaying);
        Debug.Log("Start Install");
        ComputerCaseManager.GetInstance().StartPlacementProcess();
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => ComputerCaseManager.GetInstance().computerCase.activeInHierarchy);
        Debug.Log("Dialogue 2 start");
        DialogueController.GetInstance().EnterDialogueMode(introduction2);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !isDialoguePlaying);
        optionsMenu.SetActive(true);
        partsMenu.SetActive(true);
        SaveDataManager.GetInstance().SaveFlag("Case", 1);
        yield break;
    }
}
