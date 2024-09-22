using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Android;

public class Compass : MonoBehaviour
{
    public RectTransform compassNeedle;
    public TMP_Text logText;

    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            logText.text = "Requesting location permission";
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        else
        {
            EnableCompass();
        }
#else
        EnableCompass();
#endif
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (!Input.compass.enabled)
            {
                logText.text = "Enabling compass";
                EnableCompass();
            }
            else
            {
                UpdateCompass();
            }
        }
        else
        {
            logText.text = "Permission not granted";
            Debug.LogWarning("Permission not granted");
        }
#else
        UpdateCompass();
#endif
    }

    void EnableCompass()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;

        // Wait a short period for the compass to be enabled
        StartCoroutine(WaitForCompass());
    }

    System.Collections.IEnumerator WaitForCompass()
    {
        logText.text = "Waiting for compass to be enabled...";
        Debug.Log("Waiting for compass to be enabled...");

        yield return new WaitForSeconds(1);

        if (Input.compass.enabled)
        {
            logText.text = "Compass enabled";
            Debug.Log("Compass enabled");
        }
        else
        {
            logText.text = "Failed to enable compass";
            Debug.LogWarning("Failed to enable compass");
        }
    }

    void UpdateCompass()
    {
        if (Input.compass.enabled)
        {
            float heading = Input.compass.trueHeading;
            compassNeedle.rotation = Quaternion.Euler(0, 0, -heading);
            logText.text = "Compass heading: " + heading;
            Debug.Log("Compass heading: " + heading);
        }
        else
        {
            logText.text = "Compass not enabled";
            Debug.LogWarning("Compass not enabled");
        }
    }
}
