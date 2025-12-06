using UnityEngine;

public class MouthOpenTrigger : MonoBehaviour
{
    private Animator mAnimator;
    public GameObject hand; // Assign XR hand GameObject
    public string triggerParam = "MouthOpen";

    private bool isHandInside = false;
    private bool hasMouthOpened = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (isHandInside && Input.GetKeyDown(KeyCode.M) && !hasMouthOpened)
        {
            mAnimator.SetTrigger(triggerParam);
            hasMouthOpened = true;
            Debug.Log("Mouth opened via hand + M key.");
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
