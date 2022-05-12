using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Deck
{
    public class MonsterBehaviour : MonoBehaviour
    {
        private GameObject monster;
        [SerializeField]
        private int health = 10;
        [SerializeField]
        private int baseDamage = 5;
        [SerializeField]
        private float attackRadius = 1;
        [SerializeField]
        private float attackSpeed = 5;
        private Player owner;
        private NavMeshAgent navMeshAgent;
        private MonsterBehaviour lockedMonster =null;
        private bool canAtack = true;
        public MonsterBehaviour LockedMonster => lockedMonster;

        public float AttackRadius => attackRadius;

        void Start()
        {
            monster = GetComponent<GameObject>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health <= 0)  Destroy(monster); 
            if (navMeshAgent.isStopped) Atack();
        }
        private void Damage(int damage)
        {
            health -= damage;
        }
        private int AttackCalculation()
        {
            int damage = baseDamage;
            return damage;
        }
        public void Atack()
        {
            Debug.Log(health);
            if (canAtack)
            {
                lockedMonster.Damage(baseDamage);
                canAtack = false;
                StartCoroutine(CoolDown());
            }
        }

        internal void Init(Player owner, MonsterDC monsterDC)
        {
            this.owner = owner;
        }

        public void LockEnemy(MonsterBehaviour monsterL)
        {
            if(lockedMonster==null) lockedMonster = monsterL;
        }
        private IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(attackSpeed);
            canAtack = true;
        }
    }
}
