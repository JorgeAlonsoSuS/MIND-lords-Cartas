using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private GameObject invocation;

        private Player1 player1;
        private Rigidbody card;


        private void Awake()
        {
            card = GetComponent<Rigidbody>();
            if (player1 == null)
                player1 = GameObject.Find("Player1").GetComponent<Player1>();
        }

        private void Invocar(GameObject invocacion)
        {
            var creature = Instantiate(invocacion);
            player1.AddMonster(creature);
            creature.transform.position = card.transform.position;
        }
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Colisiona");
            if (collision.gameObject.layer == LayerMask.NameToLayer("Tablero"))
            {
                Summon();
                Invocar(invocation);
                Destroy(gameObject);
            }
        }

        private IEnumerator Summon()
        {
            yield return new WaitForSeconds(1);
        }
    }
}