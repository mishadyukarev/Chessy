using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.CenterHintUIE;
using static Game.Game.CenterUpgradeUIE;
using static Game.Game.EconomyUpUIE;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class CenterEventUIS : SystemUIAbstract
    {
        internal CenterEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            Ready<ButtonUIC>().AddListener(Ready);
            JoinDiscord<ButtonUIC>().AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            CenterKingUIE.Button.AddListener(delegate { GetKing(); });
            Leave<ButtonUIC>().AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            CenterFriendUIE.ButtonC.AddListener(FriendReady);
            Hint<ButtonUIC>().AddListener(Hint);

            Units(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            Units(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            Units(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            Builds(BuildingTypes.Farm).AddListener(delegate { UpgradeBuild(BuildingTypes.Farm); });
            Builds(BuildingTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildingTypes.Woodcutter); });

            Water.AddListener(UpgradeWater);


            UIEs.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            UIEs.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { GetHero(UnitTypes.Snowy); });
            UIEs.CenterEs.HeroE(UnitTypes.Undead).ButtonC.AddListener(delegate { GetHero(UnitTypes.Undead); });
            UIEs.CenterEs.HeroE(UnitTypes.Hell).ButtonC.AddListener(delegate { GetHero(UnitTypes.Hell); });
            //UIEs.CenterEs.CenterHeroUIE(UnitTypes.Elfemale).AddListener(OpenShop);
        }

        void Ready() => Es.RpcE.ReadyToMaster();
        void FriendReady()
        {
            Es.FriendIsActiveC.IsActive = false;
        }
        void GetKing()
        {
            Es.SelectedIdxC.Reset();


            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                if (Es.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMovePlayerTC.CurPlayerI).HaveUnits)
                {
                    Es.SelUnitTC.Unit = UnitTypes.King;
                    Es.SelUnitLevelTC.Level = LevelTypes.First;
                    Es.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
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

        void UpgradeUnit(UnitTypes unit)
        {
            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                Es.RpcE.PickUpgUnitToMas(unit);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                Es.RpcE.PickUpgBuildToMas(build);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                Es.RpcE.UpgWater();

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                Es.RpcE.GetHeroToMaster(unit);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}