using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Transform AI;
    private GameObject Player;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        AI = GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(AI.transform.position, Player.transform.position - AI.transform.position, out hit))
        {
            
            Agent.SetDestination(Player.transform.position);
        }
    }
}

