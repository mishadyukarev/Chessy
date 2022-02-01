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

            var unit_from = UnitEs.Main(idx_from).UnitTC;
            var ownUnit_from = UnitEs.Main(idx_from).OwnerC;

            var unit_to = UnitEs.Main(idx_to).UnitTC;
            var ownUnit_to = UnitEs.Main(idx_to).OwnerC;


            if (unit == UnitTypes.Elfemale || unit == UnitTypes.Snowy)
            {
                if (unit_from.Is(UnitTypes.Archer))
                {
                    if (unit_to.Is(UnitTypes.Archer))
                    {
                        if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                        {
                            foreach (var idx_1 in CellEs.GetIdxsAround(idx_from))
                            {
                                if (idx_1 == idx_to)
                                {
                                    Es.Rpc.SoundToGeneral(sender, ClipTypes.GetHero);

                                    UnitEs.Main(idx_from).Clear(Es.WhereUnitsEs);
                                    UnitEs.Main(idx_to).Clear(Es.WhereUnitsEs);

                                    UnitEs.Main(idx_to).SetNew((unit, LevelTypes.First, whoseMove, ConditionUnitTypes.None, false), Es);
                                    Es.InventorUnitsEs.Units(unit, LevelTypes.First, UnitEs.Main(idx_to).OwnerC.Player).TakeUnit();

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