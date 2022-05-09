using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class MonsterBehaviour : MonoBehaviour
    {
        private Rigidbody monster;
        [SerializeField]
        private int health = 10;
        [SerializeField]
        private int baseDamage = 5;
        [SerializeField]
        private float attackRadius = 1;
        private Player owner;
        private MonsterBehaviour lockedMonster =null;
        public MonsterBehaviour LockedMonster => lockedMonster;

        public float AttackRadius => attackRadius;

        void Start()
        {
            monster = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health <= 0) Destroy(monster);
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
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != monster.gameObject.layer)
            {
                //Llamamos a la funcion attackCalculation y le hacemos daño al enemigo.
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
        public void MoveToEnemy(MonsterDC monster)
        {

        } 
    }
}
