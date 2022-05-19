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
            float SPACING = .01f;
            float CARD_WIDTH = .1f;
            float CARD_HEIGHT = .15f;
            float MAX_CARDS_PER_ROW = 5f;

            float cardPerRow = Mathf.Min(MAX_CARDS_PER_ROW, Hand.Count);
            int rowCount = Mathf.CeilToInt(Hand.Count / cardPerRow);

            Debug.Log("rows : " + rowCount);

            float width = cardPerRow * CARD_WIDTH + (Hand.Count - 1) * SPACING;
            float startPosition = -width / 2f;

            for(var i = 0; i < Hand.Count; i++)
            {
                int posInRow = (int)(i % cardPerRow);
                int currentRow = Mathf.FloorToInt(i / cardPerRow);

                var card = Hand[i];
                float xPos = startPosition + posInRow * CARD_WIDTH + posInRow * SPACING + CARD_WIDTH / 2f;
                float zPos = currentRow * (CARD_HEIGHT + SPACING); 
                Debug.Log("xPos : " + xPos);
                //if (xPos < -0.4f) card.transform.localPosition = new Vector3(xPos, .5f, -0.2f);
                //else
                card.transform.localPosition = new Vector3(xPos, .5f, -zPos);

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