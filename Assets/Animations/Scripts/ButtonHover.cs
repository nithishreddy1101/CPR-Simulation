using UnityEngine;

public class ChestHoverButtonTrigger : MonoBehaviour
{
    public GameObject startCPRButton;

    void Start()
    {
        if (startCPRButton != null)
            startCPRButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            startCPRButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            startCPRButton.SetActive(false);
        }
    }
}
