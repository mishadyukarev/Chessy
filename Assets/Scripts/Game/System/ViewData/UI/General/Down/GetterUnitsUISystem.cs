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
                    if (GetterUnitsC.IsActivatedButton(curUnitType))
                    {
                        GetterUnitsViewUIC.SetActiveCreateButton(curUnitType, true);
                        GetterUnitsC.AddTimer(curUnitType, Time.deltaTime);

                        if (GetterUnitsC.GetTimer(curUnitType) >= NEEDED_TIME)
                        {
                            GetterUnitsViewUIC.SetActiveCreateButton(curUnitType, false);
                            GetterUnitsC.ActiveNeedCreateButton(curUnitType, false);
                            GetterUnitsC.ResetCurTimer(curUnitType);
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