using UnityEngine;
using TMPro;
using System.Collections;

public class DialPadController : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public GameObject dialingPanel;
    public GameObject dialPadPanel;     // First panel
    public GameObject resultPanel;      // Second panel to appear after 3 seconds

    public void AddDigit(string digit)
    {
        displayText.text += digit;
    }

    public void ClearDisplay()
    {
        displayText.text = "";
    }

    public void ShowDialPad()
    {
        dialingPanel.SetActive(false);
        dialPadPanel.SetActive(true);
        resultPanel.SetActive(false);   // Make sure resultPanel is hidden initially
        StartCoroutine(HandleDialSequence());
    }

    private IEnumerator HandleDialSequence()
    {
        yield return new WaitForSeconds(3f);

        dialPadPanel.SetActive(false);
        resultPanel.SetActive(true);    // Show new panel after 3 seconds
    }
}
