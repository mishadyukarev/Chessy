using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SecondUniqueUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom> _cellUnitFilt = default;

        public void Run()
        {
            var selUnitC = _cellUnitFilt.Get1(SelectorC.IdxSelCell);

            if (selUnitC.Is(UnitTypes.King))
            {
                RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, true);
            }
            else RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, false);
        }
    }
}