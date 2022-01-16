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

            Leave<ButtonUIC>().AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            Friend<ButtonUIC>().AddListener(FriendReady);
            Hint<ButtonUIC>().AddListener(Hint);

            Units<ButtonUIC>(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            Units<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            Units<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { UpgradeUnit(UnitTypes.Archer); });
            Units<ButtonUIC>(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            Builds<ButtonUIC>(BuildTypes.Farm).AddListener(delegate { UpgradeBuild(BuildTypes.Farm); });
            Builds<ButtonUIC>(BuildTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildTypes.Woodcutter); });
            Builds<ButtonUIC>(BuildTypes.Mine).AddListener(delegate { UpgradeBuild(BuildTypes.Mine); });

            Water<ButtonUIC>().AddListener(UpgradeWater);


            Unit<ButtonUIC>(UnitTypes.Elfemale).AddListener(Elf);
            Unit<ButtonUIC>(UnitTypes.None).AddListener(OpenShop);
        }

        void Ready() => EntityPool.Rpc<RpcC>().ReadyToMaster();
        void FriendReady()
        {
            EntityPool.FriendZone<IsActiveC>().IsActive = false;
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

        private void UpgradeBuild(BuildTypes build)
        {
            if (WhoseMoveE.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgBuildToMas(build);

                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void UpgradeWater()
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