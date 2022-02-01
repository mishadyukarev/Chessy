using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.CenterHerosUIE;
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


            ButtonC(UnitTypes.Elfemale).AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            ButtonC(UnitTypes.Snowy).AddListener(delegate { GetHero(UnitTypes.Snowy); });
            ButtonC(UnitTypes.None).AddListener(OpenShop);
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
                    Es.ClickerObject.CellClickC.Click = CellClickTypes.SetUnit;

                    Es.SelectedUnitE.UnitTC.Unit = UnitTypes.King;
                    Es.SelectedUnitE.LevelTC.Level = LevelTypes.First;
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
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

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.PickUpgBuildToMas(build);

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.UpgWater();

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                Es.Rpc.GetHeroToMaster(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}