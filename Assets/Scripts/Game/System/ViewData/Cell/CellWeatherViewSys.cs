using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class CellWeatherViewSys : IEcsRunSystem
    {
        private EcsFilter<CellCloudDataC, CellCloudViewC> _cellWeathFilt = default;

        public void Run()
        {
            foreach (var idxCell in _cellWeathFilt)
            {
                _cellWeathFilt.Get2(idxCell).EnableCloud(_cellWeathFilt.Get1(idxCell).HaveCloud);
            }
        }
    }
}