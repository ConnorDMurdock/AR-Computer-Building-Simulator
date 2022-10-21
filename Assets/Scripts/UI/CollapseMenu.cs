using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseMenu : MonoBehaviour
{
    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private Vector2 openedPosition;
    [SerializeField] private float animationSpeed;
    private bool isMenuOpen = false;
    private UIAudioManager UIAinstance;

    private void Start()
    {
        UIAinstance = UIAudioManager.GetInstance();
    }

    public void CollapseOrOpenMenu()
    {
        if (isMenuOpen)
        {
            transform.LeanMove(closedPosition, animationSpeed).setEaseOutQuart();
            UIAinstance.PlayAudio("TurnLeft");
            isMenuOpen = false;
        }
        else if (!isMenuOpen)
        {
            transform.LeanMove(openedPosition, animationSpeed).setEaseOutQuart();
            UIAinstance.PlayAudio("TurnRight");
            isMenuOpen = true;
        }
    }
}
