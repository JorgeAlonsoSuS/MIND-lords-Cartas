using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.GameSteps
{
    public class PlayCardsPhase : IGameStep
    {
        private Player player;

        public event Action<IGameStep> OnCompleted;

        public PlayCardsPhase(Player player)
        {
            this.player = player;
        }

        public void Start()
        {
            player.ToggleCardsInteractive(true);
        }

    }
}
