using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private GameObject invocation;

        [SerializeField]
        private Player owner;
        private Rigidbody card;


        private void Awake()
        {
            card = GetComponent<Rigidbody>();
            if (owner == null)
                owner = GameObject.Find("Player1").GetComponent<Player>();
        }

        private void Invocar(GameObject invocacion)
        {
            var creature = Instantiate(invocacion);
            owner.AddMonster(creature);
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