using Core.AI.Workers;

namespace Core.AI.WorkersMovable
{
    public abstract class Movable
    {
        public abstract bool IsStop { get; }
        public abstract void Move(Character character);
    }
}