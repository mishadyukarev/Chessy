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
        internal CenterEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
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

        void Ready() => E.RpcPoolEs.ReadyToMaster();
        void FriendReady()
        {
            E.FriendIsActive = false;
        }
        void GetKing()
        {
            E.SelectedIdxC.Idx = 0;


            if (E.IsMyMove)
            {
                if (E.UnitInfo(E.CurPlayerI.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
                {
                    E.SelectedUnitE.Set(UnitTypes.King, LevelTypes.First);
                    E.CellClickTC.Click = CellClickTypes.SetUnit;
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
            if (E.IsMyMove)
            {
                E.RpcPoolEs.PickUpgUnitToMas(unit);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (E.IsMyMove)
            {
                E.RpcPoolEs.PickUpgBuildToMas(build);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (E.IsMyMove)
            {
                E.RpcPoolEs.UpgWater();

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (E.IsMyMove)
            {
                E.RpcPoolEs.GetHeroToMaster(unit);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}