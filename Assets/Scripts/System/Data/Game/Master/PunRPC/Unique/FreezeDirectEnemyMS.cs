//namespace Game.Game
//{
//    public struct FreezeDirectEnemyMS : IEcsRunSystem
//    {
//        public void Run()
//        {
//            Ents.MasterEs.FreezeDirectEnemy.IdxFromToC.Get(out var idx_from, out var idx_to);

//            ref var unitC_from = ref Ents.CellEs.UnitEs.Else(idx_from).UnitC;
//            ref var unitC_to = ref Ents.CellEs.UnitEs.Else(idx_to).UnitC;


//            var player = Ents.WhoseMove.WhoseMove.Player;

//            if (Ents.CellEs.UnitEs.VisibleE(player, idx_to).VisibleC.IsVisible && unitC_to.Have && Ents.CellEs.RiverEs.River(idx_to).RiverTC.HaveRiver)
//            {
//                ref var ownerUnitC_0 = ref Ents.CellEs.UnitEs.Else(idx_to).OwnerC;

//                if (!Ents.CellEs.UnitEs.Else(idx_from).OwnerC.Is(ownerUnitC_0.Player))
//                {
//                    Ents.CellEs.UnitEs(idx_to).Stun.ForExitStun.Amount = 2;

//                    Ents.CellEs.UnitEs.CooldownUnique(Ents.MasterEs.UniqueAbilityC.Ability, idx_from).Cooldown += 5;

//                    foreach (var idx_1 in Ents.CellEsWorker.GetIdxsAround(idx_to))
//                    {
//                        if (Ents.CellEs.UnitEs.Else(idx_1).UnitC.Have && Ents.CellEs.UnitEs.Else(idx_1).OwnerC.Is(ownerUnitC_0.Player))
//                        {
//                            Ents.CellEs.UnitEs(idx_1).Stun.ForExitStun.Amount = 2;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}