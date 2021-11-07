using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SyncCellSelUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom> _cellUnitViewFilt = default;
        private EcsFilter<SelectorC> _selectorFilter = default;

        public void Run()
        {
            if (SelectorC.IsSelUnit)
            {
                var idxCurCell = SelectorC.IdxCurCell;
                var idxPreCell = SelectorC.IdxPreVisionCell;


                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var curMainUnitViewCom = ref _cellUnitViewFilt.Get1(idxCurCell);
                ref var preVisMainUnitViewCom = ref _cellUnitViewFilt.Get1(idxPreCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curOwnUnitCom.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        preVisMainUnitViewCom.Enable_SR(true);
                        preVisMainUnitViewCom.SetSprite(SelectorC.SelUnitType, SelectorC.LevelSelUnitType);
                    }

                    else
                    {
                        curMainUnitViewCom.Enable_SR(true);
                        curMainUnitViewCom.SetSprite(SelectorC.SelUnitType, SelectorC.LevelSelUnitType);
                    }
                }

                else
                {
                    curMainUnitViewCom.Enable_SR(true);
                    curMainUnitViewCom.SetSprite(SelectorC.SelUnitType, SelectorC.LevelSelUnitType);
                }
            }
        }
    }
}
