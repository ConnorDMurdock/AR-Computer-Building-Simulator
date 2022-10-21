using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenAndClose : MonoBehaviour
{
    public GameObject menuToOpen;
    public GameObject menuToClose;

    private float openAndCloseMenuSpeed = 0.3f;
    private float waitTimeBetweenMenus = 0.5f;

    public void ChangeMenu()
    {
        StartCoroutine(OpenAndCloseMenus(menuToClose, menuToOpen));
    }

    public void ButtonPressed()
    {
        UIAudioManager.GetInstance().PlayAudio("ButtonPressed");
    }

    IEnumerator OpenAndCloseMenus(GameObject menuToClose, GameObject menuToOpen)
    {
        menuToClose.LeanScale(new Vector3(0,0,0), openAndCloseMenuSpeed);
        UIAudioManager.GetInstance().PlayAudio("CloseMenu");
        menuToClose.SetActive(false);
        yield return new WaitForSeconds(waitTimeBetweenMenus);
        menuToOpen.SetActive(true);
        menuToOpen.LeanScale(new Vector3(1, 1, 1), openAndCloseMenuSpeed);
        UIAudioManager.GetInstance().PlayAudio("OpenMenu");
        yield break;
    }
}
