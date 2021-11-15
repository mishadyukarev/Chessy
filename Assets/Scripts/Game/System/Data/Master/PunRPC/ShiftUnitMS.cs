using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class ShiftUnitMS : IEcsRunSystem
    {
        private EcsFilter<XyC> _cellXyFilt = default;
        private EcsFilter<EnvC> _cellEnvrDataFilter = default;
        private EcsFilter<RiverC> _cellRiverFilt = default;
        private EcsFilter<TrailC> _cellTrailFilt = default;
        private EcsFilter<FireC> _cellFireFilt = default;
        private EcsFilter<BuildC, OwnerC> _buildFilt = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterUnitC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, MoveInCondC, UnitEffectsC> _effUnitF = default;

        private EcsFilter<ToolWeaponC> _twUnitF = default;
        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqUnitF = default;
        private EcsFilter<CornerArcherC> _archerF = default;


        public void Run()
        {
            var idx_from = ForShiftMasCom.IdxFrom;
            var idx_to = ForShiftMasCom.IdxTo;

            var whoseMove = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(whoseMove, idx_from, idx_to))
            {
                #region Unit

                ref var unit_from = ref _unitF.Get1(idx_from);
                ref var lev_from = ref _unitF.Get2(idx_from);
                ref var own_from = ref _unitF.Get3(idx_from);

                ref var hp_from = ref _statUnitF.Get1(idx_from);
                ref var step_from = ref _statUnitF.Get2(idx_from);
                ref var water_from = ref _statUnitF.Get3(idx_from);

                ref var moveCond_from = ref _effUnitF.Get2(idx_from);
                ref var eff_from = ref _effUnitF.Get3(idx_from);   
                
                ref var twUnit_from = ref _twUnitF.Get1(idx_from);
                ref var uniq_from = ref _uniqUnitF.Get1(idx_from);
                ref var cdUniq_from = ref _uniqUnitF.Get2(idx_from);
                ref var corner_from = ref _archerF.Get1(idx_from);



                ref var unit_to = ref _unitF.Get1(idx_to);
                ref var lev_to = ref _unitF.Get2(idx_to);
                ref var own_to = ref _unitF.Get3(idx_to);

                ref var hp_to = ref _statUnitF.Get1(idx_to);
                ref var step_to = ref _statUnitF.Get2(idx_to);
                ref var water_to = ref _statUnitF.Get3(idx_to);

                ref var cond_to = ref _effUnitF.Get1(idx_to);
                ref var moveCond_to = ref _effUnitF.Get2(idx_to);
                ref var effUnit_to = ref _effUnitF.Get3(idx_to);

                ref var twUnit_to = ref _twUnitF.Get1(idx_to);
                ref var uniq_to = ref _uniqUnitF.Get1(idx_to);
                ref var cdUniq_to = ref _uniqUnitF.Get2(idx_to);
                ref var corner_to = ref _archerF.Get1(idx_to);

                #endregion



                ref var fire_to = ref _cellFireFilt.Get1(idx_to);
                ref var build_to = ref _buildFilt.Get1(idx_to);
                ref var ownBuild_to = ref _buildFilt.Get2(idx_to);
                ref var xy_to = ref _cellXyFilt.Get1(idx_to);
                ref var env_to = ref _cellEnvrDataFilter.Get1(idx_to);
                ref var river_to = ref _cellRiverFilt.Get1(idx_to);
                ref var trail_to = ref _cellTrailFilt.Get1(idx_to);

                ref var envDat_from = ref _cellEnvrDataFilter.Get1(idx_from);
                ref var trail_from = ref _cellTrailFilt.Get1(idx_from);



                var dir_from = CellSpace.GetDirect(_cellXyFilt.Get1(idx_from).Xy, _cellXyFilt.Get1(idx_to).Xy);

                step_from.TakeStepsForDoing(env_to, dir_from, trail_to);

                trail_to.TrySetNewTrail(dir_from.Invert(), env_to);
                trail_from.TrySetNewTrail(dir_from, envDat_from);


                //ref var build_from = ref _cellBuildFilt.Get1(idx_from);
                //ref var ownBuild_from = ref _cellBuildFilt.Get2(idx_from);
                //if (build_from.Is(BuildTypes.Camp))
                //{
                //    WhereBuildsC.Remove(ownBuild_from.Owner, build_from.Build, idx_from);
                //    build_from.Reset();
                //}



                unit_to = unit_from;
                lev_to.SetLevel(lev_from.Level);
                own_to = own_from;
                hp_to = hp_from;
                step_to = step_from;
                if(cond_to.HaveCondition) cond_to.Reset();
                twUnit_to = twUnit_from;
                effUnit_to.Set(eff_from);
                water_to = water_from;
                moveCond_to.ResetAll();
                //uniq_to.Replace(uniq_from);
                cdUniq_to.Replace(cdUniq_from);
                corner_to = corner_from;
                if (river_to.HaveNearRiver) water_to.SetMaxWater(UnitPercUpgC.UpgPercent(own_to.Owner, unit_to.Unit, UnitStatTypes.Water));



                if (build_to.Is(BuildTypes.Camp))
                {
                    if (!ownBuild_to.Is(own_to.Owner))
                    {
                        WhereBuildsC.Remove(ownBuild_to.Owner, build_to.Build, idx_to);
                        build_to.Remove();
                    }
                }





                WhereUnitsC.Add(own_to.Owner, unit_to.Unit, lev_to.Level, idx_to);

                WhereUnitsC.Remove(own_from.Owner, unit_from.Unit, lev_from.Level, idx_from);
                unit_from.Reset();

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}