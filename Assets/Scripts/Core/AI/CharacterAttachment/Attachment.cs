using Core.AI.Characters;
using UnityEngine;

namespace Core.AI.CharacterAttachment
{
    public abstract class Attachment
    {
        public Transform DefaultTarget;
        public Character Target;
        public abstract void Attack();
    }
}