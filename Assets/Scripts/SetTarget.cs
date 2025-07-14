using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTarget : MonoBehaviour
{
    BetterAgent[] betterAgents;
    NavMeshAgent[] agents;

    void Update()
    {
        //Find all 
        betterAgents = FindObjectsOfType<BetterAgent>();
        agents = FindObjectsOfType<NavMeshAgent>();

        //Set each agent's target to wherever this object is
        if(betterAgents != null)
        {
            foreach (var betterAgent in betterAgents)
            {
                betterAgent.SetDestination(transform.position);
            }
        }

        if (agents != null)
        {
            foreach (var agent in agents)
            {
                agent.SetDestination(transform.position);
            }
        }
    }
}
