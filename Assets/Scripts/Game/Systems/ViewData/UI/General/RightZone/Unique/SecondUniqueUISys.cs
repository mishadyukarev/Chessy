using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SecondUniqueUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var unit_sel = _cellUnitFilt.Get1(SelectorC.IdxSelCell);
            var ownUnit_sel = _cellUnitFilt.Get2(SelectorC.IdxSelCell);

            if (unit_sel.Is(UnitTypes.King))
            {
                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, true);
                }
                else RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, false);
            }
            else RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, false);
        }
    }
}