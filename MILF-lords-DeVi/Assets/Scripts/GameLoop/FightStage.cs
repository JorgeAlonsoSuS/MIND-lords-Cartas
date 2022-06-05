using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Deck.GameSteps
{
    public class FightStage : IGameStep
    {   
        private readonly Player player1;

        private readonly Player player2;

        private readonly GameManager gameManager;

        public event Action<IGameStep> OnCompleted;

        public FightStage(Player player1, Player player2, GameManager manager)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.gameManager = manager;
        }

        public void Start()
        {
            this.gameManager.startFight = true;
        }
    }
}

