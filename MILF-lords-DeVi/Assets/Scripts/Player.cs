using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{

    public enum PlayerType
    {
        Player,
        Rival
    }

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private int cardsToDraft;

        [SerializeField]
        private PlayerType playerId;

        public int CardsToDraft => cardsToDraft;

        public List<Card> Hand { get; private set; } = new List<Card>();
        public List<MonsterBehaviour> MonstersInGame { get; private set; } = new List<MonsterBehaviour>();

        public void removeMonster(MonsterBehaviour monster)
        {
            MonstersInGame.Remove(monster);
        }

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
            float CARD_HEIGHT = .25f;
            float MAX_CARDS_PER_ROW = 7f;
            float V_SPACING = .2f;

            float cardPerRow = Mathf.Min(MAX_CARDS_PER_ROW, Hand.Count);
            int rowCount = Mathf.CeilToInt(Hand.Count / cardPerRow);


            float width = cardPerRow * CARD_WIDTH + (Hand.Count - 1) * SPACING;
            float xStartPosition = -width / 2f;

            float height = rowCount * CARD_HEIGHT + (rowCount - 1) * V_SPACING;
            float zStartPosition = height / 2f;

            for (var i = 0; i < Hand.Count; i++)
            {
                int posInRow = (int)(i % cardPerRow);
                int currentRow = Mathf.FloorToInt(i / cardPerRow);

                var card = Hand[i];
                float xPos = xStartPosition + posInRow * (CARD_WIDTH + SPACING) + CARD_WIDTH / 2f;
                float zPos = zStartPosition - currentRow * (CARD_HEIGHT + V_SPACING) - CARD_HEIGHT / 2f; 
                card.transform.localPosition = new Vector3(xPos, .5f, zPos);
                if (playerId == PlayerType.Player)
                {
                    card.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

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