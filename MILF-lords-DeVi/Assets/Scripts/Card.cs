using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        private GameObject invocation;
        private Player owner;
        private Rigidbody card;


        private void Awake()
        {
            card = GetComponent<Rigidbody>();
            if (owner == null)
                owner = GameObject.Find("Player1").GetComponent<Player>();
        }
        public void SetOwner(Player p)
        {
            if(p!=null) owner = p;
        }
        public void SetInvocation(GameObject monster)
        {
            if(monster!=null)
            invocation = monster;
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
            Destroy(gameObject);
        }
    }
}