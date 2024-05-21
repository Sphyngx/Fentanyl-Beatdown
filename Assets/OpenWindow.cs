using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public ButtonController buttonController;
    public void OpenComplete()
    {
        gameObject.SetActive(false);
        buttonController.OpenComplete();
    }
}