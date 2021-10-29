using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom,  HpUnitC, DamageComponent, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, OwnerCom> _cellUnitOtherFill = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selHpUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selDamUnitC = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);
            ref var selStepUnitC = ref _cellUnitFilter.Get4(SelectorC.IdxSelCell);

            ref var selConUnitC = ref _cellUnitOtherFill.Get2(SelectorC.IdxSelCell);
            ref var selTwUnitC = ref _cellUnitOtherFill.Get3(SelectorC.IdxSelCell);
            ref var selEffUnitC = ref _cellUnitOtherFill.Get4(SelectorC.IdxSelCell);
            ref var selOwnUnitC = ref _cellUnitOtherFill.Get5(SelectorC.IdxSelCell);


            ref var selBuildC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (selUnitC.HaveUnit)
            {
                StatZoneViewUIC.SetActiveStatZone(true);

                StatZoneViewUIC.SetTextToStat(StatTypes.Health, selHpUnitC.AmountHp.ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Damage, selDamUnitC.DamageOnCell(selUnitC, selConUnitC, selTwUnitC, selEffUnitC, selBuildC.BuildType, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Steps, selStepUnitC.AmountSteps.ToString());

                StatZoneViewUIC.FillAmountHp(StatTypes.Health, selHpUnitC.AmountHp, selHpUnitC.CurMaxHpUnit(selEffUnitC, selUnitC.UnitType));
                StatZoneViewUIC.FillAmountHp(StatTypes.Damage, selDamUnitC.DamageOnCell(selUnitC, selConUnitC, selTwUnitC, selEffUnitC, selBuildC.BuildType, selEnvC.Envronments), selDamUnitC.StandDamage(selUnitC));
                StatZoneViewUIC.FillAmountHp(StatTypes.Steps, selStepUnitC.AmountSteps, selStepUnitC.MaxSteps(selUnitC.UnitType));
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}