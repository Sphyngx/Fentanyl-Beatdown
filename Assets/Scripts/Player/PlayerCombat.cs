using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Transform playerEyes; // The transform representing the player's eyes or where they are looking
    public LayerMask enemyLayer; // The layer containing the enemies
    public float visionDistance = 10f; // The maximum distance the player can see
    public float visionAngle = 45f; // The angle of the player's field of view
    public float visionRadius = 10f;
    Transform Enemy;
    public bool combatMode = false;
    public EnemyCombat enemyCombatTarget;
    RaycastHit hit;
    

    [Header("Script References")]
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    private UIManager uiManager;

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

    [Header("Kick Variables")]
    public bool CanKick;
    public bool IsKicking;

    // Start is called before the first frame update
    void Start()
    {
        // Find UIManager by searching for it in the scene with the tag
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

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
            playerMovement.CombatSpeed = true;
        }
        else
        {
            // Change the state to non-combat mode on player movement
            playerMovement.CombatSpeed = false;
        }

        Collider[] hitColliders = Physics.OverlapSphere(playerEyes.position, visionRadius, enemyLayer);
        foreach (Collider collider in hitColliders)
        {
            // Check if the detected object is an enemy
            if (IsEnemyInFieldOfView(collider.transform))
            {
                
                RaycastHit hit2;
                if (Physics.Raycast(transform.position, Enemy.transform.position - transform.position, out hit2))
                {
                    
                    if (hit2.collider.CompareTag("Enemy"))
                    {
                        // Enter combat mode when an enemy is within the player's field of view
                        CDetect();
                        return; // Exit the loop early since we only need to detect one enemy
                    }
                    
                }
                
            }
        }
        NoCDetect();
        // If no enemies are in sight, exit combat mode
        

        // Update stamina calculations
        UpdateStamina();

        // Update the player's current state
        currentState = playerInput.currentState.ToString();     
        
        
    }
    bool IsEnemyInFieldOfView(Transform enemyTransform)
    {
        Vector3 directionToEnemy = (enemyTransform.position - playerEyes.position).normalized;
        float angleToEnemy = Vector3.Angle(playerEyes.forward, directionToEnemy);

        // Check if the angle to the enemy is within the player's field of view
        if (angleToEnemy <= visionAngle / 2f)
        {
            // Check if the enemy is within the maximum distance
            RaycastHit hit;
            if (Physics.Raycast(playerEyes.position, directionToEnemy, out hit, visionDistance, enemyLayer))
            {
                // If the enemy is within the vision cone and not obstructed by obstacles, return true
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy = hit.collider.gameObject.transform;
                    return true;
                }
            }
        }

        return false;
    }
    public void CDetect()
    {
        combatMode = true;
    }

    public void NoCDetect()
    {
        combatMode = false;
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
            isAttacking = false;
            canAttack = true;
            // Return function
            yield break;
        }
        // Check if player or enemy is dead
        if (isDead || enemyCombatTarget.isDead)
        {
            isAttacking = false;
            canAttack = true;
            // Return function
            yield break;
        }
        // Attack UI
        if (playerInput.currentState == PlayerInput.AimState.Left)
        {
            uiManager.SetAttackLeft();
        }
        else if (playerInput.currentState == PlayerInput.AimState.Right)
        {
            uiManager.SetAttackRight();
        }
        else if (playerInput.currentState == PlayerInput.AimState.Middle)
        {
            uiManager.SetAttackMiddle();
        }

        // Deal damage to enemy
        enemyCombatTarget.TakeDamage((int)attackDamage);
        // Wait for 1 second
        yield return new WaitForSeconds(1);
        // Reset UI
        uiManager.ResetAim();
        // Set attacking to false
        isAttacking = false;
        // Set can attack to true
        canAttack = true;
    }

    public void Kick()
    {
        if (isAttacking)
        {
            Debug.Log("cant kick: isAttacking");
            return;
        }
        if (isBlocking)
        {
            Debug.Log("cant kick: isBlocking");
            return;
        }
        if (IsKicking)
        {
            Debug.Log("cant kick: IsKicking");
            return;
        }
        if (!combatMode)
        {
            Debug.Log("non combat kick");
            NonCombatKick();
        }
        if (combatMode)
        {
            Debug.Log("combat kick");
            CombatKick();
        }
    }
    public void NonCombatKick()
    {
        Vector3 spherePos = new Vector3(playerEyes.transform.position.x, playerEyes.transform.position.y - 1, playerEyes.transform.position.z) + playerEyes.transform.forward;
        Collider[] hitColliders = Physics.OverlapSphere(spherePos, 0.5f);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                Debug.Log(collider.name);
                if (rb == null)
                {
                    Debug.Log("Object does not have RigidBody");
                    continue;
                }
                if (rb != null && collider.CompareTag("Movable"))
                {
                    rb.AddForce(playerEyes.forward * 10, ForceMode.Impulse);
                }
                if (rb != null && collider.CompareTag("Destructable"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
    public void CombatKick()
    {
        Vector3 spherePos = new Vector3(playerEyes.transform.position.x, playerEyes.transform.position.y - 1, playerEyes.transform.position.z) + playerEyes.transform.forward;
        Collider[] hitColliders = Physics.OverlapSphere(spherePos, 0.5f);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                Debug.Log(collider.name);
                if (rb == null)
                {
                    Debug.Log("Object does not have RigidBody");
                    continue;
                }
                if (rb != null && collider.CompareTag("Enemy"))
                {
                    rb.AddForce(playerEyes.forward * 10, ForceMode.Impulse);
                }
            }
        }
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
