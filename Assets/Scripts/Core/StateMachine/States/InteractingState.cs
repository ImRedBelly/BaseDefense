using Core.AI;
using Core.AI.Workers;
using Core.Interaction;
using Library;
using UnityEngine;

namespace Core.StateMachine.States
{
    public class InteractingState : BaseState
    {
        private readonly BaseInteraction _interaction;
        private float _interactionTime;

        public InteractingState(BaseInteraction interaction)
        {
            _interaction = interaction;
        }

        private float _workerInteractionTime;

        public override void OnEnter(Worker worker)
        {
            base.OnEnter(worker);

            worker.Movable.Animator.SetFloat(AnimationsPrefsNames.Speed, 0);
            worker.Movable.Animator.SetBool(AnimationsPrefsNames.IsWorking, _interaction.GetBaseInteractionSetup().isAnimateWorker);
            worker.gameObject.transform.LookAt(_interaction.transform);

            _workerInteractionTime = _interaction.GetBaseInteractionSetup().interactionTime;
            worker.transform.localRotation = Quaternion.Euler(0, worker.transform.rotation.eulerAngles.y, 0);
        }

       

        public override void Update()
        {
            if (_workerInteractionTime == 0)
            {
                _interaction.CompleteInteraction(Worker);
                MoveToState(Worker.WorkerStateMachine.CreateState<IdleState>());
            }


            if (!Worker.Movable.IsStop)
            {
                _interaction.BreakUpInteraction(Worker);
                MoveToState(Worker.WorkerStateMachine.CreateState<MovingState>());
                return;
            }

            _interactionTime += Time.deltaTime;

            if (_interactionTime > _workerInteractionTime)
                _interactionTime = _workerInteractionTime;

            var progress = _interactionTime / _workerInteractionTime;
            _interaction.ProcessInteraction(progress);

            if (progress >= 1)
            {
                _interaction.CompleteInteraction(Worker);
                MoveToState(Worker.WorkerStateMachine.CreateState<IdleState>());
            }
        }

        public override void OnExit(Worker worker)
        {
            worker.Movable.Animator.SetBool(AnimationsPrefsNames.IsWorking, false);
            base.OnExit(worker);
        }
    }
}