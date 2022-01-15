using UnityEngine;

namespace Game.Game
{
    struct GetterUnitsUIS : IEcsRunSystem
    {
        const float NEEDED_TIME = 1;

        public void Run()
        {
            for (UnitTypes unitT_cur = UnitTypes.Start; unitT_cur < UnitTypes.End; unitT_cur++)
            {
                if (unitT_cur == UnitTypes.Pawn || unitT_cur == UnitTypes.Archer)
                {
                    if (GetterUnitsC.GetterUnit<IsActiveC>(unitT_cur).IsActive)
                    {
                        UIEntDownPawnArcher.Create<ButtonUIC>(unitT_cur).SetActive(true);

                        GetterUnitsC.GetterUnit<TimerC>(unitT_cur).Timer += Time.deltaTime;

                        if (GetterUnitsC.GetterUnit<TimerC>(unitT_cur).Timer >= NEEDED_TIME)
                        {
                            UIEntDownPawnArcher.Create<ButtonUIC>(unitT_cur).SetActive(false);
                            GetterUnitsC.GetterUnit<IsActiveC>(unitT_cur).IsActive = false;
                            GetterUnitsC.GetterUnit<TimerC>(unitT_cur).Reset();
                        }
                    }

                    else
                    {
                        UIEntDownPawnArcher.Create<ButtonUIC>(unitT_cur).SetActive(false);
                    }
                }
            }


            //UIEntDownPawnArcher.Taker<TextUIC>(UnitTypes.Pawn).Text = EntInventorUnits.Units<AmountC>(UnitTypes.Pawn, WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI).Amount.ToString();
            //UIEntDownPawnArcher.Taker<TextUIC>(UnitTypes.Archer).Text = EntInventorUnits.AmountUnits(UnitTypes.Archer, WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI).ToString();
        }
    }
}