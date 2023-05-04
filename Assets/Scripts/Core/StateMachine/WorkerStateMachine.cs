using Core.AI.Workers;
using Core.StateMachine.States;
using JetBrains.Annotations;

namespace Core.StateMachine
{
    public interface IWorkerState
    {
        void OnEnter(Character character);

        void Update();

        void OnExit(Character character);
    }

    [UsedImplicitly]
    public class WorkerStateMachine
    {
        private Character character;
        private IWorkerState _currentState;

        public void Start(Character character)
        {
            this.character = character;

            _currentState = CreateState<PeaceState>();
            _currentState.OnEnter(character);
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(IWorkerState newState)
        {
            _currentState.OnExit(character);
            _currentState = newState;
            _currentState.OnEnter(character);
        }

      

        public TState CreateState<TState>() where TState : new() => new TState();
    }
}