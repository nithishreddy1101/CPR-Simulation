using UnityEngine;
using UnityEngine.InputSystem;

public class MouthOpenAnimTrigger : MonoBehaviour
{
    private Animator mAnimator;
    public GameObject hand; // Assign XR hand GameObject
    public string triggerParam = "MouthOpening";

    private bool isHandInside = false;
    private bool hasMouthOpened = false;

    [Header("Input Action")]
    public InputActionReference triggerAction; // Assign this in inspector to the right or left trigger

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }

        if (triggerAction != null)
        {
            triggerAction.action.Enable();
        }
    }

    void Update()
    {
        if (isHandInside && triggerAction != null && triggerAction.action.ReadValue<float>() > 0.1f && !hasMouthOpened)
        {
            mAnimator.SetTrigger(triggerParam);
            hasMouthOpened = true;
            Debug.Log("Mouth opened via hand + trigger press.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hand)
        {
            isHandInside = true;
            Debug.Log("Hand entered mouth trigger.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hand)
        {
            isHandInside = false;
            hasMouthOpened = false; // Reset so it can trigger again if re-entered
            Debug.Log("Hand exited mouth trigger.");
        }
    }
}
