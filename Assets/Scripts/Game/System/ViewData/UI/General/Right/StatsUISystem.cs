using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC, DamageC, StepC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ConditionUnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOtherFill = default;
        private EcsFilter<BuildC> _cellBuildFilter = default;
        private EcsFilter<EnvC> _cellEnvFilter = default;

        public void Run()
        {
            ref var unit_sel = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);

            ref var levUnit_sel = ref _cellUnitMainFilt.Get2(SelectorC.IdxSelCell);
            ref var ownUnit_sel = ref _cellUnitMainFilt.Get3(SelectorC.IdxSelCell);

            ref var selHpUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selDamUnitC = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);
            ref var selStepUnitC = ref _cellUnitFilter.Get4(SelectorC.IdxSelCell);

            ref var selConUnitC = ref _cellUnitOtherFill.Get2(SelectorC.IdxSelCell);
            ref var twUnit_sel = ref _cellUnitOtherFill.Get3(SelectorC.IdxSelCell);
            ref var effUnit_sel = ref _cellUnitOtherFill.Get4(SelectorC.IdxSelCell);
            ref var thirUnitC_sel = ref _cellUnitOtherFill.Get5(SelectorC.IdxSelCell);



            ref var selBuildC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (unit_sel.HaveUnit)
            {
                StatZoneViewUIC.SetActiveStatZone(true);

                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Hp, selHpUnitC.Hp.ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Damage, selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, UnitPercUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage), selBuildC.Build, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Steps, selStepUnitC.Steps.ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Water, thirUnitC_sel.Water.ToString());

                StatZoneViewUIC.FillAmount(UnitStatTypes.Hp, selHpUnitC.Hp, HpC.MAX_HP);
                StatZoneViewUIC.FillAmount(UnitStatTypes.Damage, selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, UnitPercUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage), selBuildC.Build, selEnvC.Envronments), selDamUnitC.DamageAttack(unit_sel.Unit, levUnit_sel.Level, twUnit_sel, effUnit_sel, AttackTypes.Simple, UnitPercUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage)));
                StatZoneViewUIC.FillAmount(UnitStatTypes.Steps, selStepUnitC.Steps, selStepUnitC.MaxSteps(unit_sel.Unit, effUnit_sel.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_sel.Owner, unit_sel.Unit)));
                StatZoneViewUIC.FillAmount(UnitStatTypes.Water, thirUnitC_sel.Water, thirUnitC_sel.MaxWater(UnitPercUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Water)));
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}