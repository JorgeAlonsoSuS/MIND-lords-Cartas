using Deck;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private MonsterBehaviour monster;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monster = GetComponent<MonsterBehaviour>();
    }

    private void Update()
    {
        if (monster.LockedMonster == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, monster.LockedMonster.transform.position) > monster.AttackRadius)
        {
            navMeshAgent.SetDestination(monster.LockedMonster.transform.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }
}
