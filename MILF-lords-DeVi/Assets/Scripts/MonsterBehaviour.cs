using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class MonsterBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody monster;
        [SerializeField]
        private int initialHealth = 10;
        [SerializeField]
        private int baseDamage = 5;
        private int health;
        void Start()
        {
            monster = GetComponent<Rigidbody>();
            health = initialHealth;
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
    }
}
