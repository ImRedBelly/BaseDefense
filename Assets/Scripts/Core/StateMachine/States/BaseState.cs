using Core.AI;
using Core.AI.Workers;
using StateMachine;

namespace Core.StateMachine.States
{
    public abstract class BaseState : IWorkerState
    {
        protected Worker Worker;
        

        public abstract void Update();

        public virtual void OnEnter(Worker worker)
        {
            Worker = worker;
        }

        public virtual void OnExit(Worker worker)
        {
        }


        protected void MoveToState(IWorkerState newState)
        {
            Worker.WorkerStateMachine.ChangeState(newState);
        }
    }
}