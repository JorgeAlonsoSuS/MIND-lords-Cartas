using Deck;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Deck.GameSteps;
using System;

namespace Deck
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }
        public Player CurrentPlayer { get; private set; }

        [SerializeField]
        private MonsterDC[] monters;

        [SerializeField]
        private Player[] players;

        [SerializeField]
        private CardFactory cardFactory;
        private bool check = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DrawCards(players[0]);
            DrawCards(players[1]);

            players[0].ToggleCardsInteractive(false);
            players[1].ToggleCardsInteractive(false);

            var gameLoop = new GameLoop(
                new List<IGameStep>()
                {
                    new StartPlayerPhase(players[0]),
                    new PlayCardsPhase(players[0]),
                    new StartPlayerPhase(players[1]),
                    new PlayCardsPhase(players[1])
                }
            );

            gameLoop.RunGame();
        }

        internal void SetCurrentPlayer(Player player)
        {
            CurrentPlayer = player;
        }

        private void Update()
        {
            if (!check)
            {
                if(players[0].MonstersInGame.Count>0) LockPlayer1();
                if (players[1].MonstersInGame.Count > 0) LockPlayer2();
            }
            else
            {
                StartCoroutine(CheckAgain());
            }
        }
        private void LockPlayer1()
        {
            if (players[1].MonstersInGame.Count>0){
                for (int j = 0; j < players[0].MonstersInGame.Count; j++) {
                    if (players[0].MonstersInGame[j].LockedMonster == null)
                    {
                        int pos = -1;
                        float distance = 0f;
                        for (int i = 0; i < players[1].MonstersInGame.Count; i++)
                        {
                            if (pos == -1)
                            {
                                pos = 0;
                                distance = calcularDistancia(players[0].MonstersInGame[j].transform.position, players[1].MonstersInGame[i].transform.position);
                            }
                            if(distance> calcularDistancia(players[0].MonstersInGame[j].transform.position, players[1].MonstersInGame[i].transform.position))
                            {
                                pos = i;
                                distance= calcularDistancia(players[0].MonstersInGame[j].transform.position, players[1].MonstersInGame[i].transform.position);
                            }
                        }
                        players[0].MonstersInGame[j].LockEnemy(players[1].MonstersInGame[pos]);
                    }
                }
            }
        }
        private void LockPlayer2()
        {
            if (players[0].MonstersInGame.Count > 0)
            {
                for (int j = 0; j < players[1].MonstersInGame.Count; j++)
                {
                    if (players[1].MonstersInGame[j].LockedMonster == null)
                    {
                        int pos = -1;
                        float distance = 0f;
                        for (int i = 0; i < players[0].MonstersInGame.Count; i++)
                        {
                            if (pos == -1)
                            {
                                pos = 0;
                                distance = calcularDistancia(players[1].MonstersInGame[j].transform.position, players[0].MonstersInGame[i].transform.position);
                            }
                            if (distance > calcularDistancia(players[1].MonstersInGame[j].transform.position, players[0].MonstersInGame[i].transform.position))
                            {
                                pos = i;
                                distance = calcularDistancia(players[1].MonstersInGame[j].transform.position, players[0].MonstersInGame[i].transform.position);
                            }
                        }
                        players[1].MonstersInGame[j].LockEnemy(players[0].MonstersInGame[pos]);
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