using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARInstallSequence : MonoBehaviour
{
    //Variables:
    //Number of "sequences" in the install animation. How many times to loop through dialogue, install part (1 sequence), dialogue, install part (2 sequences)...
    public int numberOfSequences;
    private int currentSequence = 0;

    //Reference the the next button for the install sequence. Every part should have it's own next button.
    //each next button should start the event NextButtonPressed for its specific InstallSequence
    public Button nextButton;
    public Button partButton;

    //A list of all the buttons to show after the part is successfully installed
    //This is used to force users to install parts in a specific order
    public List<Button> unlockedParts;

    //The list of all text assets and the parts to install. The longest list length should be equal to the number of sequences
    //Sequences always go dialogue first, then part install, repeat. Animation length is to control the time when the next button is visible again
    public List<TextAsset> listOfDialogues;
    public List<GameObject> listOfPartsToInstall;
    public List<float> animationLengths;

    //The name of the flag to save on the save file. This should be EXACTLY the same as the string value in the map pair for save values
    public string flagToSave;

    //Used to hold references within the script from the above variables
    private bool isDialoguePlaying;
    private bool partSuccessfullyInstalled;
    private bool displayNext;
    private GameObject partToInstall;

    //Update Method:
    //1. updates the isDialoguePlaying boolean to whether the DialogueController Script currently has dialogue playing
    //2. updates whether or not the Next Button should be visible depending on whether or not the part to be installed
    //   is visible on screen and there is no dialogue currently playing
    private void Update()
    {
        Debug.Log(displayNext);
        
        isDialoguePlaying = DialogueController.GetInstance().dialogueIsPlaying;

        if (partToInstall != null)
        {
            if (!partToInstall.GetComponentInChildren<Renderer>().isVisible || displayNext == false)
            {
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }
        }
    }

    //Starts the install sequence. Sets current sequence count to 0, disables the Next Button (as a fail safe), and starts the Iterate Sequence Coroutine
    public void StartSequence()
    {
        currentSequence = 0;
        nextButton.gameObject.SetActive(false);
        StartCoroutine(IterateSequence());
    }

    //When the Next Button is pressed, remove the outline around the part to install and start the Part Install Sequence
    public void NextButtonPressed()
    {
        if (!isDialoguePlaying)
        {
            partToInstall.GetComponent<Outline>().enabled = false;
            StartCoroutine(InstallPart());
        }
    }

    //First, makes sure there is dialogue to play and if there is, send the sequence number to the Dialogue Controller to play the correct dialogue
    private void playDialogue(int sequence)
    {
        if (listOfDialogues[sequence] != null)
        {
            DialogueController.GetInstance().EnterDialogueMode(listOfDialogues[sequence]);
        }
    }

    //Sequences are assigned in the inspector for the gameObject. Each sequence goes as follows:
    //==play dialogue, install part, repeat==
    //The number of sequences must be set to the largest number of dialogues or parts
    //  EX. if there are 3 dialogue scripts, but only 1 part, sequence number must be 3. It will install the singular part, and then
    //  skip the part install for the remaining sequences, as there are no more parts to be installed.
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
                    displayNext = true;
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

    //Handles the installing part section of a sequence.
    IEnumerator InstallPart()
    {
        nextButton.gameObject.SetActive(false);
        partToInstall.GetComponent<Animator>().SetBool("InstallPressed", true);
        displayNext = false;
        Debug.Log("starting install");
        yield return new WaitForSeconds(animationLengths[currentSequence]);
        Debug.Log("finished install");
        partSuccessfullyInstalled = true;
        yield break;
    }
}
