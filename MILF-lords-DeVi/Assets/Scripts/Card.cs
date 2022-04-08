using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        public static List<MonsterDC> invocations;
        public static int pos=0;
        private MonsterDC invocation = new MonsterDC();
        private Player owner;
        private GameObject card;
        public Card(GameObject card, MonsterDC monster, Vector3 position, Player pOwner)
        {
            Instantiate(card);
            card.transform.position = position;
            this.invocation = monster;
            this.owner = pOwner;
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
            invocation.Instantiate(card.transform);
            Destroy(card);
        }
    }
}