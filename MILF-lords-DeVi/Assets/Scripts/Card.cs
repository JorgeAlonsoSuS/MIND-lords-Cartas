using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        private static List<MonsterDC> invocations = new List<MonsterDC>();
        private static int pos=0;
        private int cardIndex;
        private Player owner;
        private GameObject cardObject;
        public Card(GameObject card, MonsterDC monster, Vector3 position, Player pOwner, int index)
        {
            card.transform.position = position;
            Debug.Log(monster);
            invocations.Add(monster);
            owner = pOwner;
            cardObject = Instantiate(card);
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
            Instantiate(invocations[pos].GetPrefab(), transform.position,transform.rotation);
            pos++;
            Destroy(this.gameObject);
            Debug.Log("destruido");
        }
    }
}