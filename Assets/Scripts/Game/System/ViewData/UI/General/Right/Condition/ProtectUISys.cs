using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public class ProtectUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, OwnerC> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, ConditionUnitC> _cellOtherFilt = default;

        public void Run()
        {
            ref var unit_sel = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var ownUnit_sel = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);
            ref var cond_sel = ref _cellOtherFilt.Get2(SelectorC.IdxSelCell);


            var isEnableButt = false;

            if (unit_sel.HaveUnit)
            {
                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    isEnableButt = true;
                    ProtectUIC.SetZone(unit_sel.Unit);

                    if (cond_sel.Is(CondUnitTypes.Protected))
                    {
                        ProtectUIC.SetColor(Color.yellow);
                    }

                    else
                    {
                        ProtectUIC.SetColor(Color.white);
                    }
                }
            }

            ProtectUIC.SetActiveButton(isEnableButt);
        }
    }
}
