using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager instance;
    public AudioSource audioSource;

    public AudioClip openMenuSound;
    public AudioClip closeMenuSound;
    public AudioClip buttonPressSound;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one UIAudioManager in this scene!");
        }
        else
        {
            instance = this;
        }

        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayAudio(string clipName)
    {
        if (clipName.Equals("TurnRight"))
        {
            audioSource.PlayOneShot(openMenuSound);
        }
        else if (clipName.Equals("TurnLeft"))
        {
            audioSource.PlayOneShot(closeMenuSound);
        }
    }

    public void ButtonPressSound()
    {
        audioSource.PlayOneShot(buttonPressSound);
    }

    public static UIAudioManager GetInstance()
    {
        return instance;
    }
}
