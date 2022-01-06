using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class LeftEnvEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            EnvirUIC.AddListenerToEnvInfo(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            EnvInfoC.IsActivatedInfo = !EnvInfoC.IsActivatedInfo;
        }
    }
}