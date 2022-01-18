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

            ref var unit_from = ref Unit<UnitTC>(idx_from);
            ref var levUnit_from = ref Unit<LevelTC>(idx_from);
            ref var ownUnit_from = ref Unit<PlayerTC>(idx_from);

            ref var unit_to = ref Unit<UnitTC>(idx_to);
            ref var levUnit_to = ref Unit<LevelTC>(idx_to);
            ref var ownUnit_to = ref Unit<PlayerTC>(idx_to);


            if (unit_from.Is(UnitTypes.Archer))
            {
                if (unit_to.Is(UnitTypes.Archer))
                {
                    if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                    {
                        var xy_from = Cell<XyC>(idx_from).Xy;

                        var list_around = CellSpaceC.GetXyAround(xy_from);


                        foreach (var xy_1 in list_around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.GetHero);

                                EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Archer, Unit<LevelTC>(idx_from).Level, Unit<PlayerTC>(idx_from).Player, idx_from).Have = false;
                                Unit<UnitTC>(idx_from).Reset();

                                EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Archer, Unit<LevelTC>(idx_to).Level, Unit<PlayerTC>(idx_to).Player, idx_to).Have = false;
                                Unit<UnitTC>(idx_to).Reset();


                                Unit<UnitTC>(idx_to).Unit = unit;
                                Unit<LevelTC>(idx_to).Level = LevelTypes.First;

                                EntWhereUnits.HaveUnit<HaveUnitC>(unit, LevelTypes.First, Unit<PlayerTC>(idx_to).Player, idx_to).Have = true;


                                EntInventorUnits.Units<AmountC>(unit, LevelTypes.First, Unit<PlayerTC>(idx_to).Player).Amount -= 1;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}