using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct FromToNewUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unit = EntityMPool.CreateHeroFromTo<UnitTC>().Unit;
            EntityMPool.CreateHeroFromTo<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveE.WhoseMove.Player;

            ref var unit_from = ref CellUnitEntities.Else(idx_from).UnitC;
            ref var levUnit_from = ref CellUnitEntities.Else(idx_from).LevelC;
            ref var ownUnit_from = ref CellUnitEntities.Else(idx_from).OwnerC;

            ref var unit_to = ref CellUnitEntities.Else(idx_to).UnitC;
            ref var levUnit_to = ref CellUnitEntities.Else(idx_to).LevelC;
            ref var ownUnit_to = ref CellUnitEntities.Else(idx_to).OwnerC;


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
                                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.GetHero);

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitEntities.Else(idx_from).LevelC.Level, CellUnitEntities.Else(idx_from).OwnerC.Player, idx_from).Have = false;
                                    CellUnitEntities.Else(idx_from).UnitC.Reset();

                                    WhereUnitsE.HaveUnit(UnitTypes.Archer, CellUnitEntities.Else(idx_to).LevelC.Level, CellUnitEntities.Else(idx_to).OwnerC.Player, idx_to).Have = false;
                                    CellUnitEntities.Else(idx_to).UnitC.Reset();


                                    CellUnitEntities.Else(idx_to).UnitC.Unit = unit;
                                    CellUnitEntities.Else(idx_to).LevelC.Level = LevelTypes.First;

                                    WhereUnitsE.HaveUnit(unit, LevelTypes.First, CellUnitEntities.Else(idx_to).OwnerC.Player, idx_to).Have = true;


                                    InventorUnitsE.Units(unit, LevelTypes.First, CellUnitEntities.Else(idx_to).OwnerC.Player).Amount -= 1;

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