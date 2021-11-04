using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class GetterUnitsUISystem : IEcsRunSystem
    {
        private const float NEEDED_TIME = 1;

        public void Run()
        {
            for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
            {
                if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Rook || curUnitType == UnitTypes.Bishop)
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


            GetterUnitsViewUIC.SetTextToAmountUnits(UnitTypes.Pawn, InvUnitsC.AmountUnitsInInv(WhoseMoveC.CurPlayerI, UnitTypes.Pawn).ToString());
            GetterUnitsViewUIC.SetTextToAmountUnits(UnitTypes.Rook, InvUnitsC.AmountUnitsInInv(WhoseMoveC.CurPlayerI, UnitTypes.Rook).ToString());
            GetterUnitsViewUIC.SetTextToAmountUnits(UnitTypes.Bishop, InvUnitsC.AmountUnitsInInv(WhoseMoveC.CurPlayerI, UnitTypes.Bishop).ToString());
        }
    }
}