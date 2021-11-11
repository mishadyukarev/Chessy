using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class GetterUnitsUISystem : IEcsRunSystem
    {
        private const float NEEDED_TIME = 1;

        public void Run()
        {
            for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
            {
                if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Archer)
                {
                    if (GetterUnitsDataUIC.IsActivatedButton(curUnitType))
                    {
                        GetterUnitsViewUIC.SetActiveCreateButton(curUnitType, true);
                        GetterUnitsDataUIC.AddTimer(curUnitType, Time.deltaTime);

                        if (GetterUnitsDataUIC.GetTimer(curUnitType) >= NEEDED_TIME)
                        {
                            GetterUnitsViewUIC.SetActiveCreateButton(curUnitType, false);
                            GetterUnitsDataUIC.ActiveNeedCreateButton(curUnitType, false);
                            GetterUnitsDataUIC.ResetCurTimer(curUnitType);
                        }
                    }

                    else
                    {
                        GetterUnitsViewUIC.SetActiveCreateButton(curUnitType, false);
                    }
                }
            }


            GetterUnitsViewUIC.SetTextToAmountUnits(UnitTypes.Pawn, InvUnitsC.AmountUnits(WhoseMoveC.CurPlayerI, UnitTypes.Pawn).ToString());
            GetterUnitsViewUIC.SetTextToAmountUnits(UnitTypes.Archer, InvUnitsC.AmountUnits(WhoseMoveC.CurPlayerI, UnitTypes.Archer).ToString());
        }
    }
}