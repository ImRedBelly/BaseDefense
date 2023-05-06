using Core.ResourceEntity;
using Core.UI.Dialogs;
using Core.Weapons;
using UI;
using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "PrefabContainer")]
    public class PrefabContainer : ScriptableObject
    {
        public BulletController bulletController;
        public ResourceCreatorController resourceCreatorController;
        public HpViewController hpViewController;
        public RestartDialog restartDialog;
    }
}