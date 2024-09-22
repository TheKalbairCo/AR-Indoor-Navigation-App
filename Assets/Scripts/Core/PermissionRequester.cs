using UnityEngine;
using UnityEngine.Android;

public class PermissionRequester : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        else
        {
            EnableCompass();
        }
#endif
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation) && !Input.compass.enabled)
        {
            EnableCompass();
        }
#endif
    }

    void EnableCompass()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;
    }
}
