using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Deck
{
    public class MonsterBehaviour : MonoBehaviour
    {
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
        private MonsterBehaviour lockedMonster = null;
        private bool canAtack = true;
        private GameManager gameManager;
        public MonsterBehaviour LockedMonster => lockedMonster;
        
        public int Health => health;

        public float AttackRadius => attackRadius;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health <= 0)  StartCoroutine(terrorificDeath());
            if (LockedMonster != null)
            {
                if (!lockedMonster.owner.MonstersInGame.Contains(lockedMonster))
                {
                    LockEnemy();
                    Debug.Log(lockedMonster);
                }
            } 
            else
            {
                LockEnemy();
            } 
            if (navMeshAgent.isStopped && canAtack) Atack();
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
            if (lockedMonster != null)
            {
                if (Vector3.Distance(this.transform.position, lockedMonster.transform.position)<=attackRadius)
                {
                    lockedMonster.Damage(baseDamage);
                    canAtack = false;
                    StartCoroutine(CoolDown());
                }
            }
                
        }

        internal void Init(Player owner, MonsterDC monsterDC)
        {
            this.owner = owner;
        }

        public void LockEnemy()
        {
           lockedMonster = gameManager.Punch(owner, this);
        }
        private IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(attackSpeed);
            canAtack = true;
        }

        private IEnumerator terrorificDeath()
        {
            yield return new WaitForSeconds(0.5f);
            owner.removeMonster(this);
            Destroy(gameObject);
        }
    }
}
