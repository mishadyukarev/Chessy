using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, ConditionUnitC, OwnerC> _cellUnitFilter = default;

        public void Run()
        {
            ref var unit_sel = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selCondUnitC = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);


            var activeButt = false;

            if (unit_sel.HaveUnit)
            {
                if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    activeButt = true;

                    if (selCondUnitC.Is(CondUnitTypes.Relaxed))
                    {
                        RelaxUIC.SetColor(Color.green);
                    }
                    else
                    {
                        RelaxUIC.SetColor(Color.white);
                    }

                    RelaxUIC.SetZone(unit_sel.Unit);
                }
            }

            RelaxUIC.SetActiveButton(activeButt);
        }
    }
}