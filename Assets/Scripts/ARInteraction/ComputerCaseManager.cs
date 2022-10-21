using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ComputerCaseManager : MonoBehaviour
{
    public static ComputerCaseManager instance;

    [Header("Game Objects")]
    //[SerializeField] private GameObject computerCaseParentObject;
    [SerializeField] private GameObject placementCursor;
    [SerializeField] private GameObject placeCaseButton;
    [SerializeField] private GameObject surfaceErrorMessage;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private GameObject partsMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header("")]
    public GameObject computerCase;
    public Pose placementPose;
    public bool placementPoseIsValid = false;
    private List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    public bool beingPlaced = false;
    public bool isPlacedPressed = false;

    private GameObject[] otherUIElements;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one ComputerCaseManager in this scene!");
        }
        else
        {
            instance = this;
        }
        computerCase.SetActive(false);
        otherUIElements = GameObject.FindGameObjectsWithTag("User Interface");
    }

    private void Update()
    {
        if (beingPlaced)
        {
            if (placementPoseIsValid && isPlacedPressed)
            {
                PlaceObject();
            }
            else
            {
                UpdatePlacementPose();
                UpdatePlacementCursor();
            }
        }

    }

    public void PlaceCaseButtonPressed()
    {
        isPlacedPressed = true;
    }

    public void StartPlacementProcess()
    {
        beingPlaced = true;
        DisableOtherUIElements();
        computerCase.SetActive(false);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementCursor()
    {
        if (!computerCase.activeInHierarchy && placementPoseIsValid)
        {
            placementCursor.SetActive(true);
            placementCursor.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            surfaceErrorMessage.SetActive(false);
            placeCaseButton.SetActive(true);
        }
        else
        {
            surfaceErrorMessage.SetActive(true);
            placeCaseButton.SetActive(false);
            placementCursor.SetActive(false);
        }
    }

    private void PlaceObject()
    {
        placementCursor.SetActive(false);
        surfaceErrorMessage.SetActive(false);
        computerCase.transform.position = placementPose.position;
        Quaternion targetRotation = new Quaternion(-90, placementPose.rotation.y, -90, placementPose.rotation.w);
        computerCase.transform.rotation = placementPose.rotation;
        computerCase.SetActive(true);
        beingPlaced = false;
        isPlacedPressed = false;
        partsMenu.SetActive(true);
        optionsMenu.SetActive(true);
        placeCaseButton.SetActive(false);
    }

    private void DisableOtherUIElements()
    {
        foreach (GameObject objects in otherUIElements)
        {
            objects.SetActive(false);
        }
    }

    public static ComputerCaseManager GetInstance()
    {
        return instance;
    }
}
