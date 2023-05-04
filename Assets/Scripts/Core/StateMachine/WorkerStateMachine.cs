using Core.AI;
using Core.AI.Workers;
using Core.Interaction;
using Core.StateMachine.States;
using Zenject;
using Interaction;
using JetBrains.Annotations;

namespace StateMachine
{
    public interface IWorkerState
    {
        void OnEnter(Worker worker);

        void Update();

        void OnExit(Worker worker);
    }

    [UsedImplicitly]
    public class WorkerStateMachine
    {
        private Worker _worker;
        private IWorkerState _currentState;

        public void Start(Worker worker)
        {
            _worker = worker;

            _currentState = CreateState<IdleState>();
            _currentState.OnEnter(worker);
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(IWorkerState newState)
        {
            _currentState.OnExit(_worker);
            _currentState = newState;
            _currentState.OnEnter(_worker);
        }

      

        public TState CreateState<TState>() where TState : new() => new TState();

        public InteractingState CreateInteractingState(BaseInteraction interaction) =>
            new InteractingState(interaction);
    }
}