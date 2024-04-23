using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera Camera;
    RaycastHit hit;
    public GameObject ComputorScreen;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5) && Input.GetKeyDown("e"))
        {
            if (hit.collider.CompareTag("Computor"))
            {
                ComputorScreen.SetActive(true);
            }
        }
    }
}

