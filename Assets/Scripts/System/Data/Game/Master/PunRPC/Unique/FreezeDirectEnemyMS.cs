namespace Game.Game
{
    public struct FreezeDirectEnemyMS : IEcsRunSystem
    {
        public void Run()
        {
            FreezeDirectEnemyME.IdxFromToC.Get(out var idx_from, out var idx_to);

            ref var unitC_from = ref CellUnitEs.Else(idx_from).UnitC;
            ref var unitC_to = ref CellUnitEs.Else(idx_to).UnitC;


            var player = Entities.WhoseMoveE.WhoseMove.Player;

            if (CellUnitEs.VisibleE(player, idx_to).VisibleC.IsVisible && unitC_to.Have && CellRiverEs.River(idx_to).RiverTC.HaveRiver)
            {
                ref var ownerUnitC_0 = ref CellUnitEs.Else(idx_to).OwnerC;

                if (!CellUnitEs.Else(idx_from).OwnerC.Is(ownerUnitC_0.Player))
                {
                    CellUnitEs.Stun(idx_to).ForExitStun.Amount = 2;

                    CellUnitEs.CooldownUnique(EntityMPool.UniqueAbilityC.Ability, idx_from).Cooldown += 5;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                    {
                        if (CellUnitEs.Else(idx_1).UnitC.Have && CellUnitEs.Else(idx_1).OwnerC.Is(ownerUnitC_0.Player))
                        {
                            CellUnitEs.Stun(idx_1).ForExitStun.Amount = 2;
                        }
                    }
                }
            }
        }
    }
}