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
    
    [Header("Attack")]
    public bool isAttacking;
    public bool canAttack;
    
    [Header("Block")]
    public bool isBlocking;

    
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
        }
        // If player presses mouse button 0 (Attack)
        if (Input.GetMouseButtonDown(0))
        {
            // Log
            Debug.Log("Pressed attack button");
            Attack();
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

    void Attack() {
        // If player is attacking
        if (isAttacking)
        {
            // Log
            Debug.Log("Already attacking");
            return;
        }
        // If player can attack
        if (!canAttack)
        {
            // Log
            Debug.Log("Can't attack");
            return;
        }
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine() {
        // Set attacking to true
        isAttacking = true;
        // Set can attack to false
        canAttack = false;
        // Wait for 1 second
        yield return new WaitForSeconds(1);
        // Set attacking to false
        isAttacking = false;
        // Set can attack to true
        canAttack = true;
    }
}
