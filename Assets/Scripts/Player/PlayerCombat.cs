using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Parry: when player attacks in perfect timing (when enemy attacks). Then player will attack faster and stronger for next attack.
    // Block: blocks the enemys attack.
    // Attack: player attacks the enemy.
    // Dodge: WIP
    // Unblockable: Breaks the block of the enemy if hit.

    public bool combatMode = false;

    [Header("Script References")]
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;

    [Header("Player Health")]
    public bool isDead = false;
    public int playerHealth = 100;

    [Header("Player Stamina")]
    public int playerStamina = 100;

    [Header("Current State")]
    public string currentState;

    [Header("Attack Variables")]
    public bool canAttack;
    public bool isAttacking;
    public float attackDamage = 10f;
    public float attackRange = 0.5f;
    public float attackRate = 2f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combatMode)
        {
            // Change the state to combat mode on player movement
            playerMovement.combatMode = true;
        }
        else
        {
            // Change the state to non-combat mode on player movement
            playerMovement.combatMode = false;
        }

        // Update stamina calculations
        UpdateStamina();


        
    }

    public void TakeDamage(int damage) {
        // Cannot attack until a block is done
        canAttack = false;

        // Subtract damage from player health
        playerHealth -= damage;

        // If player health is less than or equal to 0
        if (playerHealth <= 0)
        {
            // Set is dead to true
            isDead = true;
            // Log
            Debug.Log("Player is dead");
        }

        // Log
        Debug.Log("Took " + damage + " damage");
    }

    void UpdateStamina() {
        if (playerStamina > 100)
        {
            playerStamina = 100;
        }
        else if (playerStamina < 0)
        {
            playerStamina = 0;
            // Player is knocked out
            isDead = true;
        }
        // Update attack speed based on stamina
        attackRate = playerStamina / 100;
    }


    public void Attack()
    {
        // If player is not in combat mode
        if (!combatMode)
        {
            // Log
            Debug.Log("Not in combat mode");
            return;
        }
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
