using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        private GameObject invocation;
        private Player owner;
        private GameObject card;


        private void Awake()
        {
            if (owner == null)
                owner = GameObject.Find("Player1").GetComponent<Player>();
        }
        public void SetOwner(Player p)
        {
            owner = p;
        }
        public void SetInvocation(GameObject monster)
        {
            invocation = monster;
        }
        public GameObject GetMonster()
        {
            return invocation;
        }
        public void SetPrefab(GameObject body)
        {
            card = body;
        }
        public GameObject GetCard()
        {
            return card;
        }
        private void Invocar(GameObject invocacion)
        {
            var creature = Instantiate(invocacion);
            owner.AddMonster(creature);
            creature.transform.position = card.transform.position;
        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Colisiona");
            if (collision.gameObject.layer == LayerMask.NameToLayer("Tablero"))
            {
                StartCoroutine(Summon());
            }
        }

        private IEnumerator Summon()
        {
            yield return new WaitForSeconds( 0.25f);
            Invocar(invocation);
            Destroy(card);
        }
    }
}