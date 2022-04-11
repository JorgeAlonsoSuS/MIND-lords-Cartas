using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private int cardsToDraft;
        [SerializeField]
        private GameObject card;


        public int CardsToDraft => cardsToDraft;

        public List<Card> Hand { get; private set; } = new List<Card>();

        void Awake()
        {
            if (cardsToDraft > gameMonster.Count) cardsToDraft = gameMonster.Count;
            
        }
        public void AddCard(Card card)
        {
            Hand.Add(card);

            Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.29f, transform.position.z);
            card.transform.position = v;
        }
    }
}