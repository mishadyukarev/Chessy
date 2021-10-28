using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class ShieldUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);

            ExtraTWZoneUIC.DisableAll();

            if (selUnitC.HaveExtraTW)
            {
                ExtraTWZoneUIC.Toggle(selUnitC.TWExtraType, selUnitC.LevelTWType, true);
            }
        }
    }
}