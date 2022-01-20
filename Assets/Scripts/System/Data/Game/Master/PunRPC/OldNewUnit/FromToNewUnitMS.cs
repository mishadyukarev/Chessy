using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct FromToNewUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unit = EntityMPool.CreateHeroFromTo<UnitTC>().Unit;
            EntityMPool.CreateHeroFromTo<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            ref var unit_from = ref Unit(idx_from);
            ref var levUnit_from = ref CellUnitElseEs.Level(idx_from);
            ref var ownUnit_from = ref CellUnitElseEs.Owner(idx_from);

            ref var unit_to = ref Unit(idx_to);
            ref var levUnit_to = ref CellUnitElseEs.Level(idx_to);
            ref var ownUnit_to = ref CellUnitElseEs.Owner(idx_to);


            if (unit_from.Is(UnitTypes.Archer))
            {
                if (unit_to.Is(UnitTypes.Archer))
                {
                    if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                    {
                        var xy_from = Cell<XyC>(idx_from).Xy;

                        var list_around = CellSpaceSupport.GetXyAround(xy_from);


                        foreach (var xy_1 in list_around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.GetHero);

                                WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitElseEs.Level(idx_from).Level, CellUnitElseEs.Owner(idx_from).Player, idx_from).Have = false;
                                Unit(idx_from).Reset();

                                WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitElseEs.Level(idx_to).Level, CellUnitElseEs.Owner(idx_to).Player, idx_to).Have = false;
                                Unit(idx_to).Reset();


                                Unit(idx_to).Unit = unit;
                                CellUnitElseEs.Level(idx_to).Level = LevelTypes.First;

                                WhereUnitsE.HaveUnit(unit, LevelTypes.First, CellUnitElseEs.Owner(idx_to).Player, idx_to).Have = true;


                                InventorUnitsE.Units(unit, LevelTypes.First, CellUnitElseEs.Owner(idx_to).Player).Amount -= 1;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}