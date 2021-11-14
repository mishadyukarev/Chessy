using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;

        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireFilt = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterUnitC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, MoveInCondC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unitForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idx_0 = _setterFilter.Get1(0).IdxCellForSetting;


            ref var env_0 = ref _envF.Get1(idx_0);
            ref var fire_0 = ref _fireFilt.Get1(idx_0);

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
                unit_0.Set(unitForSet);
                ownUnit_0.SetOwner(whoseMove);
                tw_0.ToolWeapType = default;
                eff_0.DefAllEffects();
                hp_0.SetMaxHp();
                step_0.SetMaxSteps(unitForSet, false, UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                if(cond_0.HaveCondition) cond_0.Reset();
                water_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                moveCond_0.ResetAll();
                if (InvUnitsC.Have(whoseMove, unitForSet, LevelUnitTypes.Second))
                {
                    InvUnitsC.TakeUnit(whoseMove, unitForSet, LevelUnitTypes.Second);
                    levUnit_0.SetLevel(LevelUnitTypes.Second);
                }
                else
                {
                    InvUnitsC.TakeUnit(whoseMove, unitForSet, LevelUnitTypes.First);
                    levUnit_0.SetLevel(LevelUnitTypes.First);
                }


                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                if (unitForSet == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
            }
        }
    }
}