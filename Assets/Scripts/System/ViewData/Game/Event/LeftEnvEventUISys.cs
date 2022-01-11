using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    sealed class LeftEnvEventUISys
    {
        internal LeftEnvEventUISys()
        {
            Info<ButtonVC>().AddList(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            EnvInfoC.IsActivatedInfo = !EnvInfoC.IsActivatedInfo;
        }
    }
}