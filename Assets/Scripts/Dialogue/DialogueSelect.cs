using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueSelect : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    public GameObject optionSelectUI;

    public void OptionSelected()
    {
        optionSelectUI.SetActive(false);
        DialogueController.GetInstance().EnterDialogueMode(inkJSON);
    }
}
