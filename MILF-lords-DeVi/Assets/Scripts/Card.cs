using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck {
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody invocacion;

        [SerializeField]
        private int health;

        private Rigidbody card;


        private void Awake()
        {
            card = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            
        }

        private void Invocar(Rigidbody invocacion)
        {
            var creatura = Instantiate(invocacion);
            creatura.transform.position = card.transform.position;
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Colisiona");
            if(collision.gameObject.layer == LayerMask.NameToLayer("Tablero"))
            {
                Invocar(invocacion);
                Destroy(gameObject);
            }
        }
    }
}
