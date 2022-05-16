using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deck.GameSteps
{

    public class GameLoop
    {
        private readonly IList<IGameStep> gameSteps;

        public GameLoop(IList<IGameStep> gameSteps)
        {
            this.gameSteps = gameSteps;
        }

        public void RunGame()
        {
            var firstStep = gameSteps.First();
            RunStep(firstStep);
        }

        private void RunStep(IGameStep gameStep)
        {
            gameStep.OnCompleted += GameStep_OnCompleted;
            gameStep.Start();
        }

        private void GameStep_OnCompleted(IGameStep gameStep)
        {
            gameStep.OnCompleted -= GameStep_OnCompleted;

            if (!IsGameCompleted())
            {
                RunStep(GetNextStep(gameStep));
            }
        }

        private IGameStep GetNextStep(IGameStep gameStep)
        {
            int gameStepIndex = gameSteps.IndexOf(gameStep);
            int nextStepIndex = (gameStepIndex + 1) % gameSteps.Count;

            return gameSteps[nextStepIndex];
        }

        private bool IsGameCompleted()
        {
            return false;
        }
    }

}