using Core.AI.Workers;

namespace Core.AI.WorkerAttachment
{
    public abstract class Attachment
    {
        public Character Target;
        public abstract void Attack();
    }
}