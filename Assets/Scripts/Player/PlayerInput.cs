using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public enum AimState
    {
        Left,
        Right,
        Middle
    }

    public AimState currentState;

    [Header("Script References")]
    public PlayerCombat playerCombat;
    private UIManager uiManager;
    
    void Start()
    {
        // Find UIManager by searching for it in the scene with the tag
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        currentState = AimState.Middle; // Set the initial state
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check all mouse stuff using a function
        UpdateMouse();

        if (Input.GetKeyDown(KeyCode.P)) // Just debug
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
            playerCombat.Attack();
        }
        // If player presses mouse button 1 (Block)
        if (Input.GetMouseButtonDown(1))
        {
            // Log
            Debug.Log("Pressed block button");
            // Block UI
            if (currentState == AimState.Left)
            {
                uiManager.SetBlockLeft();
            }
            else if (currentState == AimState.Right)
            {
                uiManager.SetBlockRight();
            }
            else if (currentState == AimState.Middle)
            {
                uiManager.SetBlockMiddle();
            }
        }
        //if player presses "E" (Kick)
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerCombat.Kick();
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
        //Debug.Log("Mouse X: " + mouseX + " Mouse Y: " + mouseY);

        // If mouse is moved left then change the state to left
        if (mouseY < 0)
        {
            currentState = AimState.Left;
            uiManager.SetAimLeft();
        }
        // If mouse is moved right then change the state to right
        else if (mouseY > 0)
        {
            currentState = AimState.Right;
            uiManager.SetAimRight();
        }
        // If mouse is moved up then change the state to middle
        else 
        {
            currentState = AimState.Middle;
            uiManager.SetAimMiddle();
        }
    }

}
