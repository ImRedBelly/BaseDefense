using Setups;
using UnityEngine;

namespace Zenject
{
    public class SetupInstaller : MonoInstaller
    {
        [SerializeField] private GameBalance gameBalance;
        [SerializeField] private PrefabContainer prefabContainer;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<GameBalance>().FromScriptableObject(gameBalance).AsSingle();
            Container.Bind<PrefabContainer>().FromScriptableObject(prefabContainer).AsSingle();
        }
    }
}