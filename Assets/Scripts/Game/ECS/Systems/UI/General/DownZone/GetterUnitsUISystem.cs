﻿using Leopotam.Ecs;
using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class GetterUnitsUISystem : IEcsRunSystem
    {
        private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUnitsUIFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventUnitsFilter = default;

        private const float NEEDED_TIME = 1;

        public void Run()
        {
            ref var getUnitDatCom = ref _takerUnitsUIFilter.Get1(0);
            ref var getUnitViewCom = ref _takerUnitsUIFilter.Get2(0);
            ref var invUnitCom = ref _inventUnitsFilter.Get1(0);


            for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
            {
                if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Rook || curUnitType == UnitTypes.Bishop)
                {
                    if (getUnitDatCom.IsActivatedButton(curUnitType))
                    {
                        getUnitViewCom.SetActiveCreateButton(curUnitType, true);
                        getUnitDatCom.AddTimer(curUnitType, Time.deltaTime);

                        if (getUnitDatCom.GetTimer(curUnitType) >= NEEDED_TIME)
                        {
                            getUnitViewCom.SetActiveCreateButton(curUnitType, false);
                            getUnitDatCom.ActiveNeedCreateButton(curUnitType, false);
                            getUnitDatCom.ResetCurTimer(curUnitType);
                        }
                    }

                    else
                    {
                        getUnitViewCom.SetActiveCreateButton(curUnitType, false);
                    }
                }
            }


            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Pawn, invUnitCom.AmountUnitsInInv(WhoseMoveCom.CurPlayer, UnitTypes.Pawn).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Rook, invUnitCom.AmountUnitsInInv(WhoseMoveCom.CurPlayer, UnitTypes.Rook).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Bishop, invUnitCom.AmountUnitsInInv(WhoseMoveCom.CurPlayer, UnitTypes.Bishop).ToString());
        }
    }
}