using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, VisibleCom> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curVisUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var curMainUnitViewCom = ref _cellUnitViewFilt.Get1(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitViewFilt.Get2(idxCurCell);

                curMainUnitViewCom.Disable_SR();
                curExtraUnitViewCom.Disable_SR();


                if (curUnitDatCom.HaveUnit)
                {
                    if (curVisUnitCom.IsVisibled(WhoseMoveCom.CurPlayer))
                    {
                        curMainUnitViewCom.Enable_SR();
                        curMainUnitViewCom.SetSprite(curUnitDatCom.UnitType, curUnitDatCom.LevelUnitType);


                        if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            if (curUnitDatCom.HaveExtraTW)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.TWExtraType, curUnitDatCom.LevelTWType);
                            }
                        }

                        else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                        }


                        curMainUnitViewCom.SetAlpha(curVisUnitCom.IsVisibled(WhoseMoveCom.NextPlayerFrom(WhoseMoveCom.CurPlayer)));
                        curExtraUnitViewCom.SetAlpha(curVisUnitCom.IsVisibled(WhoseMoveCom.NextPlayerFrom(WhoseMoveCom.CurPlayer)));
                    }
                }
            }
        }
    }
}
