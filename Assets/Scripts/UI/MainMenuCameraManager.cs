using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraManager : MonoBehaviour
{
    public static MainMenuCameraManager instance;

    public GameObject center;
    public Camera cameraMain;

    public float angleToRotate;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one MainMenuCameraManager in this scene!");
        }
        else
        {
            instance = this;
        }

        cameraMain = Camera.main;
    }

    private void Update()
    {
        cameraMain.transform.LookAt(center.transform);
    }

    public void RotateMenuRight(int numberOfPanels)
    {
        //cameraMain.transform.RotateAround(center.transform.position, center.transform.forward, angleToRotate);
        LeanTween.rotateAround(center, center.transform.forward, angleToRotate * numberOfPanels, 0.75f * numberOfPanels).setEaseOutQuart();
        UIAudioManager.GetInstance().PlayAudio("TurnRight");
    }

    public void RotateMenuLeft(int numberOfPanels)
    {
        LeanTween.rotateAround(center, -center.transform.forward, angleToRotate * numberOfPanels, 0.75f * numberOfPanels).setEaseOutQuart();
        UIAudioManager.GetInstance().PlayAudio("TurnLeft");
    }

    public static MainMenuCameraManager GetInstance()
    {
        return instance;
    }
}
