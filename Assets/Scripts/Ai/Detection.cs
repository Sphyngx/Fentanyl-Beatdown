using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // If the object that enters the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // Make raycast to the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
            {
                // If the raycast hits the player
                if (hit.collider.tag == "Player")
                {
                    // Log
                    Debug.Log("Player detected");
                }
            }
        }
    }

    
}
