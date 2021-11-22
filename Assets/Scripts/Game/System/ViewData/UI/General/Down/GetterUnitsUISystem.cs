﻿using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game.Game
{
    public sealed class GetterUnitsUISystem : IEcsRunSystem
    {
        private const float NEEDED_TIME = 1;

        public void Run()
        {
            for (UnitTypes curUnitType = UnitTypes.Start; curUnitType < UnitTypes.End; curUnitType++)
            {
                if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Archer)
                {
                    if (GetterUnitsC.IsActivatedButton(curUnitType))
                    {
                        GetPawnArcherUIC.SetActiveCreateButton(curUnitType, true);
                        GetterUnitsC.AddTimer(curUnitType, Time.deltaTime);

                        if (GetterUnitsC.GetTimer(curUnitType) >= NEEDED_TIME)
                        {
                            GetPawnArcherUIC.SetActiveCreateButton(curUnitType, false);
                            GetterUnitsC.ActiveNeedCreateButton(curUnitType, false);
                            GetterUnitsC.ResetCurTimer(curUnitType);
                        }
                    }

                    else
                    {
                        GetPawnArcherUIC.SetActiveCreateButton(curUnitType, false);
                    }
                }
            }


            GetPawnArcherUIC.SetTextToAmountUnits(UnitTypes.Pawn, InvUnitsC.AmountUnits(UnitTypes.Pawn, WhoseMoveC.CurPlayerI).ToString());
            GetPawnArcherUIC.SetTextToAmountUnits(UnitTypes.Archer, InvUnitsC.AmountUnits(UnitTypes.Archer, WhoseMoveC.CurPlayerI).ToString());
        }
    }
}