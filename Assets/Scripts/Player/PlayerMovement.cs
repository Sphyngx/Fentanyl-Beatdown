using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public PlayerCombat playercombat;
    float X = 0;
    float Z = 0;
    public int Speed = 6;
    bool sprint = false;
    public CharacterController Controller;
    public bool CombatSpeed = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !CombatSpeed)
        {
            sprint = true;
            Speed = 12;
        }
        else
        {
            sprint = false;
            if (CombatSpeed)
            {
                Speed = 3;
            }
            else
            {
                Speed = 6;
            }

        }
        X = Input.GetAxisRaw("Horizontal");
        Z = Input.GetAxisRaw("Vertical");

        if (Z == -1 && !sprint && !CombatSpeed || X == 1 && !sprint && !CombatSpeed || X == -1 && !sprint && !CombatSpeed)
        { 
                Speed -= 2;
        }
        else if (Z == -1 && sprint && !CombatSpeed || X == 1 && sprint && !CombatSpeed || X == -1 && sprint && !CombatSpeed)
        {
            Speed -= 4;
        }
        Vector3 Movement = transform.right * X + transform.forward * Z; 
        Controller.Move(Movement * Speed * Time.deltaTime);
    }
}
