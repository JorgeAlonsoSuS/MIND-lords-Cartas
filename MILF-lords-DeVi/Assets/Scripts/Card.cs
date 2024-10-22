using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Card : MonoBehaviour
    {

        public event Action<Card, MonsterBehaviour> OnPlayed;

        private int cardIndex;
        private Player owner;
        private GameObject cardObject;

        private MonsterDC monsterData;
        public MonsterDC MonsterData => monsterData;

        [SerializeField]
        private Renderer renderer;

        public int Id => monsterData.Id;

        private bool summoned;

        internal void Init(MonsterDC monsterDC, Player player)
        {
            owner = player;
            monsterData = monsterDC;
            renderer.material.SetTexture("_MainTex", monsterDC.CardImage);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (summoned)
            {
                return;
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Tablero"))
            {
                StartCoroutine(Summon());
            }
        }

        private IEnumerator Summon()
        {
            summoned = true;

            yield return new WaitForSeconds( 0.25f);
            var newMonsterGameObject = Instantiate(monsterData.Prefab, transform.position,owner.transform.rotation);
            var monsterBehaviour = newMonsterGameObject.GetComponent<MonsterBehaviour>();
            monsterBehaviour.Init(owner);
            if (OnPlayed != null)
            {
                OnPlayed.Invoke(this, monsterBehaviour);
                Debug.Log("A�adido");
            }
            Destroy(this.gameObject);
        }

        public void ToggleInteractive(bool interactive)
        {
            GetComponent<Collider>().enabled = interactive;
        }
    }
}