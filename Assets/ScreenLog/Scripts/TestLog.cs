using UnityEngine;

using NeptunDigital;

public class TestLog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //send the debug message
        ScreenLog.Instance.SendEvent(TextType.Debug, "Debug message.");

        //send the warning message
        ScreenLog.Instance.SendEvent(TextType.Warning, "Warning message.");

        //send the error message
        ScreenLog.Instance.SendEvent(TextType.Error, "Error message.");

    }
}
