using UnityEngine;
using System.Collections;

public class UrlButton : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://developers.google.com/ar/devices");
        Debug.Log("is this working?");
    }

}