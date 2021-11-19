using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncCellSelUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<UnitMainVC> _cellUnitViewFilt = default;

        public void Run()
        {
            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                var idxCurCell = CurIdx.Idx;
                var idxPreCell = IdxPreVis.Idx;


                ref var unit_cur = ref _cellUnitFilter.Get1(idxCurCell);
                ref var ownUnit_cur = ref _cellUnitFilter.Get2(idxCurCell);

                ref var mainUnit_cur = ref _cellUnitViewFilt.Get1(idxCurCell);
                ref var mainUnit_pre = ref _cellUnitViewFilt.Get1(idxPreCell);


                if (unit_cur.HaveUnit)
                {
                    if (ownUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnit_pre.SetEnabled_SR(true);
                        mainUnit_pre.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                    }

                    else
                    {
                        mainUnit_cur.SetEnabled_SR(true);
                        mainUnit_cur.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                    }
                }

                else
                {
                    mainUnit_cur.SetEnabled_SR(true);
                    mainUnit_cur.SetSprite(SelUnitC.Unit, SelUnitC.Level, false);
                }
            }
        }
    }
}
