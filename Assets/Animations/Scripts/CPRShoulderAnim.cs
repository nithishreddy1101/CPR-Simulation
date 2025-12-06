using UnityEngine;
using UnityEngine.InputSystem;

public class ShoulderAnimTrigger : MonoBehaviour
{
    private Animator mAnimator;
    public string boolParameterName = "Shaking";

    public GameObject hand;

    [Header("Input Actions")]
    public InputActionReference rightTriggerAction;
    

    private bool isHandInside = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Animator not found!");
        }

        if (hand == null)
            Debug.LogError("Hand GameObject not assigned!");

        if (rightTriggerAction == null)
            Debug.LogError("Trigger input not assigned!");
    }

    void Update()
    {   
        float rightTriggerValue = rightTriggerAction.action.ReadValue<float>();
        bool isTriggerPressed = rightTriggerAction != null && rightTriggerAction.action.ReadValue<float>() > 0.1f;
        bool shouldShake = isHandInside && isTriggerPressed;
        Debug.Log($"Right Trigger (OVRInput): {rightTriggerValue:F2}");

        if (mAnimator.GetBool(boolParameterName) != shouldShake)
        {
            mAnimator.SetBool(boolParameterName, shouldShake);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hand)
        {
            isHandInside = true;
            Debug.Log("Hand entered trigger.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hand)
        {
            isHandInside = false;
            mAnimator.SetBool(boolParameterName, false); // Stop shake immediately on exit
            Debug.Log("Hand exited trigger.");
        }
    }
}
