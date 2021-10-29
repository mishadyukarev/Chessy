using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpComponent, DamageComponent, StepComponent, ToolWeaponC, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selHpUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selDamUnitC = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);
            ref var selStepUnitC = ref _cellUnitFilter.Get4(SelectorC.IdxSelCell);
            ref var selTwUnitC =ref _cellUnitFilter.Get5(SelectorC.IdxSelCell);
            ref var selOwnUnitC = ref _cellUnitFilter.Get6(SelectorC.IdxSelCell);

            ref var selBuildC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            if (selUnitC.HaveUnit)
            {
                StatZoneViewUIC.SetActiveStatZone(true);

                StatZoneViewUIC.SetTextToStat(StatTypes.Health, selHpUnitC.AmountHealth.ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Damage, selDamUnitC.PowerDamageOnCell(selUnitC, selTwUnitC, selBuildC.BuildType, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Steps, selStepUnitC.AmountSteps.ToString());

                StatZoneViewUIC.FillAmountHp(StatTypes.Health, selHpUnitC.AmountHealth, selHpUnitC.MaxAmountHealth(selUnitC.UnitType));
                StatZoneViewUIC.FillAmountHp(StatTypes.Damage, selDamUnitC.PowerDamageOnCell(selUnitC, selTwUnitC, selBuildC.BuildType, selEnvC.Envronments), selDamUnitC.StandPowerDamage(selUnitC));
                StatZoneViewUIC.FillAmountHp(StatTypes.Steps, selStepUnitC.AmountSteps, selStepUnitC.MaxAmountSteps(selUnitC.UnitType));
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}