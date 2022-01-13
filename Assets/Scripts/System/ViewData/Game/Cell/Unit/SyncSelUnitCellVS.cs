using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SyncSelUnitCellVS : IEcsRunSystem
    {
        public void Run()
        {
            if (ClickerObject<CellClickC>().Is(CellClickTypes.SetUnit))
            {
                ref var unit_cur = ref Unit<UnitC>(CurIdx<IdxC>().Idx);

                ref var mainUnit_cur = ref EntityCellVPool.UnitV<UnitMainVC>(CurIdx<IdxC>().Idx);
                ref var mainUnit_pre = ref EntityCellVPool.UnitV<UnitMainVC>(PreVisIdx<IdxC>().Idx);


                if (unit_cur.Have)
                {
                    if (Unit<IsVisibledC>(EntWhoseMove.CurPlayerI, CurIdx<IdxC>().Idx).IsVisibled)
                    {
                        mainUnit_pre.SetEnabled(true);
                        mainUnit_pre.SetSprite(SelectedUnitEnt.Unit, SelectedUnitEnt.Level, false);
                    }

                    else
                    {
                        mainUnit_cur.SetEnabled(true);
                        mainUnit_cur.SetSprite(SelectedUnitEnt.Unit, SelectedUnitEnt.Level, false);
                    }
                }

                else
                {
                    mainUnit_cur.SetEnabled(true);
                    mainUnit_cur.SetSprite(SelectedUnitEnt.Unit, SelectedUnitEnt.Level, false);
                }
            }
        }
    }
}
