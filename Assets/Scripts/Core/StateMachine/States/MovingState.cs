namespace Core.StateMachine.States
{
    public class MovingState : BaseState
    {
        public override void Update()
        {
            if (Worker.Movable.IsStop)
            {
                MoveToState(Worker.WorkerStateMachine.CreateState<IdleState>());
            }
        }
    }
}