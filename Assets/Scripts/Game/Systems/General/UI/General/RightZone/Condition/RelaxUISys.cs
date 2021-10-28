using Leopotam.Ecs;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);


            var activeButt = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (!selUnitDatCom.Is(UnitTypes.Scout))
                {
                    if (selOnUnitCom.Is(WhoseMoveC.CurPlayer))
                    {
                        activeButt = true;

                        if (selUnitDatCom.Is(CondUnitTypes.Protected))
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Protected, Color.yellow);
                        }

                        else
                        {
                            CondUnitUIC.SetColor(CondUnitTypes.Protected, Color.white);
                        }

                        if (selUnitDatCom.Is(CondUnitTypes.Relaxed))
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