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
        private bool check = false;

        private void Awake()
        {
            DrawCards(players[0]);
            DrawCards(players[1]);
        }

        private void Update()
        {
            if (!check)
            {
                LockMonsters();
            }
            else
            {
                StartCoroutine(CheckAgain());
            }
        }
        private void LockMonsters()
        {
            if (players[0].MonstersInGame.Count>0 && players[1].MonstersInGame.Count>0) {
                for (int j = 0; j < players[0].MonstersInGame.Count; j++) {
                    if (players[0].MonstersInGame[j].LockedMonster == null)
                    {
                        int pos = 0;
                        for (int i = 0; i < players[1].MonstersInGame.Count; i++)
                        {
                            Debug.Log(calcularDistancia(players[1].MonstersInGame[j].transform.position, players[1].MonstersInGame[i].transform.position));
                        }
                    }
                }
            }
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
        private IEnumerator CheckAgain()
        {
            yield return new WaitForSeconds(0.25f);
        }
        private float calcularDistancia(Vector3 originPosition, Vector3 destinyPosition)
        {
            return Vector3.Distance(originPosition, destinyPosition);
        }
    }
}