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

            centerUIEs.UpgradeE.ButtonC(ButtonTypes.First).AddListener(delegate { PickFraction(ButtonTypes.First); });
            centerUIEs.UpgradeE.ButtonC(ButtonTypes.Second).AddListener(delegate { PickFraction(ButtonTypes.Second); });
            centerUIEs.UpgradeE.ButtonC(ButtonTypes.Third).AddListener(delegate { PickFraction(ButtonTypes.Third); });


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
                if (E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
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

        void PickFraction(in ButtonTypes buttonT)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.PickFractionToMaster(buttonT);

                _centerUIEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void GetHero(in UnitTypes unit)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.GetHeroToMaster(unit);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}