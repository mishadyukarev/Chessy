using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterEventUIS : SystemAbstract
    {
        readonly CenterUIEs _centerUIEs;

        internal CenterEventUIS(in CenterUIEs centerUIEs, in EntitiesModel ents) : base(ents)
        {
            _centerUIEs = centerUIEs;

            centerUIEs.ReadyButtonC.AddListener(Ready);
            centerUIEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            centerUIEs.KingE.Button.AddListener(delegate { GetKing(); });

            centerUIEs.FriendE.ButtonC.AddListener(FriendReady);
            //UIEs.CenterEs.AddListener(Hint);

            //centerUIEs.UpgradeE.ButtonC(ButtonTypes.First).AddListener(delegate { PickFraction(FractionTypes.Economy); });
            //centerUIEs.UpgradeE.ButtonC(ButtonTypes.Second).AddListener(delegate { PickFraction(FractionTypes.Attack); });
            //centerUIEs.UpgradeE.ButtonC(ButtonTypes.Third).AddListener(delegate { PickFraction(FractionTypes.Heros); });


            centerUIEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            centerUIEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { GetHero(UnitTypes.Snowy); });
            centerUIEs.HeroE(UnitTypes.Undead).ButtonC.AddListener(delegate { GetHero(UnitTypes.Undead); });
            centerUIEs.HeroE(UnitTypes.Hell).ButtonC.AddListener(delegate { GetHero(UnitTypes.Hell); });
            //UIEs.CenterEs.CenterHeroUIE(UnitTypes.Elfemale).AddListener(OpenShop);


        }

        void Ready() => E.RpcPoolEs.ReadyToMaster();
        void FriendReady()
        {
            E.FriendIsActive = false;
        }
        void GetKing()
        {
            E.SelectedIdxC.Idx = 0;

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.Sound(ClipTypes.Click).Invoke();

                if (E.PlayerInfoE(E.CurPlayerITC.Player).HaveKingInInventor)
                {
                    E.SelectedUnitE.Set(UnitTypes.King, LevelTypes.First);
                    E.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }
        void Hint()
        {
            //HintC.CurStartNumber++;

            //if (HintC.CurStartNumber <= (int)VideoClipTypes.Start5)
            //{
            //    HintC.SetWasActived((VideoClipTypes)HintC.CurStartNumber, true);
            //    //EntityCenterHintUIPool.SetVideoClip((VideoClipTypes)HintC.CurStartNumber);
            //}
            //else
            //{
            //    //EntityCenterHintUIPool.SetActiveHintZone(false);
            //}
        }

        //void PickFraction(in FractionTypes fractionT)
        //{
        //    if (E.CurPlayerITC.Is(E.WhoseMove.Player))
        //    {
        //        E.RpcPoolEs.PickFractionToMaster(fractionT);

        //        _centerUIEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
        //    }
        //    else E.Sound(ClipTypes.Mistake).Action.Invoke();
        //}

        void GetHero(in UnitTypes unit)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.Sound(ClipTypes.Click).Invoke();

                E.RpcPoolEs.GetHeroToMaster(unit);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();

            //E.NeedUpdateUI = true;
        }

        void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}