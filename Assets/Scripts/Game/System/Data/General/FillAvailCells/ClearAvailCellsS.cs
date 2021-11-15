using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ClearAvailCellsS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Simple, idx_0);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Simple, idx_0);
                CellsAttackC.Clear(PlayerTypes.First, AttackTypes.Unique, idx_0);
                CellsAttackC.Clear(PlayerTypes.Second, AttackTypes.Unique, idx_0);

                CellsArsonArcherComp.Clear(PlayerTypes.First, idx_0);
                CellsArsonArcherComp.Clear(PlayerTypes.Second, idx_0);
            }
        }
    }
}