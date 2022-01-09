namespace Game.Game
{
    sealed class ClearAvailCellsS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityCellPool.Idxs)
            {
                AttackCellsC.Clear(AttackTypes.Simple, PlayerTypes.First, idx_0);
                AttackCellsC.Clear(AttackTypes.Simple, PlayerTypes.Second, idx_0);
                AttackCellsC.Clear(AttackTypes.Unique, PlayerTypes.First, idx_0);
                AttackCellsC.Clear(AttackTypes.Unique, PlayerTypes.Second, idx_0);
            }
        }
    }
}