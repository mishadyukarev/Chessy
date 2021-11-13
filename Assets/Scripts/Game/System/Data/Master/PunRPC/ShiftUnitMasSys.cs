using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class ShiftUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _cellXyFilt = default;
        private EcsFilter<EnvC> _cellEnvrDataFilter = default;
        private EcsFilter<RiverC> _cellRiverFilt = default;
        private EcsFilter<TrailC> _cellTrailFilt = default;
        private EcsFilter<FireC> _cellFireFilt = default;
        private EcsFilter<BuildC, OwnerC> _buildFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC, StepC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;
        private EcsFilter<UnitC, ConditionUnitC, MoveInCondC> _cellUnitCondFilt = default;
        private EcsFilter<UnitC, UniqAbilC> _unitUniqAbilFilt = default;
        private EcsFilter<CornerArcherC> _archerFilt = default;



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
                ref var env_to = ref _cellEnvrDataFilter.Get1(idx_to);
                ref var river_to = ref _cellRiverFilt.Get1(idx_to);
                ref var trail_to = ref _cellTrailFilt.Get1(idx_to);


                ref var unit_from = ref _cellUnitFilter.Get1(idx_from);
                ref var levUnit_from = ref _cellUnitMainFilt.Get2(idx_from);
                ref var ownUnit_from = ref _cellUnitMainFilt.Get3(idx_from);
                ref var hpUnit_from = ref _cellUnitFilter.Get2(idx_from);
                ref var stepUnit_from = ref _cellUnitFilter.Get3(idx_from);
                ref var twUnit_from = ref _cellUnitOthFilt.Get2(idx_from);
                ref var effUnit_from = ref _cellUnitOthFilt.Get3(idx_from);
                ref var thirUnit_from = ref _cellUnitOthFilt.Get4(idx_from);
                ref var moveCond_from = ref _cellUnitCondFilt.Get3(idx_from);
                ref var uniq_from = ref _unitUniqAbilFilt.Get2(idx_from);
                ref var corner_from = ref _archerFilt.Get1(idx_from);


                ref var unit_to = ref _cellUnitFilter.Get1(idx_to);
                ref var levUnitC_to = ref _cellUnitMainFilt.Get2(idx_to);
                ref var ownUnit_to = ref _cellUnitMainFilt.Get3(idx_to);
                ref var hpUnit_to = ref _cellUnitFilter.Get2(idx_to);
                ref var stepUnit_to = ref _cellUnitFilter.Get3(idx_to);
                ref var twUnit_to = ref _cellUnitOthFilt.Get2(idx_to);
                ref var effUnit_to = ref _cellUnitOthFilt.Get3(idx_to);
                ref var thirUnit_to = ref _cellUnitOthFilt.Get4(idx_to);
                ref var condUnit_to = ref _cellUnitCondFilt.Get2(idx_to);
                ref var moveCond_to = ref _cellUnitCondFilt.Get3(idx_to);
                ref var uniq_to = ref _unitUniqAbilFilt.Get2(idx_to);
                ref var corner_to = ref _archerFilt.Get1(idx_to);

                ref var fire_to = ref _cellFireFilt.Get1(idx_to);
                ref var build_to = ref _buildFilt.Get1(idx_to);
                ref var ownBuild_to = ref _buildFilt.Get2(idx_to);



                var dir_from = CellSpaceSupport.GetDirect(_cellXyFilt.Get1(idx_from).Xy, _cellXyFilt.Get1(idx_to).Xy);

                stepUnit_from.TakeStepsForDoing(env_to, dir_from, trail_to);

                trail_to.TrySetNewTrail(dir_from.Invert(), env_to);
                trail_from.TrySetNewTrail(dir_from, envDat_from);


                //ref var build_from = ref _cellBuildFilt.Get1(idx_from);
                //ref var ownBuild_from = ref _cellBuildFilt.Get2(idx_from);
                //if (build_from.Is(BuildTypes.Camp))
                //{
                //    WhereBuildsC.Remove(ownBuild_from.Owner, build_from.Build, idx_from);
                //    build_from.Reset();
                //}



                unit_to.SetUnit(unit_from.Unit);
                levUnitC_to.SetLevel(levUnit_from.Level);
                ownUnit_to = ownUnit_from;
                hpUnit_to = hpUnit_from;
                stepUnit_to = stepUnit_from;
                if(condUnit_to.HaveCondition) condUnit_to.Def();
                twUnit_to = twUnit_from;
                effUnit_to.Set(effUnit_from);
                thirUnit_to = thirUnit_from;
                moveCond_to.ResetAll();
                uniq_to.Replace(uniq_from);
                corner_to = corner_from;
                if (river_to.HaveNearRiver) thirUnit_to.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_to.Owner, unit_to.Unit, UnitStatTypes.Water));



                if (build_to.Is(BuildTypes.Camp))
                {
                    if (!ownBuild_to.Is(ownUnit_to.Owner))
                    {
                        WhereBuildsC.Remove(ownBuild_to.Owner, build_to.Type, idx_to);
                        build_to.Remove();
                    }
                }





                WhereUnitsC.Add(ownUnit_to.Owner, unit_to.Unit, levUnitC_to.Level, idx_to);

                WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                unit_from.Reset();

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipGameTypes.ClickToTable);
            }
        }
    }
}