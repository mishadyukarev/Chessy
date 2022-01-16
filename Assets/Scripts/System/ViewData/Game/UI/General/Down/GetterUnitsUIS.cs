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
                    if (GetterUnitsE.GetterUnit<IsActiveC>(unitT_cur).IsActive)
                    {
                        PawnArcherDownUIE.Create<ButtonUIC>(unitT_cur).SetActive(true);

                        GetterUnitsE.GetterUnit<TimerC>(unitT_cur).Timer += Time.deltaTime;

                        if (GetterUnitsE.GetterUnit<TimerC>(unitT_cur).Timer >= NEEDED_TIME)
                        {
                            PawnArcherDownUIE.Create<ButtonUIC>(unitT_cur).SetActive(false);
                            GetterUnitsE.GetterUnit<IsActiveC>(unitT_cur).IsActive = false;
                            GetterUnitsE.GetterUnit<TimerC>(unitT_cur).Reset();
                        }
                    }

                    else
                    {
                        PawnArcherDownUIE.Create<ButtonUIC>(unitT_cur).SetActive(false);
                    }
                }
            }

            var amountPawns = EntInventorUnits.Units<AmountC>(UnitTypes.Pawn, LevelTypes.First, WhoseMoveE.CurPlayerI).Amount
                + EntInventorUnits.Units<AmountC>(UnitTypes.Pawn, LevelTypes.Second, WhoseMoveE.CurPlayerI).Amount;

            var amountArchers = EntInventorUnits.Units<AmountC>(UnitTypes.Archer, LevelTypes.First, WhoseMoveE.CurPlayerI).Amount
                + EntInventorUnits.Units<AmountC>(UnitTypes.Archer, LevelTypes.Second, WhoseMoveE.CurPlayerI).Amount;

            PawnArcherDownUIE.Taker<TextMPUGUIC>(UnitTypes.Pawn).Text = amountPawns.ToString();
            PawnArcherDownUIE.Taker<TextMPUGUIC>(UnitTypes.Archer).Text = amountArchers.ToString();
        }
    }
}