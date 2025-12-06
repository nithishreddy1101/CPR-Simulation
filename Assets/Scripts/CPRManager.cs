using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // For new Input System if using XR Interaction Toolkit
using UnityEngine.XR.Interaction.Toolkit; // For XR UI interactions

public class CprManagerVR : MonoBehaviour
{
    [Header("VR Player References")]
    [SerializeField] private GameObject xrOrigin; // Assign your XR Origin GameObject here
    [SerializeField] private GameObject mainViewpoint; // Original player position (e.g., an empty GameObject at start)
    [SerializeField] private GameObject cprViewpoint;   // Target player position for CPR (e.g., an empty GameObject above chest)

    [Header("UI Elements")]
    [SerializeField] private Button startCprButton;
    [SerializeField] private TextMeshProUGUI cprTimerText;

    [Header("CPR Settings")]
    [SerializeField] private float cprDuration = 30f; // 30 seconds for CPR
    [SerializeField] private float cameraShiftFadeDuration = 0.5f; // For smooth fade transition

    private float currentCprTime;
    private bool cprInProgress = false;

    // Optional: Reference to your CPR animation component
    // [SerializeField] private Animator patientAnimator;
    // [SerializeField] private string cprAnimationTrigger = "StartCPR";

    // Optional: UI Fade effect (e.g., a black quad in front of the camera)
    [SerializeField] private Image fadePanel; // Assign a UI Image (full screen, black, alpha 0) for fading

    void Start()
    {
        // Ensure XR Origin starts at the main viewpoint
        if (mainViewpoint != null)
        {
            xrOrigin.transform.position = mainViewpoint.transform.position;
            xrOrigin.transform.rotation = mainViewpoint.transform.rotation;
        }

        // Initially hide the timer text
        if (cprTimerText != null)
        {
            cprTimerText.gameObject.SetActive(false);
        }
        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 0); // Ensure fade panel is transparent initially
        }

        // Add listener to the button (using traditional UI event for simplicity with Button component)
        if (startCprButton != null)
        {
            startCprButton.onClick.AddListener(StartCprSequence);
            // If using XR Interaction Toolkit's UI events, you'd use an XRBaseInteractable.selectEntered.AddListener
            // Example:
            // XRSimpleInteractable simpleInteractable = startCprButton.GetComponent<XRSimpleInteractable>();
            // if (simpleInteractable != null)
            // {
            //     simpleInteractable.selectEntered.AddListener(OnStartCprButtonPressed);
            // }
        }
        else
        {
            Debug.LogError("Start CPR Button not assigned in CprManagerVR!");
        }
    }

    void Update()
    {
        if (cprInProgress)
        {
            currentCprTime -= Time.deltaTime;

            if (currentCprTime <= 0)
            {
                currentCprTime = 0;
                EndCprSequence();
            }

            // Update timer display
            int minutes = Mathf.FloorToInt(currentCprTime / 60);
            int seconds = Mathf.FloorToInt(currentCprTime % 60);
            cprTimerText.text = string.Format("CPR: {0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartCprSequence() // Called by UI Button
    {
        if (cprInProgress) return; // Prevent starting multiple times

        Debug.Log("Start CPR button pressed!");

        // Disable the start CPR button
        if (startCprButton != null)
        {
            startCprButton.interactable = false;
        }

        StartCoroutine(CprSequenceCoroutine());
    }

    // Optional: If using XR Interaction Toolkit for button clicks
    // public void OnStartCprButtonPressed(SelectEnterEventArgs args)
    // {
    //     StartCprSequence();
    // }

    private IEnumerator CprSequenceCoroutine()
    {
        // 1. Fade to black
        yield return StartCoroutine(FadeScreen(1f, cameraShiftFadeDuration)); // Fade to opaque

        // 2. Teleport XR Origin to CPR viewpoint
        if (cprViewpoint != null)
        {
            xrOrigin.transform.position = cprViewpoint.transform.position;
            xrOrigin.transform.rotation = cprViewpoint.transform.rotation;
        }
        Debug.Log("Teleported to CPR viewpoint.");

        // 3. Fade back in
        yield return StartCoroutine(FadeScreen(0f, cameraShiftFadeDuration)); // Fade to transparent

        // 4. Start CPR Timer and Animation
        cprInProgress = true;
        currentCprTime = cprDuration;
        cprTimerText.gameObject.SetActive(true); // Show the timer text

        // Optional: Trigger CPR animation
        // if (patientAnimator != null)
        // {
        //     patientAnimator.SetTrigger(cprAnimationTrigger);
        // }

        Debug.Log("CPR Started!");
    }

    private void EndCprSequence()
    {
        if (!cprInProgress) return; // Prevent ending multiple times

        cprInProgress = false;
        Debug.Log("CPR Ended!");

        // Hide the timer text
        if (cprTimerText != null)
        {
            cprTimerText.gameObject.SetActive(false);
        }

        // Optional: Stop CPR animation
        // if (patientAnimator != null)
        // {
        //     patientAnimator.SetTrigger("StopCPR"); // Or whatever you use to stop/reset animation
        // }

        StartCoroutine(EndCprSequenceCoroutine());
    }

    private IEnumerator EndCprSequenceCoroutine()
    {
        // 1. Fade to black
        yield return StartCoroutine(FadeScreen(1f, cameraShiftFadeDuration));

        // 2. Teleport XR Origin back to main viewpoint
        if (mainViewpoint != null)
        {
            xrOrigin.transform.position = mainViewpoint.transform.position;
            xrOrigin.transform.rotation = mainViewpoint.transform.rotation;
        }
        Debug.Log("Teleported back to main viewpoint.");

        // 3. Fade back in
        yield return StartCoroutine(FadeScreen(0f, cameraShiftFadeDuration));

        // Re-enable the start CPR button
        if (startCprButton != null)
        {
            startCprButton.interactable = true;
        }

        // Add any other logic for what happens after CPR ends (e.g., scoring, next steps)
    }

    // Coroutine for fading the screen in/out
    private IEnumerator FadeScreen(float targetAlpha, float duration)
    {
        if (fadePanel == null)
        {
            Debug.LogWarning("Fade Panel not assigned! Cannot perform screen fade.");
            yield break;
        }

        float startAlpha = fadePanel.color.a;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            fadePanel.color = new Color(0, 0, 0, newAlpha); // Keep black, change alpha
            yield return null;
        }
        fadePanel.color = new Color(0, 0, 0, targetAlpha); // Ensure final alpha is set
    }
}