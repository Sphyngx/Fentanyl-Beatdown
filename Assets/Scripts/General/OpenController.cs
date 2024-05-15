using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenController : MonoBehaviour
{
    public GameObject OpenGif;
    public GameObject ComputorScreen;
    public string OpenName = string.Empty;

    public void ButtonClicked()
    {
        if (!OpenGif.activeInHierarchy && GameObject.FindGameObjectWithTag("Aplication"))
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
        Debug.Log("complete");
        
    }
}
