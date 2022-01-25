namespace Game.Game
{
    public struct FreezeDirectEnemyMS : IEcsRunSystem
    {
        public void Run()
        {
            FreezeDirectEnemyME.IdxFromToC.Get(out var idx_from, out var idx_to);

            ref var unitC_from = ref CellUnitEntities.Else(idx_from).UnitC;
            ref var unitC_to = ref CellUnitEntities.Else(idx_to).UnitC;


            var player = WhoseMoveE.WhoseMove.Player;

            if (CellUnitVisibleEs.Visible(player, idx_to).IsVisible && unitC_to.Have && CellRiverE.River(idx_to).HaveRiver)
            {
                ref var ownerUnitC_0 = ref CellUnitEntities.Else(idx_to).OwnerC;

                if (!CellUnitEntities.Else(idx_from).OwnerC.Is(ownerUnitC_0.Player))
                {
                    CellUnitEntities.Stun(idx_to).ForExitStun.Amount = 2;

                    CellUnitEntities.CooldownUnique(EntityMPool.UniqueAbilityC.Ability, idx_from).Cooldown += 5;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                    {
                        if (CellUnitEntities.Else(idx_1).UnitC.Have && CellUnitEntities.Else(idx_1).OwnerC.Is(ownerUnitC_0.Player))
                        {
                            CellUnitEntities.Stun(idx_1).ForExitStun.Amount = 2;
                        }
                    }
                }
            }
        }
    }
}