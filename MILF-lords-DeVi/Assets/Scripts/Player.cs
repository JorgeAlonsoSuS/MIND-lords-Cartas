using System;
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


        public int CardsToDraft => cardsToDraft;

        public List<Card> Hand { get; private set; } = new List<Card>();
        public List<MonsterDC> monsterInGame { get; private set; } = new List<MonsterDC>();
        public List<MonsterBehaviour> MonstersInGame { get; private set; } = new List<MonsterBehaviour>();

        void Awake()
        {
            if (cardsToDraft > gameMonster.Count) cardsToDraft = gameMonster.Count;
            
        }
        public void AddCard(Card card)
        {
            Hand.Add(card);
            card.transform.SetParent(transform);
            Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.29f, transform.position.z);
            card.transform.position = v;
            card.OnPlayed += Card_OnPlayed;

            RepositionHand();
        }

        private void RepositionHand()
        {
            float SPACING = .1f;
            float CARD_WIDTH = 1f;
            float width = Hand.Count * CARD_WIDTH + (Hand.Count - 1) * SPACING;
            float startPosition = -width / 2f;

            for(var i = 0; i < Hand.Count; i++)
            {
                var card = Hand[i];
                float xPos = startPosition + (i * CARD_WIDTH + SPACING);
                card.transform.localPosition = new Vector3(xPos, .5f, 0);
            }
        }

        private void Card_OnPlayed(Card card, MonsterBehaviour obj)
        {
            Hand.Remove(card);
            MonstersInGame.Add(obj);
            RepositionHand();
        }

        public void ToggleCardsInteractive(bool interactive)
        {
            foreach (var card in Hand)
            {
                card.ToggleInteractive(interactive);
            }
        }
    }
}