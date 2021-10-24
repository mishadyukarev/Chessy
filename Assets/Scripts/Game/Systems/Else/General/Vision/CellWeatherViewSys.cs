using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class CellWeatherViewSys : IEcsRunSystem
    {
        private EcsFilter<CellWeatherDataCom, CellWeatherViewCom> _cellWeathFilt = default;

        public void Run()
        {
            foreach (var idxCell in _cellWeathFilt)
            {
                _cellWeathFilt.Get2(idxCell).EnableCloud(_cellWeathFilt.Get1(idxCell).EnabledCloud);
            }
        }
    }
}