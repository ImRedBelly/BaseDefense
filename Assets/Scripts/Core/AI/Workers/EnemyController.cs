using System;
using Core.AI.WorkerAttachment;
using Core.AI.WorkersMovable;
using Setups;
using Setups.CharacterSetups;
using UnityEngine;

namespace Core.AI.Workers
{
    public class EnemyController : Character<BaseEnemySetup>
    {
        private void Awake()
        {
            WorkerStateMachine.Start(this);
            Attachment = new PlayerAttacment(this);
        }

        protected override void Start()
        {
            base.Start();
            SessionManager.AddEnemy(this);
        }


        public override Vector3 Direction { get; }
        public override CharacterSetup GetCharacterSetup() => setup;


        public override void FindTargetToAttack()
        {
            
        }
    }
}