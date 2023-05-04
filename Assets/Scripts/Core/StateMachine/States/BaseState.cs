using Core.AI;
using Core.AI.Workers;

namespace Core.StateMachine.States
{
    public abstract class BaseState : IWorkerState
    {
        protected Character Character;

        public abstract void Update();

        public virtual void OnEnter(Character character)
        {
            Character = character;
        }

        public virtual void OnExit(Character character)
        {
        }


        protected void MoveToState(IWorkerState newState)
        {
            Character.WorkerStateMachine.ChangeState(newState);
        }
    }
}