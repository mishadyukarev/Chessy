using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class LeftEnvEventUISys : IEcsInitSystem
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