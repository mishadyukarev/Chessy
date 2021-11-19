using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SetterUnitMS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, MoveInCondC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            UnitDoingMC.Get(out var unit);
            IdxDoingMC.Get(out var idx_0);


            ref var env_0 = ref _envF.Get1(idx_0);
            ref var fire_0 = ref _fireF.Get1(idx_0);

            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var levUnit_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            ref var hp_0 = ref _statUnitF.Get1(idx_0);
            ref var step_0 = ref _statUnitF.Get2(idx_0);
            ref var water_0 = ref _statUnitF.Get3(idx_0);

            ref var cond_0 = ref _effUnitF.Get1(idx_0);
            ref var moveCond_0 = ref _effUnitF.Get2(idx_0);
            ref var eff_0 = ref _effUnitF.Get3(idx_0);

            ref var tw_0 = ref _twUnitF.Get1(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (CellsForSetUnitC.HaveIdxCell(whoseMove, idx_0))
            {
                unit_0.Set(unit);
                ownUnit_0.SetOwner(whoseMove);
                tw_0.ToolWeapon = default;
                eff_0.DefAllEffects();
                hp_0.SetMaxHp();
                if (cond_0.HaveCondition) cond_0.Reset();
                
                moveCond_0.ResetAll();
                if (InvUnitsC.Have(whoseMove, unit, LevelTypes.Second))
                {
                    InvUnitsC.TakeUnit(whoseMove, unit, LevelTypes.Second);
                    levUnit_0.SetLevel(LevelTypes.Second);
                }
                else
                {
                    InvUnitsC.TakeUnit(whoseMove, unit, LevelTypes.First);
                    levUnit_0.SetLevel(LevelTypes.First);
                }
                step_0.SetMaxSteps(unit, false, UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));
                water_0.SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));


                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}