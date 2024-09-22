using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel1;
    public GameObject closeButton1;
    public GameObject popupPanel2;
    public GameObject closeButton2;

    public void ToggleImagePopup()
    {
        bool isActive = !popupPanel1.activeSelf;
        popupPanel1.SetActive(isActive);
        closeButton1.SetActive(isActive);
    }
    public void ToggleWarningPopup()
    { 
        bool isActive = !popupPanel2.activeSelf;
        popupPanel2.SetActive(isActive);
        closeButton2.SetActive(isActive);
    }

    public void ClosePopup1()
    {
        popupPanel1.SetActive(false);
        closeButton1.SetActive(false);
    }
    public void ClosePopup2()
    {
        popupPanel2.SetActive(false);
        closeButton2.SetActive(false);
    }


}
