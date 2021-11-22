using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ClearAvailCellsS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                AttackCellsC.Clear(AttackTypes.Simple, PlayerTypes.First,  idx_0);
                AttackCellsC.Clear(AttackTypes.Simple, PlayerTypes.Second, idx_0);
                AttackCellsC.Clear(AttackTypes.Unique, PlayerTypes.First,  idx_0);
                AttackCellsC.Clear(AttackTypes.Unique, PlayerTypes.Second,  idx_0);

                ArsonCellsC.Clear(PlayerTypes.First, idx_0);
                ArsonCellsC.Clear(PlayerTypes.Second, idx_0);
            }
        }
    }
}