using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityVPool;
using static Game.Game.EconomyUpUIE;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.CenterHerosUIE;
using static Game.Game.CenterFriendUIE;

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

            CenterKingUIE.Button<ButtonUIC>().AddListener(delegate { GetKing(); });
            Leave<ButtonUIC>().AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            Friend<ButtonUIC>().AddListener(FriendReady);
            Hint<ButtonUIC>().AddListener(Hint);

            Units<ButtonUIC>(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            Units<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            Units<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { UpgradeUnit(UnitTypes.Archer); });
            Units<ButtonUIC>(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            Builds<ButtonUIC>(BuildingTypes.Farm).AddListener(delegate { UpgradeBuild(BuildingTypes.Farm); });
            Builds<ButtonUIC>(BuildingTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildingTypes.Woodcutter); });
            Builds<ButtonUIC>(BuildingTypes.Mine).AddListener(delegate { UpgradeBuild(BuildingTypes.Mine); });

            Water<ButtonUIC>().AddListener(UpgradeWater);


            ButtonC(UnitTypes.Elfemale).AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            ButtonC(UnitTypes.Snowy).AddListener(delegate { GetHero(UnitTypes.Snowy); });
            ButtonC(UnitTypes.None).AddListener(OpenShop);
        }

        void Ready() => EntityPool.Rpc.ReadyToMaster();
        void FriendReady()
        {
            EntityPool.FriendZone<IsActiveC>().IsActive = false;
        }
        void GetKing()
        {
            SelectedIdxE.IdxC.Reset();


            if (WhoseMoveE.IsMyMove)
            {
                if (InventorUnitsE.Units(UnitTypes.King, LevelTypes.First, WhoseMoveE.CurPlayerI).Have)
                {
                    EntityPool.ClickerObject<CellClickC>().Click = CellClickTypes.SetUnit;

                    SelectedUnitE.SelUnit<UnitTC>().Unit = UnitTypes.King;
                    SelectedUnitE.SelUnit<LevelTC>().Level = LevelTypes.First;
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
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc.PickUpgUnitToMas(unit);

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc.PickUpgBuildToMas(build);

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc.UpgWater();

                Parent.SetActive(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetHero(in UnitTypes unit)
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc.GetHero(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}