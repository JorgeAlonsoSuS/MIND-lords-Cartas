using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Deck.GameSteps
{
    public class StartPlayerPhase : IGameStep
    {
        private readonly Player player;

        public event Action<IGameStep> OnCompleted;

        public StartPlayerPhase(Player player)
        {
            this.player = player;
        }

        public void Start()
        {
            Debug.Log("Start player phase : " + player.name);

            GameManager.Instance.SetCurrentPlayer(player);

            AutoComplete();
        }

        private async void AutoComplete()
        {
            await Task.Delay(1000);
            OnCompleted?.Invoke(this);
        }
    }
}
