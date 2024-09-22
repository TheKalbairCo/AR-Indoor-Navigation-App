using UnityEngine;
using TMPro;

public class AccelerometerDisplay : MonoBehaviour
{
    public TextMeshProUGUI stepCounterText; // TextMeshProUGUI for displaying step count
    public int stepCount { get; private set; } // Step counter
    private Vector3 previousAcceleration; // Store the previous acceleration value
    private bool isStep = false; // Flag to check if a step was detected

    void Start()
    {
        // Initialize previousAcceleration with the current acceleration
        previousAcceleration = Input.acceleration;

        // Check if TextMeshProUGUI component is assigned
        if (stepCounterText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned!");
        }
    }

    void Update()
    {
        // Get the current acceleration
        Vector3 currentAcceleration = Input.acceleration;

        // Calculate the difference between the current and previous acceleration
        Vector3 deltaAcceleration = currentAcceleration - previousAcceleration;

        // Calculate the magnitude of the difference
        float deltaMagnitude = deltaAcceleration.magnitude;

        // Check if the difference exceeds the threshold to detect a step
        if (deltaMagnitude > 1.0f && !isStep) // Adjust threshold if needed
        {
            stepCount++;
            isStep = true; // A step was detected
        }
        else if (deltaMagnitude < 0.1f && isStep) // Adjust threshold if needed
        {
            isStep = false; // Reset the step detection
        }

        // Update the previous acceleration value
        previousAcceleration = currentAcceleration;

        // Update the TextMeshProUGUI component with the current step count
        if (stepCounterText != null)
        {
            stepCounterText.text = $"Step Count: {stepCount}";
        }
    }
}
