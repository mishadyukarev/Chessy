using Leopotam.Ecs;

namespace Chessy.Game
{
    public class ProtectUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);


            var isEnableButt = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (selUnitDatCom.Is(UnitTypes.Scout))
                {

                }
                else if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    isEnableButt = true;
                }
            }


            CondUnitUIC.SetActive(CondUnitTypes.Protected, isEnableButt);
        }
    }
}
