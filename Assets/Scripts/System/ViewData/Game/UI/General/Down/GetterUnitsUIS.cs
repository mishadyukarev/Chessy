using UnityEngine;

namespace Game.Game
{
    sealed class GetterUnitsUIS : SystemViewAbstract, IEcsRunSystem
    {
        const float NEEDED_TIME = 1;

        public GetterUnitsUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (UnitTypes unitT_cur = UnitTypes.None; unitT_cur < UnitTypes.End; unitT_cur++)
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

            var amountPawns = Es.InventorUnitsEs.Units(UnitTypes.Pawn, LevelTypes.First, Es.WhoseMove.CurPlayerI).Units.Amount
                + Es.InventorUnitsEs.Units(UnitTypes.Pawn, LevelTypes.Second, Es.WhoseMove.CurPlayerI).Units.Amount;

            var amountArchers = Es.InventorUnitsEs.Units(UnitTypes.Archer, LevelTypes.First, Es.WhoseMove.CurPlayerI).Units.Amount
                + Es.InventorUnitsEs.Units(UnitTypes.Archer, LevelTypes.Second, Es.WhoseMove.CurPlayerI).Units.Amount;

            PawnArcherDownUIE.Taker<TextUIC>(UnitTypes.Pawn).Text = amountPawns.ToString();
            PawnArcherDownUIE.Taker<TextUIC>(UnitTypes.Archer).Text = amountArchers.ToString();
        }
    }
}