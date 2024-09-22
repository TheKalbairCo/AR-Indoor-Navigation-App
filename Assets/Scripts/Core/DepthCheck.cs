using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARSubsystems;

public class DepthCheck : MonoBehaviour
{
    public TMP_Text warningText; 
    public TMP_Text deviceInfoText; 

    // List of device models that support the Depth API
    private List<string> supportedDevices = new List<string>
    {
        "OPPO CPH2249"
    };

    void Start()
    {
        CheckDepthSupport();
    }

    void CheckDepthSupport()
    {
        // Check if the current device model is in the list of supported devices
        string deviceModel = SystemInfo.deviceModel;
        bool isDeviceSupported = supportedDevices.Contains(deviceModel);

        // Display the detected device model information
        if (deviceInfoText != null)
        {
            deviceInfoText.text = "Device Model: " + deviceModel;
        }

        // Check if the depth API is supported by the occlusion subsystem
        List<XROcclusionSubsystemDescriptor> descriptors = new List<XROcclusionSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(descriptors);

        bool depthSupported = false;
        foreach (var descriptor in descriptors)
        {
            if (descriptor.environmentDepthImageSupported == Supported.Supported ||
                descriptor.humanSegmentationDepthImageSupported == Supported.Supported)
            {
                depthSupported = true;
                break;
            }
        }

        // Combine the checks for depth support and device model
        if (!depthSupported && !isDeviceSupported)
        {
            if (warningText != null)
            {
                warningText.text = "Warning: Your device does not support the Depth API. Navigation accuracy might be affected and some features may not be supported";
                warningText.fontSize = 45;
            }
            Debug.LogWarning("Warning: Your device does not support the Depth API. Navigation accuracy might be affected and some features may not be supported" +"");
        }
        else
        {
            Debug.Log("Depth API is supported and running.");
        }
    }
}
