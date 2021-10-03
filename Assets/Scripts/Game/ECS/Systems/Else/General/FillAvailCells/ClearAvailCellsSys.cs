using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class ClearAvailCellsSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;

        private EcsFilter<CellsForAttackCom> _availCellsForAttackFilter = default;
        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        public void Run()
        {
            ref var availCellsForAttackComp = ref _availCellsForAttackFilter.Get1(0);

            foreach (byte curIdxCell in _xyCellFilter)
            {
                availCellsForAttackComp.Clear(PlayerTypes.First, AttackTypes.Simple, curIdxCell);
                availCellsForAttackComp.Clear(PlayerTypes.Second, AttackTypes.Simple, curIdxCell);
                availCellsForAttackComp.Clear(PlayerTypes.First, AttackTypes.Unique, curIdxCell);
                availCellsForAttackComp.Clear(PlayerTypes.Second, AttackTypes.Unique, curIdxCell);

                _cellsArsonFilter.Get1(0).Clear(PlayerTypes.First, curIdxCell);
                _cellsArsonFilter.Get1(0).Clear(PlayerTypes.Second, curIdxCell);
            }
        }
    }
}