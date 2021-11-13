using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ClearAvailCellsSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Simple, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Simple, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Unique, curIdxCell);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Unique, curIdxCell);

                CellsArsonArcherComp.Clear(PlayerTypes.First, curIdxCell);
                CellsArsonArcherComp.Clear(PlayerTypes.Second, curIdxCell);
            }
        }
    }
}