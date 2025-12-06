using UnityEngine;

public class CPRAnimator : MonoBehaviour
{
    public Animator chestAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (chestAnimator != null)
                chestAnimator.SetTrigger("PlayCPR");
        }
    }
}
