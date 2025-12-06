using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;

public class CPRSceneManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform cprCameraPosition;

    public GameObject xrRig;
    public GameObject leftXRHand, rightXRHand;
    public GameObject cprSceneAnimation; // Whole CPR animation scene
    public Animator cprAnimator;
    public Button startCPRButton;

    public float animationDuration = 10f; // Duration of CPR animation in seconds

    private Vector3 originalCamPos;
    private Quaternion originalCamRot;

    void Start()
    {
        startCPRButton.onClick.AddListener(StartCPRScene);
        cprSceneAnimation.SetActive(false);
    }

    void StartCPRScene()
    {
        // Save XR camera original position
        originalCamPos = mainCamera.transform.position;
        originalCamRot = mainCamera.transform.rotation;

        // Move camera to CPR view
        mainCamera.transform.position = cprCameraPosition.position;
        mainCamera.transform.rotation = cprCameraPosition.rotation;

        // Disable XR hands
        leftXRHand.SetActive(false);
        rightXRHand.SetActive(false);

        // Play full CPR animation
        cprSceneAnimation.SetActive(true);
        cprAnimator.Play("CPR_FullScene"); // Replace with your animation name

        // Schedule return to normal
        Invoke(nameof(EndCPRScene), animationDuration);
    }

    void EndCPRScene()
    {
        // Re-enable XR hands
        leftXRHand.SetActive(true);
        rightXRHand.SetActive(true);

        // Hide CPR animation
        cprSceneAnimation.SetActive(false);

        // Restore camera
        mainCamera.transform.position = originalCamPos;
        mainCamera.transform.rotation = originalCamRot;
    }
}
