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
            if (InterfaceManager.Instance.MainCamera != null)
            {
                InterfaceManager.Instance.MainCamera.transform.rotation *= Quaternion.Euler(0, player.transform.rotation.y, 0);
                InterfaceManager.Instance.MainCamera.transform.position = new Vector3(0, 0, (player.transform.position.z) * 1.5f);
            }
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
