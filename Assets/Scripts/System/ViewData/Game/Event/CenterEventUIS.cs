using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityVPool;
using static Game.Game.EconomyUpUIE;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.CenterHeroUIE;
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


            Unit<ButtonUIC>(UnitTypes.Elfemale).AddListener(Elf);
            Unit<ButtonUIC>(UnitTypes.None).AddListener(OpenShop);
        }

        void Ready() => EntityPool.Rpc<RpcC>().ReadyToMaster();
        void FriendReady()
        {
            EntityPool.FriendZone<IsActiveC>().IsActive = false;
        }
        void GetKing()
        {
            EntityPool.SelIdx<IdxC>().Reset();


            if (WhoseMoveE.IsMyMove)
            {
                if (InventorUnitsE.Units<AmountC>(UnitTypes.King, LevelTypes.First, WhoseMoveE.CurPlayerI).Have)
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
                EntityPool.Rpc<RpcC>().PickUpgUnitToMas(unit);

                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgBuildToMas(build);

                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UpgradeWater()
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().UpgWater();

                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void Elf()
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().GetHero(UnitTypes.Elfemale);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}