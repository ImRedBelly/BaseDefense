using Core;

namespace Zenject
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SessionManager>().AsSingle();
        }
    }
}