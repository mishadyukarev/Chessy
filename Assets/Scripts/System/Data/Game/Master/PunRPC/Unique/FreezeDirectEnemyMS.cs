namespace Game.Game
{
    public struct FreezeDirectEnemyMS : IEcsRunSystem
    {
        public void Run()
        {
            FreezeDirectEnemyME.IdxFromToC.Get(out var idx_from, out var idx_to);

            ref var unitC_from = ref CellUnitEs.Unit(idx_from);
            ref var unitC_to = ref CellUnitEs.Unit(idx_to);


            var player = WhoseMoveE.WhoseMove.Player;

            if (CellUnitVisibleEs.Visible(player, idx_to).IsVisible && unitC_to.Have && CellRiverE.River(idx_to).HaveRiver)
            {
                ref var ownerUnitC_0 = ref CellUnitElseEs.Owner(idx_to);

                if (!CellUnitElseEs.Owner(idx_from).Is(ownerUnitC_0.Player))
                {
                    CellUnitStunEs.ForExitStun(idx_to).Amount = 2;

                    CellUnitAbilityUniqueEs.Cooldown(EntityMPool.UniqueAbilityC.Ability, idx_from).Add(5);

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                    {
                        if (CellUnitEs.Unit(idx_1).Have && CellUnitElseEs.Owner(idx_1).Is(ownerUnitC_0.Player))
                        {
                            CellUnitStunEs.ForExitStun(idx_1).Amount = 2;
                        }
                    }
                }
            }
        }
    }
}