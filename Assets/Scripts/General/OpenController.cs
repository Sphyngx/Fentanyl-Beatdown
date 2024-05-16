using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenController : MonoBehaviour
{
    public GameObject OpenGif;
<<<<<<< HEAD
    public GameObject ComputorScreen;
    public string OpenName = string.Empty;
    public GameObject MailApp;
    public GameObject MRShadyApp;
    public GameObject SettingsApp;
<<<<<<< Updated upstream
=======
=======
>>>>>>> parent of 01c6a40 (menu v2)
>>>>>>> Stashed changes

    public void ButtonClicked()
    {
        if (!OpenGif.activeInHierarchy)
        {
            OpenGif.SetActive(true);
        }
    }
<<<<<<< HEAD

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
=======
>>>>>>> parent of 01c6a40 (menu v2)
}
