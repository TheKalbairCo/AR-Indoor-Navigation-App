using UnityEngine;
using TMPro;

public class ARNavigationController : MonoBehaviour
{
    public AccelerometerDisplay stepCounter; // Reference to the step counter script
    public TextMeshProUGUI stepsToDestinationText; // TextMeshProUGUI for displaying steps to destination

    private int stepsToDestination = 100; // Steps to destination (static value for demonstration)

    void Start()
    {
        // Display initial steps to destination
        UpdateRemainingSteps();
    }

    void Update()
    {
        // Update remaining steps continuously (optional)
        UpdateRemainingSteps();
    }

    private void UpdateRemainingSteps()
    {
        // Get the current steps taken from the AccelerometerStepCounter script
        int currentStepsTaken = stepCounter.stepCount;

        // Calculate remaining steps
        int remainingSteps = Mathf.Max(0, stepsToDestination - currentStepsTaken); // Ensure remaining steps is not negative

        // Display remaining steps using TextMeshProUGUI
        if (stepsToDestinationText != null)
        {
            stepsToDestinationText.text = $"Remaining Steps: {remainingSteps}";
        }
    }
}
