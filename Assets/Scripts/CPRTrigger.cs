using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CPRModeController : MonoBehaviour
{
    public GameObject xrLeftHand;
    public GameObject xrRightHand;
    public GameObject xrLeftRay;
    public GameObject xrRightRay;
    public GameObject animatedCPRHand1;
    public GameObject animatedCPRHand2;

    [Header("Hand References")]
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject cprCount_;
    public GameObject exit;

    public GameObject CPRAudio;

    public Transform cameraRig;
    public Transform cameraOffset;

    public Transform cprCameraPoint;
    public TextMeshProUGUI cprCountText;


    public InputActionReference rightTriggerAction;
    public InputActionReference leftTriggerAction;

    private bool wasRightTriggerPressed = false;
    private bool wasLeftTriggerPressed = false;

    private bool isLeftHandNear = false;
    private bool isRightHandNear = false;


    private bool inCPR = false;

    private int cprCount = 0;



    private Vector3 originalCamPos;
    private Quaternion originalCamRot;

    private Vector3 originalOffsetPos;
    private Quaternion originalOffsetRot;


    public Transform mainCamera; // Drag your Main Camera here






    void Update()
    {
        if (inCPR)
        {


            float rightTriggerValue = rightTriggerAction.action.ReadValue<float>();
            float leftTriggerValue = leftTriggerAction.action.ReadValue<float>();

            bool isRightTriggerPressed = rightTriggerValue > 0.5f;
            bool isLeftTriggerPressed = leftTriggerValue > 0.5f;

            bool bothHandsNear = isLeftHandNear && isRightHandNear;

            if (bothHandsNear)
            {

                if ((isRightTriggerPressed && !wasRightTriggerPressed) || (isLeftTriggerPressed && !wasLeftTriggerPressed))
                {
                    cprCount++;
                    if (cprCountText != null)
                        cprCountText.text = "CPR Count: " + cprCount;
                }
            }

            if (cprCount >= 30)
            {
                ExitCPRMode();
            }

            wasRightTriggerPressed = isRightTriggerPressed;
            wasLeftTriggerPressed = isLeftTriggerPressed;
        }
    }

    public void EnterCPRMode()
    {

        // originalCamPos = mainCamera.position;
        // originalCamRot = mainCamera.rotation;

        // mainCamera.position = cprCameraPoint.position;
        // mainCamera.rotation = cprCameraPoint.rotation;


        xrLeftHand.SetActive(false);
        xrRightHand.SetActive(false);
        xrLeftRay.SetActive(false);
        xrRightRay.SetActive(false);

        animatedCPRHand1.SetActive(true);
        animatedCPRHand2.SetActive(true);

        CPRAudio.SetActive(true);

        inCPR = true;

        Debug.Log("CPR started.");

        cprCount = 0;
        if (cprCountText != null)
            cprCountText.text = "CPR Count: 0";

    }

    public void ExitCPRMode()
    {
        xrLeftHand.SetActive(true);
        xrRightHand.SetActive(true);
        xrLeftRay.SetActive(true);
        xrRightRay.SetActive(true);

        animatedCPRHand1.SetActive(false);
        animatedCPRHand2.SetActive(false);

        mainCamera.position = originalCamPos;
        mainCamera.rotation = originalCamRot;

        cprCount_.SetActive(false);
        exit.SetActive(true);
        CPRAudio.SetActive(false);


        inCPR = false;



        Debug.Log("CPR ended after 30 seconds.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand)
        {
            isLeftHandNear = true;
            Debug.Log("Left hand entered CPR zone.");
        }
        else if (other.gameObject == rightHand)
        {
            isRightHandNear = true;
            Debug.Log("Right hand entered CPR zone.");
        }
    }
    
    void OnTriggerExit(Collider other)
{
    if (other.gameObject == leftHand)
    {
        isLeftHandNear = false;
        Debug.Log("Left hand exited CPR zone.");
    }
    else if (other.gameObject == rightHand)
    {
        isRightHandNear = false;
        Debug.Log("Right hand exited CPR zone.");
    }
}
}
