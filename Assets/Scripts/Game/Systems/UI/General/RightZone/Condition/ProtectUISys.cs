using Leopotam.Ecs;

namespace Scripts.Game
{
    internal class ProtectUISys : IEcsRunSystem
    {
        private EcsFilter<CondUnitUICom> _condUnitUIFilt = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;
            ref var condUnitUICom = ref _condUnitUIFilt.Get1(0);

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);


            var isEnableButt = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (selUnitDatCom.Is(UnitTypes.Scout))
                {

                }
                else if (selOnUnitCom.IsPlayerType(WhoseMoveCom.CurPlayer))
                {
                    isEnableButt = true;
                }
            }


            condUnitUICom.SetActive(CondUnitTypes.Protected, isEnableButt);
        }
    }
}
