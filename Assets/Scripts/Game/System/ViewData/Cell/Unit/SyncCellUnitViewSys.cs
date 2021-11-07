using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, ToolWeaponC> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerCom> _cellUnitLevFilter = default;
        private EcsFilter<VisibleC, CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitFilter)
            {
                ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levelUnitC_0 = ref _cellUnitLevFilter.Get2(idx_0);

                ref var twUnitC_0 = ref _cellUnitFilter.Get2(idx_0);

                ref var visUnitC_0 = ref _cellUnitViewFilt.Get1(idx_0);
                ref var mainUnitC_0 = ref _cellUnitViewFilt.Get2(idx_0);
                ref var extraUnitC_0 = ref _cellUnitViewFilt.Get3(idx_0);


                mainUnitC_0.Enable_SR(false);
                extraUnitC_0.Disable_SR();

                if (unitC_0.HaveUnit)
                {
                    if (visUnitC_0.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnitC_0.Enable_SR(true);
                        mainUnitC_0.SetSprite(unitC_0.Unit, levelUnitC_0.Level);


                        if (unitC_0.Is(UnitTypes.Pawn))
                        {
                            if (twUnitC_0.HaveToolWeap)
                            {
                                extraUnitC_0.Enable_SR();
                                extraUnitC_0.SetToolWeapon_Sprite(twUnitC_0.ToolWeapType, twUnitC_0.LevelTWType);
                            }
                        }

                        else if (unitC_0.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                        }


                        mainUnitC_0.SetAlpha(visUnitC_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                        extraUnitC_0.SetAlpha(visUnitC_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                    }
                }
            }
        }
    }
}
