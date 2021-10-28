using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class LeftEnvEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            EnvirZoneViewUICom.AddListenerToEnvInfo(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            EnvirZoneDataUIC.IsActivatedInfo = !EnvirZoneDataUIC.IsActivatedInfo;
        }
    }
}