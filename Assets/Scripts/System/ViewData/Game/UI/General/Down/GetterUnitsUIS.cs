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

            //if (GetterUnitsEs.IsActiveC.IsActive)
            //{
            //    PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(true);

            //    GetterUnitsEs.TimerC>(unitT_cur).Timer += Time.deltaTime;

            //    if (GetterUnitsEs.TimerC.Timer >= NEEDED_TIME)
            //    {
            //        PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(false);
            //        GetterUnitsEs.IsActiveC.IsActive = false;
            //        GetterUnitsEs.TimerC.Reset();
            //    }
            //}

            //else
            //{
            //    PawnArcherDownUIE.BuyUnit<ButtonUIC>(unitT_cur).SetActive(false);
            //}

            var curPlayerI = Es.WhoseMoveE.CurPlayerI;

            var amountPawns = Es.WhereWorker.AmountPaws(curPlayerI);

            DownPawnUIE.TextUIC.Text = amountPawns.ToString() + "/" + Es.MaxAvailablePawnsE(curPlayerI).MaxPawns;
        }
    }
}