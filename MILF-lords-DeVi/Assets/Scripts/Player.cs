using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        private List<GameObject> invocations;
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private int cardsToDraft;
        private List<Card> deckCards = new List<Card>();
        private List<int> cardsSelected = new List<int>();
        void Awake()
        {
            invocations = new List<GameObject>();
            if (cardsToDraft > gameMonster.Count) cardsToDraft = gameMonster.Count;
            //Debug.Log(cardsToDraft);
            cardDraft();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void cardDraft()
        {
            for(int i = 0; i<cardsToDraft;)
            {
                int num =Random.Range(0, gameMonster.Count);
                if (AddCard(num))
                {
                    AddCarta(gameMonster[num], deckCards);
                    cardsSelected.Add(num);
                    Debug.Log(deckCards.Count);
                    i++;
                }
            }
        }
        public bool AddCard(int num)
        {
            for(int i=0; i < cardsSelected.Count; i++)
            {
                if (num == cardsSelected[i]) return false;
            }
            return true;
        }
        public void AddCarta(MonsterDC prefab, List<Card> deckCards)
        {
            Card carta = new Card();
            carta.SetInvocation(prefab.GetPrefab());
            carta.SetOwner(GetComponent<Player>());
            deckCards.Add(carta);
        }
        public void AddMonster(GameObject monster)
        {
            invocations.Add(monster);
            Debug.Log(invocations.Count);
        }
    }
}