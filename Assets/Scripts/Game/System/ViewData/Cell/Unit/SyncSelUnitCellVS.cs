using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncSelUnitCellVS : IEcsRunSystem
    {
        private EcsFilter<UnitC, VisibleC> _unitF = default;
        private EcsFilter<UnitMainVC> _unitVF = default;

        public void Run()
        {
            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                ref var unit_cur = ref _unitF.Get1(CurIdx.Idx);
                ref var ownUnit_cur = ref _unitF.Get2(CurIdx.Idx);

                ref var mainUnit_cur = ref _unitVF.Get1(CurIdx.Idx);
                ref var mainUnit_pre = ref _unitVF.Get1(IdxPreVis.Idx);


                if (unit_cur.HaveUnit)
                {
                    if (ownUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
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
