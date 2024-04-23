using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public enum AimState
    {
        Left,
        Right,
        Middle
    }

    public PlayerCombat playerCombatTarget;
    private UIManager uiManager;

    [Header("Enemy Health")]
    public bool isDead = false;
    public int enemyHealth = 100;

    [Header("Enemy Stamina")]
    public float enemyStamina = 100f;
    public bool canRegenStamina = true;

    [Header("Current Aim State")]
    public AimState selectedAim;

    [Header("Reaction Time")]
    public float reactionTime = 0.3f;
    private int ms = 0;
    public float parryPariod = 1.5f;

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
        // Find UIManager by searching for it in the scene with the tag
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        attackRateCopy = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();

        React();
        
    }

    public void TakeDamage(int damage) {
        // Cannot attack until a block is done
        isBlocking = true;
        isAttacking = false;
        enemyHealth -= damage;
        if (enemyHealth <= 0) {
            isDead = true;
        }
    }

    void UpdateStamina() {
        if (enemyStamina > 100)
        {
            enemyStamina = 100;
        }
        else if (enemyStamina < 0)
        {
            enemyStamina = 0;
            // Enemy is knocked out
            isDead = true;
        }
        attackRate = attackRateCopy - (enemyStamina / 100);
    }

    public void React() {
        // Every 0.3 seconds, the enemy will react
        ms += (int)(Time.deltaTime * 1000);
        if (ms >= reactionTime * 1000) {
            ms = 0;
            // Move current state to the selected aim
            selectedAim = (EnemyCombat.AimState)playerCombatTarget.playerInput.currentState;
            // If the player is in parry state, block
            if (playerCombatTarget.isPerfectParry) {
                Attack();
            }
            // If the player is attacking, block
            else if (playerCombatTarget.isAttacking) {
                Block();
            }
            // If the player is not attacking, attack
            else {
                Attack();
            }
        }

    }

    void Attack() {
        // If player is attacking
        if (isAttacking)
        {
            // Log
            Debug.Log(gameObject.name + ": Already attacking");
            return;
        }
        // If player can attack
        if (!canAttack)
        {
            // Log
            Debug.Log(gameObject.name + ": Can't attack");
            return;
        }
        StartCoroutine(AttackRoutine());
    }

    void Block() {
        // If player is attacking
        if (isBlocking)
        {
            // Log
            Debug.Log(gameObject.name + ": Already blocking");
            return;
        }
        // If player can attack
        if (!canBlock)
        {
            // Log
            Debug.Log(gameObject.name + ": Can't block");
            return;
        }
        StartCoroutine(BlockRoutine());
    }

    IEnumerator BlockRoutine() {
        // Set blocking to true
        isBlocking = true;
        // Set can block to false
        canBlock = false;
        // Drain stamina
        enemyStamina -= attackStamina;
        Debug.Log(gameObject.name + ": Blocking");
        // Wait for 0.5 second
        yield return new WaitForSeconds(0.5f);
        // Set blocking to false
        isBlocking = false;
        // Set can block to true
        canBlock = true;
    }

    IEnumerator AttackRoutine() {
        // Set attacking to true
        isAttacking = true;
        // Set can attack to false
        canAttack = false;
        // Drain stamina
        enemyStamina -= attackStamina;
        // Set perfect parry to true
        isPerfectParry = true;
        // Wait for 0.5 second
        // Move to random aim state but not the same as the player
        selectedAim = (AimState)Random.Range(0, 3);
        while (selectedAim == (EnemyCombat.AimState)playerCombatTarget.playerInput.currentState) {
            selectedAim = (AimState)Random.Range(0, 3);
        }
        // Change UI
        if (selectedAim == AimState.Left)
        {
            uiManager.SetEnemyAttackLeft();
        }
        else if (selectedAim == AimState.Right)
        {
            uiManager.SetEnemyAttackRight();
        }
        else if (selectedAim == AimState.Middle)
        {
            uiManager.SetEnemyAttackMiddle();
        }
        // Wait for parryPariod second
        yield return new WaitForSeconds(parryPariod);
        // Check if enemy is blocking
        if (playerCombatTarget.isBlocking)
        {
            // Log
            Debug.Log(gameObject.name + ": Player is blocking");
            // Drain stamina
            playerCombatTarget.playerStamina -= attackDamage;
            // Disable perfect parry
            isPerfectParry = false;
            // Set attacking to false
            isAttacking = false;
            // Set can attack to true
            canAttack = true;
            // Reset enemy ui
            uiManager.ResetEnemyAim();
            // Return function
            yield break;
        }
        // Check if enemy is attacking (parry)
        if (playerCombatTarget.isAttacking && isPerfectParry)
        {
            // Log
            Debug.Log(gameObject.name + ": Perfect parry");
            // Disable perfect parry
            isPerfectParry = false;
            // Set attacking to false
            isAttacking = false;
            // Set can attack to true
            canAttack = true;
            // Reset enemy ui
            uiManager.ResetEnemyAim();
            // Change UI
            if (selectedAim == AimState.Left)
            {
                uiManager.SetParryLeft();
            }
            else if (selectedAim == AimState.Right)
            {
                uiManager.SetParryRight();
            }
            else if (selectedAim == AimState.Middle)
            {
                uiManager.SetParryMiddle();
            }
            // Return function
            yield break;
        }
        // Check if player or enemy is dead
        if (isDead || playerCombatTarget.isDead)
        {
            // Log
            Debug.Log(gameObject.name + ": Player or enemy is dead");
            // Disable perfect parry
            isPerfectParry = false;
            // Set attacking to false
            isAttacking = false;
            // Set can attack to true
            canAttack = true;
            // Reset enemy ui
            uiManager.ResetEnemyAim();
            // Return function
            yield break;
        }
        // Set perfect parry to false
        isPerfectParry = false;
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        playerCombatTarget.TakeDamage((int)attackDamage);
        // Debug
        Debug.Log(gameObject.name + ": Attacking on " + selectedAim + " aim state. (Damage: " + attackDamage + ")");
        // Set attacking to false
        isAttacking = false;
        // Set can attack to true
        canAttack = true;
        // Reset enemy ui
        uiManager.ResetEnemyAim();
    }

}
