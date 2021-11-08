using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ClearAvailCellsSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Simple, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Simple, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Unique, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Unique, curIdxCell);

                _cellsArsonFilter.Get1(0).Clear(PlayerTypes.First, curIdxCell);
                _cellsArsonFilter.Get1(0).Clear(PlayerTypes.Second, curIdxCell);
            }
        }
    }
}