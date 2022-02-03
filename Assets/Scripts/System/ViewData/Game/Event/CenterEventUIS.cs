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
    sealed class CenterEventUIS : SystemViewAbstract
    {
        internal CenterEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            Ready<ButtonUIC>().AddListener(Ready);
            JoinDiscord<ButtonUIC>().AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            CenterKingUIE.Button.AddListener(delegate { GetKing(); });
            Leave<ButtonUIC>().AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            CenterFriendUIE.ButtonC.AddListener(FriendReady);
            Hint<ButtonUIC>().AddListener(Hint);

            Units(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            Units(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            Units(UnitTypes.Archer).AddListener(delegate { UpgradeUnit(UnitTypes.Archer); });
            Units(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            Builds(BuildingTypes.Farm).AddListener(delegate { UpgradeBuild(BuildingTypes.Farm); });
            Builds(BuildingTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildingTypes.Woodcutter); });
            Builds(BuildingTypes.Mine).AddListener(delegate { UpgradeBuild(BuildingTypes.Mine); });

            Water.AddListener(UpgradeWater);


            CenterUIEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            CenterUIEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { GetHero(UnitTypes.Snowy); });
            CenterUIEs.HeroE(UnitTypes.Undead).ButtonC.AddListener(delegate { GetHero(UnitTypes.Undead); });
            CenterUIEs.HeroE(UnitTypes.Hell).ButtonC.AddListener(delegate { GetHero(UnitTypes.Hell); });
            //VEs.UIEs.CenterEs.CenterHeroUIE(UnitTypes.Elfemale).AddListener(OpenShop);
        }

        void Ready() => Es.Rpc.ReadyToMaster();
        void FriendReady()
        {
            Es.FriendZoneE.IsActiveC.IsActive = false;
        }
        void GetKing()
        {
            Es.SelectedIdxE.IdxC.Reset();


            if (Es.WhoseMove.IsMyMove)
            {
                if (Es.InventorUnitsEs.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMove.CurPlayerI).HaveUnits)
                {
                    Es.SelectedUnitE.SetSelectedUnit(UnitTypes.King, LevelTypes.First, Es.ClickerObject);
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
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.PickUpgUnitToMas(unit);

                VEs.UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.PickUpgBuildToMas(build);

                VEs.UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.UpgWater();

                VEs.UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.GetHeroToMaster(unit);
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}