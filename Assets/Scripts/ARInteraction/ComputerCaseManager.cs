using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ComputerCaseManager : MonoBehaviour
{
    public static ComputerCaseManager instance;

    //References to all of the objects needed by this script to do the place case sequence properly
    [Header("Game Objects")]
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

    //Gets all of the UI elements with the User Interface tag and gives an error if there is more than one singleton instance of this script
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

    //If the placement pose is valid and the place case button is pressed, place the case at the cursor point
    //Otherwise, continue to update the placement pose and the cursor
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

    //Starts the placement process
    public void StartPlacementProcess()
    {
        beingPlaced = true;
        DisableOtherUIElements();
        computerCase.SetActive(false);
    }

    //Updates the current placement position by using raycasts from the center of the screen to detect transform from where the raycast hits on the detected planes
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

    //While in placement mode, update the UI cursor to the current placement location
    //If no surface found, disable the place case button, show the user an error, and prevent the cursor from showing until a surface is found by the app
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

    //Sets the transform of the computer case to the transform of the cursor used during placement
    //also turns off the UI for placing the case and turns on the main game UI components
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

    //Turns off all UI elements in the game to clear up the screen while the case is placed
    private void DisableOtherUIElements()
    {
        foreach (GameObject objects in otherUIElements)
        {
            objects.SetActive(false);
        }
    }

    //Gets the instance of this singleton class
    public static ComputerCaseManager GetInstance()
    {
        return instance;
    }
}
