using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.GameSteps
{
        public class MoveCameraToCombatSight : IGameStep
        {
            public event Action<IGameStep> OnCompleted;
            public GameManager gameManager;
            public MoveCameraToCombatSight(GameManager manager)
            {
                this.gameManager = manager;
            }

            public void Start()
            {
                gameManager.CameraCombatMove();
                AutoComplete();
            }

            private async void AutoComplete()
            {
                await Task.Delay(2000);
                OnCompleted?.Invoke(this);
            }
    }
    }