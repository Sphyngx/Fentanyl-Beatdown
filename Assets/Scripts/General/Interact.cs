using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera Camera;
    RaycastHit hit;
    public GameObject ComputorScreen;
    public bool computor = false;
    private PlayerMovement Movement;
    
    // Start is called before the first frame update
    void Start()
    {
        Movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5) && hit.collider.CompareTag("Computor"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                computor = true;
                ComputorScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Movement.CanMove = false;
            }
        }

        if (!ComputorScreen.activeInHierarchy)
        {
            computor = false;
            Cursor.lockState = CursorLockMode.Locked;
            Movement.CanMove = true;
        }
    }
}

