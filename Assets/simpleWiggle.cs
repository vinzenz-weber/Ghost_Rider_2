using UnityEngine;

public class SimpleWiggle : MonoBehaviour
{
    public float wiggleSpeed = 5f; // Speed of the wiggle
    public float wiggleAmount = 10f; // Maximum rotation angle (degrees)

    private RectTransform rectTransform;
    private float startZRotation;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("SimpleWiggle script requires a RectTransform component on the GameObject.");
            enabled = false;
            return;
        }

        // Store the initial z-rotation
        startZRotation = rectTransform.eulerAngles.z;
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // Calculate the new z-rotation using a sine wave
            float zRotation = startZRotation + Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;

            // Apply the rotation to the RectTransform
            rectTransform.rotation = Quaternion.Euler(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, zRotation);
        }
    }
}