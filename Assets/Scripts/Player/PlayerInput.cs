using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateConstants
{
    public const string Left = "Left";
    public const string Right = "Right";
    public const string Middle = "Middle";
}

public class PlayerInput : MonoBehaviour
{
    [Header("Selected State")]
    public string currentState;

    [Header("Script References")]
    public PlayerCombat playerCombat;

    
    void Start()
    {
        currentState = StateConstants.Middle; // Set the initial state
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check all mouse stuff using a function
        UpdateMouse();

        if (Input.GetKeyDown(KeyCode.E)) // Just debug
        {
            // Log
            Debug.Log("State: " + currentState);
            // TOGGLE COMBAT MODE (WIP)
            if (playerCombat.combatMode)
            {
                playerCombat.combatMode = false;
            }
            else if (!playerCombat.combatMode)
            {
                playerCombat.combatMode = true;
            }
        }
        // If player presses mouse button 0 (Attack)
        if (Input.GetMouseButtonDown(0))
        {
            // Log
            Debug.Log("Pressed attack button");
            playerCombat.Attack();
        }
        // If player presses mouse button 1 (Block)
        if (Input.GetMouseButtonDown(1))
        {
            // Log
            Debug.Log("Pressed block button");
        }
    }
    
    void UpdateMouse() {
        // Get the mouse position
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = Input.GetAxis("Mouse X");

        // If mouse is moved at all
        if (mouseX == 0 && mouseY == 0)
        {
            return;
        }

        // Debug
        Debug.Log("Mouse X: " + mouseX + " Mouse Y: " + mouseY);

        // If mouse is moved left then change the state to left
        if (mouseY < 0)
        {
            currentState = StateConstants.Left;
        }
        // If mouse is moved right then change the state to right
        else if (mouseY > 0)
        {
            currentState = StateConstants.Right;
        }
        // If mouse is moved up then change the state to middle
        else 
        {
            currentState = StateConstants.Middle;
        }
    }

}
