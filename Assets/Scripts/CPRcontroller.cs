using UnityEngine;

public class CPRNippleCompressor : MonoBehaviour
{
    [Header("Chest Bones")]
    public Transform leftNipple;
    public Transform rightNipple;

    [Header("Hands")]
    public Transform leftHand;
    public Transform rightHand;

    [Header("Settings")]
    public float compressionDepth = 0.03f;
    public float handMoveDepth = 0.05f;
    public float compressionSpeed = 5f;

    private Vector3 leftNippleOriginalPos;
    private Vector3 rightNippleOriginalPos;
    private Vector3 leftHandOriginalPos;
    private Vector3 rightHandOriginalPos;

    private bool isCompressing = false;

    void Start()
    {
        if (leftNipple != null) leftNippleOriginalPos = leftNipple.localPosition;
        if (rightNipple != null) rightNippleOriginalPos = rightNipple.localPosition;
        if (leftHand != null) leftHandOriginalPos = leftHand.localPosition;
        if (rightHand != null) rightHandOriginalPos = rightHand.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) isCompressing = true;
        if (Input.GetKeyUp(KeyCode.Space)) isCompressing = false;

        // --- Nipple Compression ---
        if (leftNipple != null && rightNipple != null)
        {
            Vector3 leftTarget = isCompressing
                ? leftNippleOriginalPos - leftNipple.InverseTransformDirection(leftNipple.forward) * compressionDepth
                : leftNippleOriginalPos;

            Vector3 rightTarget = isCompressing
                ? rightNippleOriginalPos - rightNipple.InverseTransformDirection(rightNipple.forward) * compressionDepth
                : rightNippleOriginalPos;

            leftNipple.localPosition = Vector3.Lerp(leftNipple.localPosition, leftTarget, Time.deltaTime * compressionSpeed);
            rightNipple.localPosition = Vector3.Lerp(rightNipple.localPosition, rightTarget, Time.deltaTime * compressionSpeed);
        }

        // --- Hand Movement ---
        if (leftHand != null && rightHand != null)
        {
            Vector3 handOffset = new Vector3(-handMoveDepth, 0, 0); // Y-axis down by default

            Vector3 leftTarget = isCompressing
                ? leftHandOriginalPos + handOffset
                : leftHandOriginalPos;

            Vector3 rightTarget = isCompressing
                ? rightHandOriginalPos + handOffset
                : rightHandOriginalPos;

            leftHand.localPosition = Vector3.Lerp(leftHand.localPosition, leftTarget, Time.deltaTime * compressionSpeed);
            rightHand.localPosition = Vector3.Lerp(rightHand.localPosition, rightTarget, Time.deltaTime * compressionSpeed);
        }
    }
}
