using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ShiftUnitMS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _cellEnvrDataFilter = default;
        private EcsFilter<RiverC> _cellRiverFilt = default;
        private EcsFilter<FireC> _cellFireFilt = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionC, MoveInCondC, EffectsC> _effUnitF = default;

        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqUnitF = default;
        private EcsFilter<CornerArcherC> _archerF = default;


        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;


            if (ShiftCellsC.HaveIdxCell(whoseMove, idx_from, idx_to))
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

                ref var uniq_from = ref _uniqUnitF.Get1(idx_from);
                ref var cdUniq_from = ref _uniqUnitF.Get2(idx_from);
                ref var corner_from = ref _archerF.Get1(idx_from);

                ref var tw_from = ref UnitToolWeapon<ToolWeaponC>(idx_from);
                ref var twLevel_from = ref UnitToolWeapon<LevelC>(idx_from);
                ref var twShield_from = ref UnitToolWeapon<ShieldProtectionC>(idx_from);



                ref var unit_to = ref _unitF.Get1(idx_to);
                ref var lev_to = ref _unitF.Get2(idx_to);
                ref var own_to = ref _unitF.Get3(idx_to);

                ref var hp_to = ref _statUnitF.Get1(idx_to);
                ref var step_to = ref _statUnitF.Get2(idx_to);
                ref var water_to = ref _statUnitF.Get3(idx_to);

                ref var cond_to = ref _effUnitF.Get1(idx_to);
                ref var moveCond_to = ref _effUnitF.Get2(idx_to);
                ref var effUnit_to = ref _effUnitF.Get3(idx_to);

                ref var uniq_to = ref _uniqUnitF.Get1(idx_to);
                ref var cdUniq_to = ref _uniqUnitF.Get2(idx_to);
                ref var corner_to = ref _archerF.Get1(idx_to);

                ref var tw_to = ref UnitToolWeapon<ToolWeaponC>(idx_to);
                ref var twLevel_to = ref UnitToolWeapon<LevelC>(idx_to);
                ref var twShield_to = ref UnitToolWeapon<ShieldProtectionC>(idx_to);

                #endregion



                ref var fire_to = ref _cellFireFilt.Get1(idx_to);
                ref var build_to = ref Build<BuildC>(idx_to);
                ref var ownBuild_to = ref Build<OwnerC>(idx_to);
                ref var xy_to = ref Cell<XyC>(idx_to);
                ref var env_to = ref _cellEnvrDataFilter.Get1(idx_to);
                ref var river_to = ref _cellRiverFilt.Get1(idx_to);
                ref var trail_to = ref Trail<TrailC>(idx_to);



                var dir_from = CellSpaceC.GetDirect(Cell<XyC>(idx_from).Xy, Cell<XyC>(idx_to).Xy);

                UnitStat<UnitStatC>(idx_from).TakeStepsForDoing(env_to, dir_from, trail_to);

                unit_to.Shift(idx_from, dir_from);

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}