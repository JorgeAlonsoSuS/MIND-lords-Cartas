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

        [SerializeField]
        public  Camera mainCamera;



        [SerializeField]
        private Transform[] playerPositions;
        
        private GameLoop gameLoop;
        private Boolean combatCam = false;
        public Boolean startFight = false;


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

                gameLoop = new GameLoop(
                new List<IGameStep>()
                {
                     new CleaningPhase(players[0], players[1]),
                    new StartPlayerPhase(players[0]),
                    new PlayCardsPhase(players[0]),
                    new StartPlayerPhase(players[1]),
                    new PlayCardsPhase(players[1]),
                    new MoveCameraToCombatSight(this),
                    new FightStage(players[0], players[1], this)
                }
            );

            gameLoop.RunGame();
        }
        private void cameraMove()
        {
            if (!combatCam)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (CurrentPlayer == players[i])
                    {
                        if (mainCamera.transform.position != playerPositions[i].position || mainCamera.transform.rotation != playerPositions[i].rotation)
                        {
                            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, playerPositions[i].position, 30f * Time.deltaTime);
                            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, playerPositions[i].rotation, 60f * Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                if (mainCamera.transform.position != playerPositions[2].position || mainCamera.transform.rotation != playerPositions[2].rotation)
                {
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, playerPositions[2].position, 30f * Time.deltaTime);
                    mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, playerPositions[2].rotation, 60f * Time.deltaTime);
                }
            }
        }
        public void CameraCombatMove()
        {
            combatCam = true;
        }
        internal void SetCurrentPlayer(Player player)
        {
            CurrentPlayer = player;
        }

        private void Update()
        {
            cameraMove();
            if (startFight)
            {
                if (players[0].MonstersInGame.Count == 0 || players[1].MonstersInGame.Count == 0)
                {
                    if (players[0].MonstersInGame.Count == 0)
                    {
                        FinishGame(players[1]);
                    }
                    else
                    {
                        FinishGame(players[0]);
                    }
                }
            }
        }

        private void FinishGame(Player player)
        {
            startFight = false;
            combatCam = false;
            Debug.Log("Ha ganado: " + player.SelectedPlayerType);
            player.victorias++;
            if (player.victorias != 2)
            {
                gameLoop.RunGame();
            }
        }

        public MonsterBehaviour Punch(Player player, MonsterBehaviour monsterB) 
        {
            if (startFight)
            {
                if (players[0] == player)
                {
                    if (players[1].MonstersInGame.Count > 0)
                    {
                        int pos = -1;
                        float distance = 0f;
                        for (int i = 0; i < players[1].MonstersInGame.Count; i++)
                        {
                            if (pos == -1)
                            {
                                pos = 0;
                                distance = calcularDistancia(monsterB.transform.position, players[1].MonstersInGame[i].transform.position);
                            }
                            if (distance > calcularDistancia(monsterB.transform.position, players[1].MonstersInGame[i].transform.position))
                            {
                                pos = i;
                                distance = calcularDistancia(monsterB.transform.position, players[1].MonstersInGame[i].transform.position);
                            }
                        }
                        return players[1].MonstersInGame[pos];
                    }
                }

                else
                {
                    if (players[0].MonstersInGame.Count > 0)
                    {
                        int pos = -1;
                        float distance = 0f;
                        for (int i = 0; i < players[0].MonstersInGame.Count; i++)
                        {
                            if (pos == -1)
                            {
                                pos = 0;
                                distance = calcularDistancia(monsterB.transform.position, players[0].MonstersInGame[i].transform.position);
                            }
                            if (distance > calcularDistancia(monsterB.transform.position, players[0].MonstersInGame[i].transform.position))
                            {
                                pos = i;
                                distance = calcularDistancia(monsterB.transform.position, players[0].MonstersInGame[i].transform.position);
                            }
                        }
                        return players[0].MonstersInGame[pos];
                    }
                }
            }
            

            return null;
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
        private float calcularDistancia(Vector3 originPosition, Vector3 destinyPosition)
        {
            return Vector3.Distance(originPosition, destinyPosition);
        }

    }
}