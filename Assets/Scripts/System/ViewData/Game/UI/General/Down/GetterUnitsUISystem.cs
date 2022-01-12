using UnityEngine;

namespace Game.Game
{
    struct GetterUnitsUISystem : IEcsRunSystem
    {
        const float NEEDED_TIME = 1;

        public void Run()
        {
            for (UnitTypes curUnitType = UnitTypes.Start; curUnitType < UnitTypes.End; curUnitType++)
            {
                if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Archer)
                {
                    if (GetterUnitsC.IsActivatedButton(curUnitType))
                    {
                        UIEntDownPawnArcher.Create<ButtonUIC>(curUnitType).SetActive(true);
                        GetterUnitsC.AddTimer(curUnitType, Time.deltaTime);

                        if (GetterUnitsC.GetTimer(curUnitType) >= NEEDED_TIME)
                        {
                            UIEntDownPawnArcher.Create<ButtonUIC>(curUnitType).SetActive(false);
                            GetterUnitsC.ActiveNeedCreateButton(curUnitType, false);
                            GetterUnitsC.ResetCurTimer(curUnitType);
                        }
                    }

                    else
                    {
                        UIEntDownPawnArcher.Create<ButtonUIC>(curUnitType).SetActive(false);
                    }
                }
            }


            UIEntDownPawnArcher.Taker<TextUIC>(UnitTypes.Pawn).Text = InvUnitsC.AmountUnits(UnitTypes.Pawn, WhoseMoveC.CurPlayerI).ToString();
            UIEntDownPawnArcher.Taker<TextUIC>(UnitTypes.Archer).Text = InvUnitsC.AmountUnits(UnitTypes.Archer, WhoseMoveC.CurPlayerI).ToString();
        }
    }
}