using Deck.GameSteps;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Deck.GameSteps
{
    internal class CleaningPhase : IGameStep
    {
        public Player player1;
        public Player player2;
        public event Action<IGameStep> OnCompleted;
        public CleaningPhase(Player pl1, Player pl2)
        {
            this.player1 = pl1;
            this.player2 = pl2;
        }
        public void Start()
        {
            for(int i = 0; i<player1.MonstersInGame.Count;i++)
            {
                player1.MonstersInGame[i].changeHealt(0);
            }
            for (int i = 0; i < player2.MonstersInGame.Count; i++)
            {
                player2.MonstersInGame[i].changeHealt(0);
            }
            OnCompleted?.Invoke(this);
        }
    }
}