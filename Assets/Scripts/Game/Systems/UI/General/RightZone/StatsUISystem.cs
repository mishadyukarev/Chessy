using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

            ref var selBuildC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            if (selUnitC.HaveUnit)
            {
                StatZoneViewUIC.SetActiveStatZone(true);

                StatZoneViewUIC.SetTextToStat(StatTypes.Health, selUnitC.AmountHealth.ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Damage, selUnitC.PowerDamageOnCell(selBuildC.BuildType, selEnvC.Envronments).ToString());
                StatZoneViewUIC.SetTextToStat(StatTypes.Steps, selUnitC.AmountSteps.ToString());
            }

            else
            {
                StatZoneViewUIC.SetActiveStatZone(false);
            }
        }
    }
}