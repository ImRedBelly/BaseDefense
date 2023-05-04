using Core.AI;
using Core.AI.Workers;
using Library;

namespace Core.StateMachine.States
{
    public class IdleState : BaseState
    {
        public override void OnEnter(Worker worker)
        {
            base.OnEnter(worker);
            worker.Movable.Animator.SetFloat(AnimationsPrefsNames.Speed, 0);
        }

        public override void Update()
        {
            if (!Worker.Movable.IsStop)
            {
                Worker.GetFirstBaseInteraction()?.ResetInteraction();
                MoveToState(Worker.WorkerStateMachine.CreateState<MovingState>());
            }
            else
            {
                var toInteract = Worker.GetFirstAllowedBaseInteraction();

                if (toInteract != null)
                {
                    if (toInteract.CanInteract(Worker))
                    {
                        toInteract.Interact(Worker);
                        MoveToState(Worker.WorkerStateMachine.CreateInteractingState(toInteract));
                    }
                }
            }
        }
    }
}