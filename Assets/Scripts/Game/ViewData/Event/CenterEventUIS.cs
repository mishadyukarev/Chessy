using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterEventUIS : SystemUIAbstract
    {
        internal CenterEventUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            UIE.CenterEs.ReadyButtonC.AddListener(Ready);
            UIE.CenterEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            UIE.CenterEs.KingE.Button.AddListener(delegate { GetKing(); });

            UIE.CenterEs.FriendE.ButtonC.AddListener(FriendReady);
            //UIEs.CenterEs.AddListener(Hint);

            UIE.CenterEs.UpgradeE.ButtonC(ButtonTypes.First).AddListener(delegate { PickFraction(ButtonTypes.First); });
            UIE.CenterEs.UpgradeE.ButtonC(ButtonTypes.Second).AddListener(delegate { PickFraction(ButtonTypes.Second); });
            UIE.CenterEs.UpgradeE.ButtonC(ButtonTypes.Third).AddListener(delegate { PickFraction(ButtonTypes.Third); });


            //UIEs.CenterEs.Units(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            //Units(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            //Units(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            //Builds(BuildingTypes.Farm).AddListener(delegate { UpgradeBuild(BuildingTypes.Farm); });
            //Builds(BuildingTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildingTypes.Woodcutter); });

            //Water.AddListener(UpgradeWater);


            UIE.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            UIE.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { GetHero(UnitTypes.Snowy); });
            UIE.CenterEs.HeroE(UnitTypes.Undead).ButtonC.AddListener(delegate { GetHero(UnitTypes.Undead); });
            UIE.CenterEs.HeroE(UnitTypes.Hell).ButtonC.AddListener(delegate { GetHero(UnitTypes.Hell); });
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

                UIE.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
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

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}