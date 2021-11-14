using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<ConditionUnitC> _effUnitF = default;

        public void Run()
        {
            ref var unit_sel = ref _unitF.Get1(IdxSel.Idx);
            ref var selOnUnitCom = ref _unitF.Get2(IdxSel.Idx);

            ref var selCondUnitC = ref _effUnitF.Get1(IdxSel.Idx);  


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