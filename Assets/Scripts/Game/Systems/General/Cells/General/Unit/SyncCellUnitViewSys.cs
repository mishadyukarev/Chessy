using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, ToolWeaponC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var twUnitC = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curVisUnitCom = ref _cellUnitFilter.Get3(idxCurCell);

                ref var curMainUnitViewCom = ref _cellUnitViewFilt.Get1(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitViewFilt.Get2(idxCurCell);

                curMainUnitViewCom.Enable_SR(false);
                curExtraUnitViewCom.Disable_SR();


                if (curUnitDatCom.HaveUnit)
                {
                    if (curVisUnitCom.IsVisibled(WhoseMoveC.CurPlayer))
                    {
                        curMainUnitViewCom.Enable_SR(true);
                        curMainUnitViewCom.SetSprite(curUnitDatCom.UnitType, curUnitDatCom.LevelUnitType);


                        if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            if (twUnitC.HaveToolWeap)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(twUnitC.ToolWeapType, twUnitC.LevelTWType);
                            }
                        }

                        else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                        }


                        curMainUnitViewCom.SetAlpha(curVisUnitCom.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayer)));
                        curExtraUnitViewCom.SetAlpha(curVisUnitCom.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayer)));
                    }
                }
            }
        }
    }
}
