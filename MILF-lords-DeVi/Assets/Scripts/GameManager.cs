using Deck;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Deck
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private MonsterDC[] monters;

        [SerializeField]
        private Player[] players;

        [SerializeField]
        private CardFactory cardFactory;

        private void Awake()
        {
            DrawCards(players[0]);
            DrawCards(players[1]);
        }

        private void DrawCards(Player player)
        {
            for (int i = 0; i < player.CardsToDraft; i++)
            {
                var handCardIds = player.Hand.Select(c => c.Id);
                Card newCard = cardFactory.DrawCard(player, handCardIds);
                player.AddCard(newCard);
            }
        }

    }
}