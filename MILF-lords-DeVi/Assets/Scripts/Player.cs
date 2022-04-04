using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        private List<GameObject> invocations;
        [SerializeField]
        private List<GameObject> gameCards;
        [SerializeField]
        private int cardsToDraft;
        private List<GameObject> deckCards;
        private int[] cardsSelected;
        void Awake()
        {
            invocations = new List<GameObject>();
            if (cardsToDraft > gameCards.Count) cardsToDraft = gameCards.Count;
            Debug.Log(cardsToDraft);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void cardDraft()
        {
            for(int i = 0; i<cardsToDraft;)
            {
                int num =Random.Range(0, gameCards.Count);
                if (AddCard(num))
                {
                    deckCards.Add(gameCards[num]);
                    Debug.Log(deckCards.Count);
                    i++;
                }
            }
        }
        public bool AddCard(int num)
        {
            for(int i=0; i < cardsSelected.Length; i++)
            {
                if (num == cardsSelected[i]) return false;
            }
            return true;
        }
        public void AddMonster(GameObject monster)
        {
            invocations.Add(monster);
            Debug.Log(invocations.Count);
        }
    }

}

