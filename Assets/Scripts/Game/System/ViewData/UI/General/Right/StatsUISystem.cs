using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, DamageC, StepC, WaterUnitC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        private EcsFilter<BuildC> _buildF = default;
        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            ref var unit_sel = ref _unitF.Get1(SelectorC.IdxSelCell);
            ref var levUnit_sel = ref _unitF.Get2(SelectorC.IdxSelCell);
            ref var ownUnit_sel = ref _unitF.Get3(SelectorC.IdxSelCell);

            ref var selHpUnitC = ref _statUnitF.Get1(SelectorC.IdxSelCell);
            ref var selDamUnitC = ref _statUnitF.Get2(SelectorC.IdxSelCell);
            ref var selStepUnitC = ref _statUnitF.Get3(SelectorC.IdxSelCell);
            ref var thirUnitC_sel = ref _statUnitF.Get4(SelectorC.IdxSelCell);

            ref var selConUnitC = ref _effUnitF.Get1(SelectorC.IdxSelCell);
            ref var effUnit_sel = ref _effUnitF.Get2(SelectorC.IdxSelCell);

            ref var twUnit_sel = ref _twUnitF.Get1(SelectorC.IdxSelCell);
            
            



            ref var selBuildC = ref _buildF.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _envF.Get1(SelectorC.IdxSelCell);


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