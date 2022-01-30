using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    sealed class LeftEnvEventUISys : SystemViewAbstract
    {
        public LeftEnvEventUISys(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            Info<ButtonUIC>().AddListener(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            Es.InfoEnvironment.IsActiveC.Toggle();
        }
    }
}