using UnityEngine;

public class ShoulderShakeTrigger : MonoBehaviour
{
    private Animator mAnimator;
    public string boolParameterName = "IsShaking";
    public GameObject hand;

    private bool isHandInside = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Animator not found!");
        }
    }

    void Update()
    {
        if (isHandInside && Input.GetKey(KeyCode.G))
        {
            mAnimator.SetBool(boolParameterName, true);
        }
        else
        {
            mAnimator.SetBool(boolParameterName, false);
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
