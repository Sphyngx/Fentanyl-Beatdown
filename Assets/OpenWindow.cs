using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public OpenController openController;
    public void OpenComplete()
    {
        gameObject.SetActive(false);
        openController.OpenComplete();
    }
}