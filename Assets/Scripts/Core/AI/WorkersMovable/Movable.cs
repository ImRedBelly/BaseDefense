using Core.AI.Workers;
using UnityEngine;

namespace Core.AI.WorkersMovable
{
    public abstract class Movable
    {
        public abstract float Speed { get; }
        public abstract bool IsStop { get; }

        public abstract void Move(Worker worker);
    }
}