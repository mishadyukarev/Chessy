using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class WindUISys : IEcsRunSystem
    {
        private EcsFilter<WindZoneUICom> _windUIFilt = default;
        private EcsFilter<WindCom> _windFilt = default;

        public void Run()
        {
            _windUIFilt.Get1(0).SetEulerRot(_windFilt.Get1(0).DirectWind);
        }
    }
}