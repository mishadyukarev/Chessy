namespace Game.Game
{
    public struct FreezeDirectEnemyMS : IEcsRunSystem
    {
        public void Run()
        {
            Entities.MasterEs.FreezeDirectEnemy.IdxFromToC.Get(out var idx_from, out var idx_to);

            ref var unitC_from = ref Entities.CellEs.UnitEs.Else(idx_from).UnitC;
            ref var unitC_to = ref Entities.CellEs.UnitEs.Else(idx_to).UnitC;


            var player = Entities.WhoseMove.WhoseMove.Player;

            if (Entities.CellEs.UnitEs.VisibleE(player, idx_to).VisibleC.IsVisible && unitC_to.Have && Entities.CellEs.RiverEs.River(idx_to).RiverTC.HaveRiver)
            {
                ref var ownerUnitC_0 = ref Entities.CellEs.UnitEs.Else(idx_to).OwnerC;

                if (!Entities.CellEs.UnitEs.Else(idx_from).OwnerC.Is(ownerUnitC_0.Player))
                {
                    Entities.CellEs.UnitEs.Stun(idx_to).ForExitStun.Amount = 2;

                    Entities.CellEs.UnitEs.CooldownUnique(Entities.MasterEs.UniqueAbilityC.Ability, idx_from).Cooldown += 5;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_to))
                    {
                        if (Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have && Entities.CellEs.UnitEs.Else(idx_1).OwnerC.Is(ownerUnitC_0.Player))
                        {
                            Entities.CellEs.UnitEs.Stun(idx_1).ForExitStun.Amount = 2;
                        }
                    }
                }
            }
        }
    }
}