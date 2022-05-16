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
            Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.29f, transform.position.z);
            card.transform.position = v;
            card.OnPlayed += Card_OnPlayed;
        }

        private void Card_OnPlayed(MonsterBehaviour obj)
        {
            MonstersInGame.Add(obj);
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