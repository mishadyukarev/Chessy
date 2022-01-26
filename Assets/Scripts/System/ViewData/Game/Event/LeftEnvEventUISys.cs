using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    sealed class LeftEnvEventUISys
    {
        internal LeftEnvEventUISys()
        {
            Info<ButtonUIC>().AddListener(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            Entities.InfoEnvironment.IsActiveC.Toggle();
        }
    }
}