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
                    if (GetterUnitsEs.GetterUnit<IsActiveC>(unitT_cur).IsActive)
                    {
                        PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(true);

                        GetterUnitsEs.GetterUnit<TimerC>(unitT_cur).Timer += Time.deltaTime;

                        if (GetterUnitsEs.GetterUnit<TimerC>(unitT_cur).Timer >= NEEDED_TIME)
                        {
                            PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(false);
                            GetterUnitsEs.GetterUnit<IsActiveC>(unitT_cur).IsActive = false;
                            GetterUnitsEs.GetterUnit<TimerC>(unitT_cur).Reset();
                        }
                    }

                    else
                    {
                        PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(false);
                    }
                }
            }

            var amountPawns = InventorUnitsE.Units(UnitTypes.Pawn, LevelTypes.First, Entities.WhoseMove.CurPlayerI).Amount
                + InventorUnitsE.Units(UnitTypes.Pawn, LevelTypes.Second, Entities.WhoseMove.CurPlayerI).Amount;

            var amountArchers = InventorUnitsE.Units(UnitTypes.Archer, LevelTypes.First, Entities.WhoseMove.CurPlayerI).Amount
                + InventorUnitsE.Units(UnitTypes.Archer, LevelTypes.Second, Entities.WhoseMove.CurPlayerI).Amount;

            PawnArcherDownUIE.Taker<TextUIC>(UnitTypes.Pawn).Text = amountPawns.ToString();
            PawnArcherDownUIE.Taker<TextUIC>(UnitTypes.Archer).Text = amountArchers.ToString();
        }
    }
}