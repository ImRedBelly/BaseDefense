namespace Core.Interactions
{
    public class HomeInteraction : BaseInteraction
    {
        public override void OnEnter()
        {
            base.OnEnter();
            SessionManager.ChangeZonePlayer.Invoke(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            SessionManager.ChangeZonePlayer.Invoke(false);
        }
    }
}