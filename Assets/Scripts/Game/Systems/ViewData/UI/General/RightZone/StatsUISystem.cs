using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, DamageC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOtherFill = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

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

                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Hp, selHpUnitC.AmountHp.ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Damage, selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, UnitsUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage), selBuildC.BuildType, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Steps, selStepUnitC.StepsAmount.ToString());
                StatZoneViewUIC.SetTextToStat(UnitStatTypes.Water, thirUnitC_sel.WaterAmount.ToString());

                StatZoneViewUIC.FillAmount(UnitStatTypes.Hp, selHpUnitC.AmountHp, selHpUnitC.MaxHpUnit(unit_sel.Unit, effUnit_sel.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Hp)));
                StatZoneViewUIC.FillAmount(UnitStatTypes.Damage, selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, UnitsUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage), selBuildC.BuildType, selEnvC.Envronments), selDamUnitC.DamageAttack(unit_sel.Unit, levUnit_sel.Level, twUnit_sel, effUnit_sel, AttackTypes.Simple, UnitsUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Damage)));
                StatZoneViewUIC.FillAmount(UnitStatTypes.Steps, selStepUnitC.StepsAmount, selStepUnitC.MaxSteps(effUnit_sel, unit_sel.Unit, UnitsUpgC.UpgSteps(ownUnit_sel.Owner, unit_sel.Unit)));
                StatZoneViewUIC.FillAmount(UnitStatTypes.Water, thirUnitC_sel.WaterAmount, thirUnitC_sel.MaxWater(UnitsUpgC.UpgPercent(ownUnit_sel.Owner, unit_sel.Unit, UnitStatTypes.Water)));
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}