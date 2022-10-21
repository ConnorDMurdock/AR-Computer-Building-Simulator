using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseArrow : MonoBehaviour
{
    [SerializeField] private GameObject computerCase;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject caseNotVisibleError;

    private bool isCaseVisible;
    private Transform arrowFocus;

    private void Update()
    {
        isCaseVisible = computerCase.GetComponentInChildren<Renderer>().isVisible;

        if (!computerCase.activeInHierarchy)
        {
            return;
        }
        else
        {
            if (!isCaseVisible)
            {
                arrow.SetActive(true);
                caseNotVisibleError.SetActive(true);
                arrowFocus = computerCase.transform;
                arrow.transform.LookAt(arrowFocus);
            }
            else
            {
                arrow.SetActive(false);
                caseNotVisibleError.SetActive(false);
            }
        }
    }
}
