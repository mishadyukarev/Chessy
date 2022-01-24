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
                ref var ownerUnitC_0 = ref EntitiesPool.UnitElse.Owner(idx_to);

                if (!EntitiesPool.UnitElse.Owner(idx_from).Is(ownerUnitC_0.Player))
                {
                    EntitiesPool.UnitStuns[idx_to].ForExitStun.Amount = 2;

                    CellUnitAbilityUniqueEs.Cooldown(EntityMPool.UniqueAbilityC.Ability, idx_from) += 5;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                    {
                        if (CellUnitEs.Unit(idx_1).Have && EntitiesPool.UnitElse.Owner(idx_1).Is(ownerUnitC_0.Player))
                        {
                            EntitiesPool.UnitStuns[idx_1].ForExitStun.Amount = 2;
                        }
                    }
                }
            }
        }
    }
}