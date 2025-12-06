using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CPRHaptic : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference rightTriggerAction;
    public InputActionReference leftTriggerAction;

    [Header("Haptics")]
    [SerializeField] private XRBaseController leftXRController;
    [SerializeField] private XRBaseController rightXRController;

    [Header("Animator")]
    [SerializeField] private Animator cprAnimator;

    [SerializeField] private DepthIndex _depthbar;

    private float _maxDepth = 1;
    private float _currentDepth;
    public void Start()
    {
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();
        cprAnimator = GetComponent<Animator>();
        _currentDepth = _maxDepth;
        _depthbar.UpdateDepthBar(_maxDepth, _currentDepth);
    }

    void Update()
    {
        float leftTriggerValue = leftTriggerAction.action.ReadValue<float>();
        float rightTriggerValue = rightTriggerAction.action.ReadValue<float>();
        Debug.Log($"Right Trigger (OVRInput): {rightTriggerValue:F2}");
        Debug.Log($"Left Trigger (OVRInput): {leftTriggerValue:F2}");
        cprAnimator.SetFloat("DoCPR", leftTriggerValue);
        _currentDepth = leftTriggerValue;
        _depthbar.UpdateDepthBar(_maxDepth, _currentDepth);

        if (leftTriggerValue > 0.1f)
        {
            LeftControllerVibration(leftTriggerValue, Time.deltaTime); // Duration short to avoid overlap
        }

        if (rightTriggerValue > 0.1f)
        {
            RightControllerVibration(rightTriggerValue, Time.deltaTime);
        }
    }
    public void SendHaptic(XRBaseController controller, float amplitude, float duration)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration); //0.1f, 0.1, 0.2, 0.3
        }
    }


    public void LeftControllerVibration(float amplitude, float duration)
    {
        SendHaptic(leftXRController, amplitude, duration);
    }

    public void RightControllerVibration(float amplitude, float duration)
    {
        SendHaptic(rightXRController, amplitude, duration);
    } 
}
