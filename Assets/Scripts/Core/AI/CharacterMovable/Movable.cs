using Core.AI.Workers;

namespace Core.AI.CharacterMovable
{
    public abstract class Movable
    {
        public abstract bool IsStop { get; }
        public abstract void Move(Character character);
    }
}