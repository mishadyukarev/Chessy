using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;

        private EcsFilter<EnvC> _cellEnvirDataFilter = default;
        private EcsFilter<FireC> _fireFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC, StepC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;
        private EcsFilter<UnitC, ConditionUnitC, MoveInCondC> _cellUnitCondFilt = default;
        private EcsFilter<UnitC, UniqAbilC> _unitUniqAbilFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unitForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idx_0 = _setterFilter.Get1(0).IdxCellForSetting;


            ref var env_0 = ref _cellEnvirDataFilter.Get1(idx_0);
            ref var fire_0 = ref _fireFilt.Get1(idx_0);

            ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
            ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
            ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idx_0);
            ref var curTwUnitC = ref _cellUnitOthFilt.Get2(idx_0);
            ref var effUnit_0 = ref _cellUnitOthFilt.Get3(idx_0);
            ref var thirUnitC_0 = ref _cellUnitOthFilt.Get4(idx_0);
            ref var condUnit_0 = ref _cellUnitCondFilt.Get2(idx_0);
            ref var moveCond_0 = ref _cellUnitCondFilt.Get3(idx_0);
            ref var firstUniq_0 = ref _unitUniqAbilFilt.Get2(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (CellsForSetUnitC.HaveIdxCell(whoseMove, idx_0))
            {
                unit_0.SetUnit(unitForSet);
                ownUnit_0.SetOwner(whoseMove);
                curTwUnitC.ToolWeapType = default;
                effUnit_0.DefAllEffects();
                hpUnit_0.SetMaxHp();
                stepUnitC.SetMaxSteps(unitForSet, false, UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                if(condUnit_0.HaveCondition) condUnit_0.Def();
                thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
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