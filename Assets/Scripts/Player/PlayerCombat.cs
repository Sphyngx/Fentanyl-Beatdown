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


    // Attack phase:
    // 1. Check if player is in combat mode
    // 2. Check if player can attack
    // 3. Check if player is attacking
    // 4. Set attacking to true
    // 5. Set can attack to false
    // 6. Drain stamina
    // 7. Check if enemy is blocking
    // 7.1 If enemy is blocking, drain stamina
    // 8. Check if enemy is attacking (parry)
    // 8.1 Return function
    // 9. Check if player is dead
    // 9.1 If player is dead, return function
    // 10. Wait for 1 second
    // 11. Set attacking to false
    // 12. Set can attack to true

    
    public bool combatMode = false;
    public EnemyCombat enemyCombatTarget;

    [Header("Script References")]
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;

    [Header("Player Health")]
    public bool isDead = false;
    public int playerHealth = 100;

    [Header("Player Stamina")]
    public float playerStamina = 100f;
    public bool canRegenStamina = true;
    public float staminaRegenRate = 5f;

    [Header("Current State")]
    public string currentState;

    [Header("Attack Variables")]
    public bool canAttack;
    public bool isAttacking;
    public bool isPerfectParry;
    public float attackDamage = 10f;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float attackRateCopy;
    public float attackStamina = 12f;

    [Header("Block Variables")]
    public bool canBlock;
    public bool isBlocking;


    // Start is called before the first frame update
    void Start()
    {
        attackRateCopy = attackRate;

        // Kick start the stamina regen
        StartCoroutine(RegenStamina());
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
        //canAttack = false;

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

    IEnumerator RegenStamina() {
        while (!isDead)
        {
            if (canRegenStamina)
            {
                // Regen stamina
                playerStamina += staminaRegenRate;
                // Wait for 1 second
                yield return new WaitForSeconds(1);
            }
        }
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
        // Update attack speed based on stamina (if player has 100 stamina, attack rate is 2 seconds, if player has 50 stamina, attack rate is 1 second, etc.
        attackRate = attackRateCopy - (playerStamina / 100);
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
        // Drain stamina
        playerStamina -= attackStamina;
        // Check if enemy is blocking
        if (enemyCombatTarget.isBlocking)
        {
            // Drain stamina
            enemyCombatTarget.enemyStamina -= attackDamage;
        }
        // Check if enemy is attacking (parry)
        if (enemyCombatTarget.isAttacking && isPerfectParry)
        {
            // Return function
            yield break;
        }
        // Check if player or enemy is dead
        if (isDead || enemyCombatTarget.isDead)
        {
            // Return function
            yield break;
        }
        enemyCombatTarget.TakeDamage((int)attackDamage);
        // Wait for 1 second
        yield return new WaitForSeconds(1);
        // Set attacking to false
        isAttacking = false;
        // Set can attack to true
        canAttack = true;
    }

    // Attack phase:
    // 1. Check if player is in combat mode
    // 2. Check if player can attack
    // 3. Check if player is attacking
    // 4. Set attacking to true
    // 5. Set can attack to false
    // 6. Drain stamina
    // DONE ^
    // 7. Check if enemy is blocking 
    // 7.1 If enemy is blocking, drain stamina
    // 8. Check if enemy is attacking (parry)
    // 8.1 Return function
    // 9. Check if player is dead
    // 9.1 If player is dead, return function
    // 10. Wait for 1 second
    // 11. Set attacking to false
    // 12. Set can attack to true
}
