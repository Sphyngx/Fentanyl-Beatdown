using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public GameObject OpenGif;
    public GameObject ComputorScreen;
    public string OpenName = string.Empty;
    public GameObject MailApp;
    public GameObject MRShadyApp;
    public GameObject SettingsApp;

    public void ButtonClicked()
    {
        if (!OpenGif.activeInHierarchy)
        {
            OpenGif.SetActive(true);
            OpenName = EventSystem.current.currentSelectedGameObject.name;
        }
    }

    public void CloseComputorClicked()
    {
         ComputorScreen.SetActive(false);
    }

    public void OpenComplete()
    {
        Debug.Log("Load anim complete");
        if (OpenName == "Mail")
        {
            MailApp.SetActive(true);
        }
        else if (OpenName == "MRShady")
        {
            MRShadyApp.SetActive(true);
        }
        else if (OpenName == "Settings")
        {
            SettingsApp.SetActive(true);
        }
    }

    public void MailClose()
    {
        MailApp.SetActive(false);
    }
}
