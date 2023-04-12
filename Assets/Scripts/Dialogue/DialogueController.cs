using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance;

    [Header("Text")]
    [SerializeField]private TMP_Text tmpText;
    public float textScrollSpeed;

    public GameObject expressionsParent;
    public List<Image> expressions;
    [SerializeField] private GameObject textBox;

    [SerializeField] private GameObject continueIcon;
    private bool canContinueToNextLine = false;

    [Header("Audio")]
    [SerializeField] private AudioClip[] dialogueTypingSoundClips;
    [SerializeField] private bool stopAudioSource;
    [Range(1, 10)]
    [SerializeField] private int frequencyLevel = 5;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;

    private AudioSource audioSource;

    private Story currentStory;
    public bool dialogueIsPlaying;

    //private const string EXPRESSION = "expression";
    private const string NEUTRAL = "neutral";
    private const string HAPPY = "happy";
    private const string ERROR = "error";
    private const string THUMBSUP = "thumbsup";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one DialogueController in this scene!");
        }
        else
        {
            instance = this;
        }

        //StartCoroutine(RevealCharacters());

        audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        textBox.SetActive(false);
        expressionsParent.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && canContinueToNextLine)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        textBox.SetActive(true);
        expressionsParent.SetActive(true);
        expressions[0].gameObject.SetActive(true);
        

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        textBox.SetActive(false);
        expressionsParent.SetActive(false);
        tmpText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagValue)
            {
                case NEUTRAL:
                    ChangeExpression(expressions[0]);
                    break;
                case HAPPY:
                    ChangeExpression(expressions[1]);
                    break;
                case ERROR:
                    ChangeExpression(expressions[2]);
                    break;
                case THUMBSUP:
                    ChangeExpression(expressions[3]);
                    break;
            }
        }
    }

    IEnumerator DisplayLine(string line)
    {
        canContinueToNextLine = false;
        continueIcon.SetActive(false);
        tmpText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            PlayDialogueSound(tmpText.textInfo.characterCount);
            tmpText.text += letter;
            yield return new WaitForSeconds(textScrollSpeed);
        }
        canContinueToNextLine = true;
        continueIcon.SetActive(true);
    }

    IEnumerator RevealCharacters()
    {
        ChangeExpression(expressions[0]);

        tmpText.ForceMeshUpdate();
        int currentCharacters = 0;
        TMP_TextInfo textInfo = tmpText.textInfo;
        int maxCharacters = textInfo.characterCount;

        while (currentCharacters < maxCharacters)
        {
            yield return new WaitForSeconds(textScrollSpeed);
            PlayDialogueSound(currentCharacters);
            tmpText.maxVisibleCharacters = currentCharacters;
            currentCharacters++;
        }

        yield return null;
    }

    private void ChangeExpression(Image expressionToDisplay)
    {
        foreach (Image image in expressions)
        {
            image.gameObject.SetActive(false);
            expressionToDisplay.gameObject.SetActive(true);
        }
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }

            int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
            AudioClip soundClip = dialogueTypingSoundClips[randomIndex];

            audioSource.pitch = Random.Range(minPitch, maxPitch);

            audioSource.PlayOneShot(soundClip);
        }
    }

    public void MuteAudio()
    {
        audioSource.volume = 0;
    }

    public void UnmuteAudio()
    {
        audioSource.volume = 1;
    }

    public static DialogueController GetInstance()
    {
        return instance;
    }
}
