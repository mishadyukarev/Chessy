using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncCellSelUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, CellUnitMainViewCom> _cellUnitFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        public void Run()
        {
            ref var selCom = ref _selectorFilter.Get1(0);



            if (selCom.IsSelUnit)
            {
                var idxCurCell = selCom.IdxCurCell;
                var idxPreCell = selCom.IdxPreVisionCell;


                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var preVisMainUnitViewCom = ref _cellUnitFilter.Get2(idxPreCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curUnitDatCom.IsVisibleUnit(WhoseMoveCom.CurPlayer))
                    {
                        preVisMainUnitViewCom.Enable_SR();
                        preVisMainUnitViewCom.SetSprite(selCom.SelUnitType, selCom.LevelSelUnitType);
                    }

                    else
                    {
                        curMainUnitViewCom.Enable_SR();
                        curMainUnitViewCom.SetSprite(selCom.SelUnitType, selCom.LevelSelUnitType);
                    }
                }

                else
                {
                    curMainUnitViewCom.Enable_SR();
                    curMainUnitViewCom.SetSprite(selCom.SelUnitType, selCom.LevelSelUnitType);
                }
            }
        }
    }
}
