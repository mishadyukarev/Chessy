using Game.Common;
using Leopotam.Ecs;
using UnityEngine;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class FromToNewUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var levUnit_from = ref Unit<LevelC>(idx_from);
            ref var ownUnit_from = ref Unit<OwnerC>(idx_from);

            ref var unit_to = ref Unit<UnitC>(idx_to);
            ref var levUnit_to = ref Unit<LevelC>(idx_to);
            ref var ownUnit_to = ref Unit<OwnerC>(idx_to);


            if (unit_from.Is(UnitTypes.Archer))
            {
                if (unit_to.Is(UnitTypes.Archer))
                {
                    if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                    {
                        var xy_from = Cell<XyC>(idx_from).Xy;

                        var list_around = CellSpaceC.XyAround(xy_from);

                        
                        foreach (var xy_1 in list_around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.GetHero);


                                WhereUnitsC.Set(UnitTypes.Archer, levUnit_from.Level, ownUnit_from.Owner, idx_from, false);
                                unit_from.Set(UnitTypes.None);

                                WhereUnitsC.Set(UnitTypes.Archer, levUnit_to.Level, ownUnit_to.Owner, idx_to, false);
                                unit_to.Set(UnitTypes.None);


                                unit_to.SetNew(UnitTypes.Archer, LevelTypes.First, ownUnit_to.Owner);

                                WhereUnitsC.Set(unit, levUnit_to.Level, ownUnit_to.Owner, idx_to, true);


                                InvUnitsC.Take(ownUnit_to.Owner, unit_to.Unit, levUnit_to.Level);


                                break;
                            }
                        }
                    }
                }
            }

        }
    }
}