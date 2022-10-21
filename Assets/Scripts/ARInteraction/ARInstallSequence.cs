using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARInstallSequence : MonoBehaviour
{
    public int numberOfSequences;
    private int currentSequence = 0;

    public Button nextButton;
    public Button partButton;

    public List<Button> unlockedParts;

    public List<TextAsset> listOfDialogues;
    public List<GameObject> listOfPartsToInstall;
    public List<float> animationLengths;

    private bool isDialoguePlaying;
    private bool partSuccessfullyInstalled;

    private GameObject partToInstall;

    public string flagToSave;

    private void Update()
    {
        isDialoguePlaying = DialogueController.GetInstance().dialogueIsPlaying;

        if (partToInstall != null && partSuccessfullyInstalled == false)
        {
            if (!partToInstall.GetComponentInChildren<Renderer>().isVisible)
            {
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }
        }
        else if (partSuccessfullyInstalled)
        {
            nextButton.gameObject.SetActive(false);
        }
    }


    public void StartSequence()
    {
        currentSequence = 0;
        nextButton.gameObject.SetActive(false);
        StartCoroutine(IterateSequence());
    }

    public void NextButtonPressed()
    {
        if (!isDialoguePlaying)
        {
            partToInstall.GetComponent<Outline>().enabled = false;
            StartCoroutine(InstallPart());
        }
    }

    private void playDialogue(int sequence)
    {
        if (listOfDialogues[sequence] != null)
        {
            DialogueController.GetInstance().EnterDialogueMode(listOfDialogues[sequence]);
        }
    }

    IEnumerator IterateSequence()
    {
        yield return new WaitForSeconds(0.5f);
        while (currentSequence < numberOfSequences)
        {
            partSuccessfullyInstalled = false;

            playDialogue(currentSequence);

            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !isDialoguePlaying);

            try
            {
                if (listOfPartsToInstall[currentSequence] != null)
                {
                    partToInstall = listOfPartsToInstall[currentSequence];
                    partToInstall.GetComponent<Outline>().enabled = true;
                    nextButton.gameObject.SetActive(true);
                }
            }
            catch (System.Exception)
            {
                partSuccessfullyInstalled = true;
            }

            yield return new WaitUntil(() => partSuccessfullyInstalled);
            currentSequence++;
        }
        partSuccessfullyInstalled = true;
        SaveDataManager.GetInstance().SaveFlag(flagToSave, 1);
        partButton.gameObject.SetActive(false);
        foreach (Button b in unlockedParts)
        {
            b.gameObject.SetActive(true);
        }
    }

    IEnumerator InstallPart()
    {
        nextButton.gameObject.SetActive(false);
        partToInstall.GetComponent<Animator>().SetBool("InstallPressed", true);
        Debug.Log("starting install");
        yield return new WaitForSeconds(animationLengths[currentSequence]);
        Debug.Log("finished install");
        partSuccessfullyInstalled = true;
        yield break;
    }
}
