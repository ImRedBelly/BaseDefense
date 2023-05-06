using Setups.Recipes;
using Sirenix.Utilities;
using UnityEngine;

namespace Core.ResourceEntity
{
    public class PointResource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private GameObject[] resourceView;
        public ResourceType ResourceType => resourceType;

        public void ActivateResourceView(int count, bool state)
        {
            resourceView.ForEach(x => x.SetActive(false));
            for (int i = 0; i < count; i++)
            {
                if (i < resourceView.Length)
                    resourceView[i].SetActive(state);
            }
        }
    }
}