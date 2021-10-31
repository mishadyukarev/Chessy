using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom,  HpUnitC, DamageComponent, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyUnitC> _cellUnitOtherFill = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);

            ref var levUnitC_sel = ref _cellUnitMainFilt.Get2(SelectorC.IdxSelCell);
            ref var ownUnitC_sel = ref _cellUnitMainFilt.Get3(SelectorC.IdxSelCell);

            ref var selHpUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selDamUnitC = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);
            ref var selStepUnitC = ref _cellUnitFilter.Get4(SelectorC.IdxSelCell);

            ref var selConUnitC = ref _cellUnitOtherFill.Get2(SelectorC.IdxSelCell);
            ref var selTwUnitC = ref _cellUnitOtherFill.Get3(SelectorC.IdxSelCell);
            ref var selEffUnitC = ref _cellUnitOtherFill.Get4(SelectorC.IdxSelCell);
            ref var thirUnitC_sel = ref _cellUnitOtherFill.Get5(SelectorC.IdxSelCell);
           


            ref var selBuildC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (selUnitC.HaveUnit)
            {
                StatZoneViewUIC.SetActiveStatZone(true);

                StatZoneViewUIC.SetTextToStat(StatTypes.Health, selHpUnitC.AmountHp.ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Damage, selDamUnitC.DamageOnCell(selUnitC.UnitType, levUnitC_sel.LevelUnitType, selConUnitC, selTwUnitC, selEffUnitC, selBuildC.BuildType, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Steps, selStepUnitC.StepsAmount.ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Water, thirUnitC_sel.WaterAmount.ToString());

                StatZoneViewUIC.FillAmount(StatTypes.Health, selHpUnitC.AmountHp, selHpUnitC.MaxHpUnit(selEffUnitC, selUnitC.UnitType));
                StatZoneViewUIC.FillAmount(StatTypes.Damage, selDamUnitC.DamageOnCell(selUnitC.UnitType, levUnitC_sel.LevelUnitType, selConUnitC, selTwUnitC, selEffUnitC, selBuildC.BuildType, selEnvC.Envronments), selDamUnitC.StandDamage(selUnitC.UnitType, levUnitC_sel.LevelUnitType));
                StatZoneViewUIC.FillAmount(StatTypes.Steps, selStepUnitC.StepsAmount, selStepUnitC.MaxSteps(selEffUnitC, selUnitC.UnitType));
                StatZoneViewUIC.FillAmount(StatTypes.Water, thirUnitC_sel.WaterAmount, thirUnitC_sel.MaxWater(selUnitC.UnitType));
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}