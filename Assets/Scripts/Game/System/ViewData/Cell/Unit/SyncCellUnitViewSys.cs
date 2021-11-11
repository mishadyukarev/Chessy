using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, ToolWeaponC> _cellUnitFilter = default;
        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitLevFilter = default;
        private EcsFilter<CornerArcherC> _archerFilt = default;

        private EcsFilter<VisibleC, CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levelUnit_0 = ref _cellUnitLevFilter.Get2(idx_0);
                ref var corner_0 = ref _archerFilt.Get1(idx_0);

                ref var twUnitC_0 = ref _cellUnitFilter.Get2(idx_0);

                ref var visUnitC_0 = ref _cellUnitViewFilt.Get1(idx_0);
                ref var mainUnitC_0 = ref _cellUnitViewFilt.Get2(idx_0);
                ref var extraUnitC_0 = ref _cellUnitViewFilt.Get3(idx_0);


                mainUnitC_0.SetEnabled_SR(false);
                extraUnitC_0.Disable_SR();

                if (unit_0.HaveUnit)
                {
                    if (visUnitC_0.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnitC_0.SetEnabled_SR(true);

                        if (unit_0.Is(UnitTypes.Pawn))
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);

                            if (twUnitC_0.HaveToolWeap)
                            {
                                extraUnitC_0.Enable_SR();
                                extraUnitC_0.SetToolWeapon_Sprite(twUnitC_0.ToolWeapType, twUnitC_0.LevelTWType);
                            }
                        }

                        else if (unit_0.Is(UnitTypes.Archer))
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, corner_0.IsCornered);
                        }

                        else
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);
                        }


                        mainUnitC_0.SetAlpha(visUnitC_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                        extraUnitC_0.SetAlpha(visUnitC_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                    }
                }
            }
        }
    }
}
