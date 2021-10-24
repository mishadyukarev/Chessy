using Leopotam.Ecs;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<CondUnitUICom> _condUIFilt = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;


        public void Run()
        {
            var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;
            ref var condUnitUICom = ref _condUIFilt.Get1(0);

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);


            var activeButt = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (!selUnitDatCom.Is(UnitTypes.Scout))
                {
                    if (selOnUnitCom.IsPlayerType(WhoseMoveCom.CurPlayer))
                    {
                        activeButt = true;

                        if (selUnitDatCom.Is(CondUnitTypes.Protected))
                        {
                            condUnitUICom.SetColor(CondUnitTypes.Protected, Color.yellow);
                        }

                        else
                        {
                            condUnitUICom.SetColor(CondUnitTypes.Protected, Color.white);
                        }

                        if (selUnitDatCom.Is(CondUnitTypes.Relaxed))
                        {
                            condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.green);
                        }
                        else
                        {
                            condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.white);
                        }
                    }
                }
            }


            condUnitUICom.SetActive(CondUnitTypes.Relaxed, activeButt);
        }
    }
}