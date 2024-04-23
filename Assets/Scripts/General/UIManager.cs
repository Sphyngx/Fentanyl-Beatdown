using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject aimLeft;
    public GameObject aimMiddle;
    public GameObject aimRight;
    public GameObject enemyLeft;
    public GameObject enemyMiddle;
    public GameObject enemyRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    // Aim state
    public void SetAimLeft() {
        aimLeft.GetComponent<Image>().color = Color.white;
    }

    public void SetAimMiddle() {
        aimMiddle.GetComponent<Image>().color = Color.white;
    }

    public void SetAimRight() {
        aimRight.GetComponent<Image>().color = Color.white;        
    }


    // Attack
    public void SetAttackLeft() {
        aimLeft.GetComponent<Image>().color = Color.red;
    }

    public void SetAttackMiddle() {
        aimMiddle.GetComponent<Image>().color = Color.red;
    }

    public void SetAttackRight() {
        aimRight.GetComponent<Image>().color = Color.red;
    }


    // Block
    public void SetBlockLeft() {
        aimLeft.GetComponent<Image>().color = Color.blue;
    }

    public void SetBlockMiddle() {
        aimMiddle.GetComponent<Image>().color = Color.blue;
    }

    public void SetBlockRight() {
        aimRight.GetComponent<Image>().color = Color.blue;
    }


    // Parry
    public void SetParryLeft() {
        aimLeft.GetComponent<Image>().color = Color.yellow;
    }

    public void SetParryMiddle() {
        aimMiddle.GetComponent<Image>().color = Color.yellow;
    }

    public void SetParryRight() {
        aimRight.GetComponent<Image>().color = Color.yellow;
    }


    // Reset aim to 'neutral' state
    public void ResetAim() {
        aimLeft.GetComponent<Image>().color = Color.gray;
        aimMiddle.GetComponent<Image>().color = Color.gray;
        aimRight.GetComponent<Image>().color = Color.gray;
    }

    // Reset enemy aim to 'neutral' state
    public void ResetEnemyAim() {
        enemyLeft.GetComponent<Image>().color = Color.gray;
        enemyMiddle.GetComponent<Image>().color = Color.gray;
        enemyRight.GetComponent<Image>().color = Color.gray;
        // Hide enemy aim
        enemyLeft.SetActive(false);
        enemyMiddle.SetActive(false);
        enemyRight.SetActive(false);
    }


    // Enemy aim states
    public void SetEnemyAimLeft() {
        // Set enemy aim to visible
        enemyLeft.SetActive(true);
        enemyLeft.GetComponent<Image>().color = Color.white;
        enemyMiddle.GetComponent<Image>().color = Color.gray;
        enemyRight.GetComponent<Image>().color = Color.gray;
    }

    public void SetEnemyAimMiddle() {
        // Set enemy aim to visible
        enemyMiddle.SetActive(true);
        enemyLeft.GetComponent<Image>().color = Color.gray;
        enemyMiddle.GetComponent<Image>().color = Color.white;
        enemyRight.GetComponent<Image>().color = Color.gray;
    }

    public void SetEnemyAimRight() {
        // Set enemy aim to visible
        enemyRight.SetActive(true);
        enemyLeft.GetComponent<Image>().color = Color.gray;
        enemyMiddle.GetComponent<Image>().color = Color.gray;
        enemyRight.GetComponent<Image>().color = Color.white;        
    }


    // Enemy attack
    public void SetEnemyAttackLeft() {
        // Set enemy aim to visible
        enemyRight.SetActive(true);
        enemyLeft.GetComponent<Image>().color = Color.red;
    }

    public void SetEnemyAttackMiddle() {
        // Set enemy aim to visible
        enemyRight.SetActive(true);
        enemyMiddle.GetComponent<Image>().color = Color.red;
    }

    public void SetEnemyAttackRight() {
        // Set enemy aim to visible
        enemyRight.SetActive(true);
        enemyRight.GetComponent<Image>().color = Color.red;
    }

    
    // Enemy block
    public void SetEnemyBlockLeft() {
        enemyLeft.GetComponent<Image>().color = Color.blue;
    }

    public void SetEnemyBlockMiddle() {
        enemyMiddle.GetComponent<Image>().color = Color.blue;
    }

    public void SetEnemyBlockRight() {
        enemyRight.GetComponent<Image>().color = Color.blue;
    }

}
