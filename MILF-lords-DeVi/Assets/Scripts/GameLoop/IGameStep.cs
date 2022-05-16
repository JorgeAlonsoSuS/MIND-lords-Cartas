using System;

namespace Deck.GameSteps
{

    public interface IGameStep
    {

        event Action<IGameStep> OnCompleted;

        void Start();

    }
}
