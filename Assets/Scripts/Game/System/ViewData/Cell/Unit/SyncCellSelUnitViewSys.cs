﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SyncCellSelUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom> _cellUnitViewFilt = default;

        public void Run()
        {
            if (SelUnitC.IsSelUnit)
            {
                var idxCurCell = SelectorC.IdxCurCell;
                var idxPreCell = SelectorC.IdxPreVisionCell;


                ref var unit_cur = ref _cellUnitFilter.Get1(idxCurCell);
                ref var ownUnit_cur = ref _cellUnitFilter.Get2(idxCurCell);

                ref var mainUnit_cur = ref _cellUnitViewFilt.Get1(idxCurCell);
                ref var mainUnit_pre = ref _cellUnitViewFilt.Get1(idxPreCell);


                if (unit_cur.HaveUnit)
                {
                    if (ownUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnit_pre.Enable_SR(true);
                        mainUnit_pre.SetSprite(SelUnitC.SelUnit, SelUnitC.LevelSelUnit);
                    }

                    else
                    {
                        mainUnit_cur.Enable_SR(true);
                        mainUnit_cur.SetSprite(SelUnitC.SelUnit, SelUnitC.LevelSelUnit);
                    }
                }

                else
                {
                    mainUnit_cur.Enable_SR(true);
                    mainUnit_cur.SetSprite(SelUnitC.SelUnit, SelUnitC.LevelSelUnit);
                }
            }
        }
    }
}
