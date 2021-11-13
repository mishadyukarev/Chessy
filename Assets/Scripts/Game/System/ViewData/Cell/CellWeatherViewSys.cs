using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class CellWeatherViewSys : IEcsRunSystem
    {
        private EcsFilter<CloudC> _cloudF = default;
        private EcsFilter<CloudVC> _cloudVF = default;

        public void Run()
        {
            foreach (var idxCell in _cloudVF)
            {
                _cloudVF.Get1(idxCell).EnableCloud(_cloudF.Get1(idxCell).Have);
            }
        }
    }
}