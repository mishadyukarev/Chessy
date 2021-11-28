using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncSelUnitCellVS : IEcsRunSystem
    {
        public void Run()
        {
            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                ref var unit_cur = ref EntityPool.Unit<UnitC>(CurIdx.Idx);
                ref var visUnit_cur = ref EntityPool.Unit<VisibleC>(CurIdx.Idx);

                ref var mainUnit_cur = ref EntityVPool.UnitCellVC<UnitMainVC>(CurIdx.Idx);
                ref var mainUnit_pre = ref EntityVPool.UnitCellVC<UnitMainVC>(IdxPreVis.Idx);


                if (unit_cur.HaveUnit)
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
