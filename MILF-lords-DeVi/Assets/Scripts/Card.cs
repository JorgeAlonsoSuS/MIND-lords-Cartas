using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody invocation;


        private Rigidbody card;


        private void Awake()
        {
            card = GetComponent<Rigidbody>();
        }

        

        private void Invocar(Rigidbody invocacion)
        {
            var creature = Instantiate(invocacion);
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