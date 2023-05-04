using Sirenix.OdinInspector;

namespace Setups.Interactions
{
    public abstract class BaseInteractionSetup : SerializedScriptableObject
    {
        public float interactionTime = 2;
        public float interactionAngle = 60;

        public bool isAnimateWorker;
        public string nameAnimateWork ="IsWorking";
 
    }
}