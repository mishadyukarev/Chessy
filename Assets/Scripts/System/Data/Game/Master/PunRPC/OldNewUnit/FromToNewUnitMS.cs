namespace Game.Game
{
    struct FromToNewUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unit = Entities.MasterEs.CreateHeroFromTo<UnitTC>().Unit;
            Entities.MasterEs.CreateHeroFromTo<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            ref var unit_from = ref Entities.CellEs.UnitEs.Else(idx_from).UnitC;
            ref var levUnit_from = ref Entities.CellEs.UnitEs.Else(idx_from).LevelC;
            ref var ownUnit_from = ref Entities.CellEs.UnitEs.Else(idx_from).OwnerC;

            ref var unit_to = ref Entities.CellEs.UnitEs.Else(idx_to).UnitC;
            ref var levUnit_to = ref Entities.CellEs.UnitEs.Else(idx_to).LevelC;
            ref var ownUnit_to = ref Entities.CellEs.UnitEs.Else(idx_to).OwnerC;


            if (unit == UnitTypes.Elfemale || unit == UnitTypes.Snowy)
            {
                if (unit_from.Is(UnitTypes.Archer))
                {
                    if (unit_to.Is(UnitTypes.Archer))
                    {
                        if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                        {
                            foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_from))
                            {
                                if (idx_1 == idx_to)
                                {
                                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.GetHero);

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, Entities.CellEs.UnitEs.Else(idx_from).LevelC.Level, Entities.CellEs.UnitEs.Else(idx_from).OwnerC.Player, idx_from).Have = false;
                                    Entities.CellEs.UnitEs.Else(idx_from).UnitC.Reset();

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, Entities.CellEs.UnitEs.Else(idx_to).LevelC.Level, Entities.CellEs.UnitEs.Else(idx_to).OwnerC.Player, idx_to).Have = false;
                                    Entities.CellEs.UnitEs.Else(idx_to).UnitC.Reset();


                                    Entities.CellEs.UnitEs.Else(idx_to).UnitC.Unit = unit;
                                    Entities.CellEs.UnitEs.Else(idx_to).LevelC.Level = LevelTypes.First;

                                    WhereUnitsE.HaveUnit(unit, LevelTypes.First, Entities.CellEs.UnitEs.Else(idx_to).OwnerC.Player, idx_to).Have = true;


                                    InventorUnitsE.Units(unit, LevelTypes.First, Entities.CellEs.UnitEs.Else(idx_to).OwnerC.Player).Amount -= 1;

                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}