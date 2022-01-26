using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityVPool;
using static Game.Game.EconomyUpUIE;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.CenterHerosUIE;
using static Game.Game.CenterUpgradeUIE;
using static Game.Game.CenterHintUIE;

namespace Game.Game
{
    sealed class CenterEventUIS
    {
        internal CenterEventUIS()
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

        void Ready() => Entities.Rpc.ReadyToMaster();
        void FriendReady()
        {
            Entities.FriendZoneE.IsActiveC.IsActive = false;
        }
        void GetKing()
        {
            Entities.SelectedIdxE.IdxC.Reset();


            if (Entities.WhoseMoveE.IsMyMove)
            {
                if (InventorUnitsE.Units(UnitTypes.King, LevelTypes.First, Entities.WhoseMoveE.CurPlayerI).Have)
                {
                    Entities.ClickerObject.CellClickC.Click = CellClickTypes.SetUnit;

                    Entities.SelectedUnitE.UnitTC.Unit = UnitTypes.King;
                    Entities.SelectedUnitE.LevelTC.Level = LevelTypes.First;
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
            if (Entities.WhoseMoveE.IsMyMove)
            {
                Entities.Rpc.PickUpgUnitToMas(unit);

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (Entities.WhoseMoveE.IsMyMove)
            {
                Entities.Rpc.PickUpgBuildToMas(build);

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (Entities.WhoseMoveE.IsMyMove)
            {
                Entities.Rpc.UpgWater();

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (Entities.WhoseMoveE.IsMyMove)
            {
                Entities.Rpc.GetHeroToMaster(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}