namespace Game.Game
{
    sealed class FromToNewUnitMS : SystemAbstract, IEcsRunSystem
    {
        public FromToNewUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unit = Es.MasterEs.CreateHeroFromTo<UnitTC>().Unit;
            Es.MasterEs.CreateHeroFromTo<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            ref var unit_from = ref Es.CellEs.UnitEs.Main(idx_from).UnitC;
            ref var levUnit_from = ref Es.CellEs.UnitEs.Main(idx_from).LevelC;
            ref var ownUnit_from = ref Es.CellEs.UnitEs.Main(idx_from).OwnerC;

            ref var unit_to = ref Es.CellEs.UnitEs.Main(idx_to).UnitC;
            ref var levUnit_to = ref Es.CellEs.UnitEs.Main(idx_to).LevelC;
            ref var ownUnit_to = ref Es.CellEs.UnitEs.Main(idx_to).OwnerC;


            if (unit == UnitTypes.Elfemale || unit == UnitTypes.Snowy)
            {
                if (unit_from.Is(UnitTypes.Archer))
                {
                    if (unit_to.Is(UnitTypes.Archer))
                    {
                        if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                        {
                            foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_from))
                            {
                                if (idx_1 == idx_to)
                                {
                                    Es.Rpc.SoundToGeneral(sender, ClipTypes.GetHero);

                                    Es.WhereUnitsEs.WhereUnit(UnitTypes.Archer, Es.CellEs.UnitEs.Main(idx_from).LevelC.Level, Es.CellEs.UnitEs.Main(idx_from).OwnerC.Player, idx_from).HaveUnit.Have = false;
                                    Es.CellEs.UnitEs.Main(idx_from).UnitC.Reset();

                                    Es.WhereUnitsEs.WhereUnit(UnitTypes.Archer, Es.CellEs.UnitEs.Main(idx_to).LevelC.Level, Es.CellEs.UnitEs.Main(idx_to).OwnerC.Player, idx_to).HaveUnit.Have = false;
                                    Es.CellEs.UnitEs.Main(idx_to).UnitC.Reset();


                                    Es.CellEs.UnitEs.Main(idx_to).UnitC.Unit = unit;
                                    Es.CellEs.UnitEs.Main(idx_to).LevelC.Level = LevelTypes.First;

                                    Es.WhereUnitsEs.WhereUnit(unit, LevelTypes.First, Es.CellEs.UnitEs.Main(idx_to).OwnerC.Player, idx_to).HaveUnit.Have = true;


                                    Es.InventorUnitsEs.Units(unit, LevelTypes.First, Es.CellEs.UnitEs.Main(idx_to).OwnerC.Player).Units.Amount -= 1;

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