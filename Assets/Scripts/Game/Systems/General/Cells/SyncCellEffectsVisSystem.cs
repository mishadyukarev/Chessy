using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncCellEffectsVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellFireDataComponent, CellFireViewComponent> _cellFireFilter;

        public void Run()
        {
            foreach (var idx in _cellFireFilter)
            {
                if (_cellFireFilter.Get1(idx).HaveFire)
                {
                    _cellFireFilter.Get2(idx).EnableSR();
                }

                else
                {
                    _cellFireFilter.Get2(idx).DisableSR();
                }
            }
        }
    }
}