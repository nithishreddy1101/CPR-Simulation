using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Uncomment this if using haptics with XR controllers
// using UnityEngine.XR.Interaction.Toolkit;

public class CPRHandAndChestMover : MonoBehaviour
{
    [Header("Transforms")]
    public Transform hand1;
    public Transform hand2;
    public Transform nippleLeft;
    public Transform nippleRight;

    [Header("Compression Settings")]
    public float moveDistance = 0.05f;   // How deep to compress (meters)
    public float moveSpeed = 3f;         // Speed of compression animation
    public float compressDuration = 1f;  // Total time for one compression (down + up)
    public KeyCode triggerKey = KeyCode.Space;

    [Header("UI")]
    public Slider depthSlider;                   // Shows depth (0 to 1)
    public TextMeshProUGUI compressionCounterText;

    [Header("Audio")]
    public AudioSource compressionSound;

    // [Header("Haptics (XR Controllers)")]
    // public XRBaseController leftController;
    // public XRBaseController rightController;

    private Vector3 hand1StartPos;
    private Vector3 hand2StartPos;
    private Vector3 nippleLStartPos;
    private Vector3 nippleRStartPos;

    private bool isCompressing = false;
    private float compressProgress = 0f;
    private int compressionCount = 0;

    void Start()
    {
        if (hand1) hand1StartPos = hand1.localPosition;
        if (hand2) hand2StartPos = hand2.localPosition;
        if (nippleLeft) nippleLStartPos = nippleLeft.localPosition;
        if (nippleRight) nippleRStartPos = nippleRight.localPosition;

        if (depthSlider) depthSlider.value = 0f;
        if (compressionCounterText) compressionCounterText.text = "Compressions: 0";
    }

    void Update()
    {
        // Start compression when key is pressed
        if (Input.GetKeyDown(triggerKey) && !isCompressing)
        {
            isCompressing = true;
            compressProgress = 0f;
        }

        if (isCompressing)
        {
            compressProgress += Time.deltaTime * moveSpeed;

            // t goes from 0 → 1 → 0
            float t = Mathf.PingPong(compressProgress, 1f);

            // Global downward offset (Y-axis)
            Vector3 downOffset = Vector3.down * moveDistance * t;

            // Move hands and chest downward
            if (hand1) hand1.localPosition = hand1StartPos + downOffset;
            if (hand2) hand2.localPosition = hand2StartPos + downOffset;
            if (nippleLeft) nippleLeft.localPosition = nippleLStartPos + downOffset;
            if (nippleRight) nippleRight.localPosition = nippleRStartPos + downOffset;

            // Update UI slider
            if (depthSlider) depthSlider.value = t;

            // End of compression cycle
            if (compressProgress >= 2f)
            {
                isCompressing = false;

                // Reset all positions
                if (hand1) hand1.localPosition = hand1StartPos;
                if (hand2) hand2.localPosition = hand2StartPos;
                if (nippleLeft) nippleLeft.localPosition = nippleLStartPos;
                if (nippleRight) nippleRight.localPosition = nippleRStartPos;

                if (depthSlider) depthSlider.value = 0f;

                // Increase compression count
                compressionCount++;
                if (compressionCounterText)
                    compressionCounterText.text = "Compressions: " + compressionCount;

                // Play audio feedback
                if (compressionSound) compressionSound.Play();

                // Send haptic feedback (if using XR)
                // if (leftController) leftController.SendHapticImpulse(0.5f, 0.2f);
                // if (rightController) rightController.SendHapticImpulse(0.5f, 0.2f);
            }
        }
    }
}
  