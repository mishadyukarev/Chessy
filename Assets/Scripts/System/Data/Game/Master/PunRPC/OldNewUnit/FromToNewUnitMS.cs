namespace Game.Game
{
    struct FromToNewUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unit = EntitiesMaster.CreateHeroFromTo<UnitTC>().Unit;
            EntitiesMaster.CreateHeroFromTo<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            ref var unit_from = ref CellUnitEs.Else(idx_from).UnitC;
            ref var levUnit_from = ref CellUnitEs.Else(idx_from).LevelC;
            ref var ownUnit_from = ref CellUnitEs.Else(idx_from).OwnerC;

            ref var unit_to = ref CellUnitEs.Else(idx_to).UnitC;
            ref var levUnit_to = ref CellUnitEs.Else(idx_to).LevelC;
            ref var ownUnit_to = ref CellUnitEs.Else(idx_to).OwnerC;


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

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitEs.Else(idx_from).LevelC.Level, CellUnitEs.Else(idx_from).OwnerC.Player, idx_from).Have = false;
                                    CellUnitEs.Else(idx_from).UnitC.Reset();

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitEs.Else(idx_to).LevelC.Level, CellUnitEs.Else(idx_to).OwnerC.Player, idx_to).Have = false;
                                    CellUnitEs.Else(idx_to).UnitC.Reset();


                                    CellUnitEs.Else(idx_to).UnitC.Unit = unit;
                                    CellUnitEs.Else(idx_to).LevelC.Level = LevelTypes.First;

                                    WhereUnitsE.HaveUnit(unit, LevelTypes.First, CellUnitEs.Else(idx_to).OwnerC.Player, idx_to).Have = true;


                                    InventorUnitsE.Units(unit, LevelTypes.First, CellUnitEs.Else(idx_to).OwnerC.Player).Amount -= 1;

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