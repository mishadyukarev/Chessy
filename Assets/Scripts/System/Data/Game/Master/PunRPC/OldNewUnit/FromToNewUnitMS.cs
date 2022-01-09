﻿using static Game.Game.EntityCellPool;

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
                                RpcS.SoundToGeneral(sender, ClipTypes.GetHero);

                                Unit<UnitCellEC>(idx_to).SetHero(idx_from, unit, LevelTypes.First);

                                break;
                            }
                        }
                    }
                }
            }

        }
    }
}