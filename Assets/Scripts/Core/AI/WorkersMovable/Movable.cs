using Core.AI.Workers;
using UnityEngine;

namespace Core.AI.WorkersMovable
{
    public abstract class Movable
    {
        public Animator Animator { get; private set; }
        public abstract float Speed { get; }
        public abstract bool IsStop { get; }

        public abstract void Move(Worker worker);
        protected Movable(Animator animator) =>  Animator = animator;
    }
}