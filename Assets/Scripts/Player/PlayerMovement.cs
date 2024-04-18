using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float X = 0;
    float Z = 0;
    public int Speed = 6;
    bool sprint = false;
    public CharacterController Controller;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
            Speed = 12;
        }
        else
        {
            sprint = false;
            Speed = 6;
        }
        X = Input.GetAxisRaw("Horizontal");
        Z = Input.GetAxisRaw("Vertical");

        if (Z == -1 && sprint == false || X == 1 && sprint == false || X == -1 && sprint == false)
        {
            Speed -= 2;
        }
        else if (Z == -1 && sprint == true || X == 1 && sprint == true || X == -1 && sprint == true)
        {
            Speed -= 4;
        }
        Vector3 Movement = transform.right * X + transform.forward * Z; 
        Controller.Move(Movement * Speed * Time.deltaTime);
    }
}
