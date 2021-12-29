using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class SyncSelUnitCellVS : IEcsRunSystem
    {
        public void Run()
        {
            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                ref var unit_cur = ref Unit<UnitC>(CurIdx<IdxC>().Idx);
                ref var visUnit_cur = ref Unit<VisibleC>(CurIdx<IdxC>().Idx);

                ref var mainUnit_cur = ref EntityCellVPool.UnitCellVC<UnitMainVC>(CurIdx<IdxC>().Idx);
                ref var mainUnit_pre = ref EntityCellVPool.UnitCellVC<UnitMainVC>(PreVisIdx<IdxC>().Idx);


                if (unit_cur.Have)
                {
                    if (visUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnit_pre.SetEnabled(true);
                        mainUnit_pre.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                    }

                    else
                    {
                        mainUnit_cur.SetEnabled(true);
                        mainUnit_cur.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                    }
                }

                else
                {
                    mainUnit_cur.SetEnabled(true);
                    mainUnit_cur.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                }
            }
        }
    }
}
