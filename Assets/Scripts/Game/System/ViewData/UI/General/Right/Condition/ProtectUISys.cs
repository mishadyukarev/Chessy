﻿using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public class ProtectUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<ConditionUnitC> _effUnitF = default;

        public void Run()
        {
            ref var unit_sel = ref _unitF.Get1(IdxSel.Idx);
            ref var ownUnit_sel = ref _unitF.Get2(IdxSel.Idx);
            ref var cond_sel = ref _effUnitF.Get1(IdxSel.Idx);


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
