using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deck
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private GameObject cardOriginal;

        public Card DrawCard(Player player, IEnumerable<int> excludeCardIds)
        {
            var newCard = Instantiate(cardOriginal);
            var card = newCard.GetComponent<Card>();
            var validCards = gameMonster.Where(c => !excludeCardIds.Contains(c.Id)).ToList();
            var randomIndex = Random.Range(0, validCards.Count());
            card.Init(gameMonster[randomIndex], GetComponent<Player>());
            return card;
        }
    }

}