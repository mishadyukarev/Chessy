using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class ShiftUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellEnvDataC> _cellEnvrDataFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;

        public void Run()
        {
            var idx_from = ForShiftMasCom.IdxFrom;
            var idx_to = ForShiftMasCom.IdxTo;

            var whoseMove = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(whoseMove, idx_from, idx_to))
            {
                ref var envDat_from = ref _cellEnvrDataFilter.Get1(idx_from);
                ref var trail_from = ref _cellTrailFilt.Get1(idx_from);

                ref var xy_to = ref _cellXyFilt.Get1(idx_to);
                ref var envDat_to = ref _cellEnvrDataFilter.Get1(idx_to);
                ref var river_to = ref _cellRiverFilt.Get1(idx_to);
                ref var trail_to = ref _cellTrailFilt.Get1(idx_to);


                ref var unit_from = ref _cellUnitFilter.Get1(idx_from);
                ref var levUnit_from = ref _cellUnitMainFilt.Get2(idx_from);
                ref var ownUnit_from = ref _cellUnitMainFilt.Get3(idx_from);
                ref var hpUnitC_from = ref _cellUnitFilter.Get2(idx_from);
                ref var stepUnitC_from = ref _cellUnitFilter.Get3(idx_from);
                ref var twUnitC_from = ref _cellUnitOthFilt.Get3(idx_from);
                ref var effUnitC_from = ref _cellUnitOthFilt.Get4(idx_from);
                ref var thirUnitC_from = ref _cellUnitOthFilt.Get5(idx_from);

                ref var unit_to = ref _cellUnitFilter.Get1(idx_to);
                ref var levUnitC_to = ref _cellUnitMainFilt.Get2(idx_to);
                ref var ownUnit_to = ref _cellUnitMainFilt.Get3(idx_to);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(idx_to);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(idx_to);
                ref var condUnitC_to = ref _cellUnitOthFilt.Get2(idx_to);
                ref var twUnitC_to = ref _cellUnitOthFilt.Get3(idx_to);
                ref var effUnitC_to = ref _cellUnitOthFilt.Get4(idx_to);
                ref var thirUnitC_to = ref _cellUnitOthFilt.Get5(idx_to);



                var dir_from = CellSpaceSupport.GetDirect(_cellXyFilt.Get1(idx_from).XyCell, _cellXyFilt.Get1(idx_to).XyCell);

                stepUnitC_from.TakeStepsForDoing(envDat_to, dir_from, trail_to);

                trail_to.TrySetNewTrain(dir_from.Invert(), envDat_to);
                trail_from.TrySetNewTrain(dir_from, envDat_from);


                ref var build_from = ref _cellBuildFilt.Get1(idx_from);
                ref var ownBuild_from = ref _cellBuildFilt.Get2(idx_from);
                if (build_from.Is(BuildTypes.Camp))
                {
                    WhereBuildsC.Remove(ownBuild_from.Owner, build_from.BuildType, idx_from);
                    build_from.Reset();
                }



                unit_to.SetUnit(unit_from.Unit);
                levUnitC_to.SetLevel(levUnit_from.Level);
                ownUnit_to.SetOwner(whoseMove);
                hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                stepUnitC_to.StepsAmount = stepUnitC_from.StepsAmount;
                condUnitC_to.DefCondition();
                twUnitC_to.Set(twUnitC_from);
                effUnitC_to.Set(effUnitC_from);
                thirUnitC_to.Set(thirUnitC_from);
                if (river_to.HaveNearRiver) thirUnitC_to.SetMaxWater(UnitsUpgC.UpgPercent(ownUnit_to.Owner, unit_to.Unit, UnitStatTypes.Water));
                WhereUnitsC.Add(ownUnit_to.Owner, unit_to.Unit, levUnitC_to.Level, idx_to);

                WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                unit_from.NoneUnit();

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipGameTypes.ClickToTable);
            }
        }
    }
}