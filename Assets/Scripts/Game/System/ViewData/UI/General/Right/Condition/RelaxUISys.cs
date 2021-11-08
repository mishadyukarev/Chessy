using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, ConditionUnitC, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selCondUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);


            var activeButt = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (!selUnitDatCom.Is(UnitTypes.Scout))
                {
                    if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        activeButt = true;

                        if (selCondUnitC.Is(CondUnitTypes.Protected))
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Protected, Color.yellow);
                        }

                        else
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Protected, Color.white);
                        }

                        if (selCondUnitC.Is(CondUnitTypes.Relaxed))
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Relaxed, Color.green);
                        }
                        else
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Relaxed, Color.white);
                        }
                    }
                }
            }


            CondUnitUIC.SetActive(CondUnitTypes.Relaxed, activeButt);
        }
    }
}