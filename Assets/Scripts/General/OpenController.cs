using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenController : MonoBehaviour
{
    public GameObject OpenGif;

    public void ButtonClicked()
    {
        if (!OpenGif.activeInHierarchy)
        {
            OpenGif.SetActive(true);
        }
    }
}
