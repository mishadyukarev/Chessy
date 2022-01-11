using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityVPool;
using static Game.Game.EntityUpUIPool;
using static Game.Game.EntityCenterUIPool;
using static Game.Game.EntityCenterHeroUIPool;
using static Game.Game.EntityCenterFriendUIPool;

using static Game.Game.EntityCenterPickUpgUIPool;
using static Game.Game.EntityCenterHintUIPool;

namespace Game.Game
{
    sealed class CenterEventUIS
    {
        internal CenterEventUIS()
        {
            Ready<ButtonVC>().AddList(Ready);
            JoinDiscord<ButtonVC>().AddList(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            Leave<ButtonVC>().AddList(delegate { PhotonNetwork.LeaveRoom(); });
            Friend<ButtonVC>().AddList(FriendReady);
            Hint<ButtonVC>().AddList(Hint);

            Units<ButtonVC>(UnitTypes.King).AddList(delegate { UpgradeUnit(UnitTypes.King); });
            Units<ButtonVC>(UnitTypes.Pawn).AddList(delegate { UpgradeUnit(UnitTypes.Pawn); });
            Units<ButtonVC>(UnitTypes.Archer).AddList(delegate { UpgradeUnit(UnitTypes.Archer); });
            Units<ButtonVC>(UnitTypes.Scout).AddList(delegate { UpgradeUnit(UnitTypes.Scout); });

            Builds<ButtonVC>(BuildTypes.Farm).AddList(delegate { UpgradeBuild(BuildTypes.Farm); });
            Builds<ButtonVC>(BuildTypes.Woodcutter).AddList(delegate { UpgradeBuild(BuildTypes.Woodcutter); });
            Builds<ButtonVC>(BuildTypes.Mine).AddList(delegate { UpgradeBuild(BuildTypes.Mine); });

            Water<ButtonVC>().AddList(UpgradeWater);


            Unit<ButtonVC>(UnitTypes.Elfemale).AddList(Elf);
            Unit<ButtonVC>(UnitTypes.None).AddList(OpenShop);
        }

        void Ready() => EntityPool.Rpc<RpcC>().ReadyToMaster();
        void FriendReady()
        {
            FriendC.IsActiveFriendZone = false;
        }
        void Hint()
        {
            HintC.CurStartNumber++;

            if (HintC.CurStartNumber <= (int)VideoClipTypes.Start5)
            {
                HintC.SetWasActived((VideoClipTypes)HintC.CurStartNumber, true);
                //EntityCenterHintUIPool.SetVideoClip((VideoClipTypes)HintC.CurStartNumber);
            }
            else
            {
                //EntityCenterHintUIPool.SetActiveHintZone(false);
            }
        }

        void UpgradeUnit(UnitTypes unit)
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgUnitToMas(unit);

                Unit<ButtonVC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void UpgradeBuild(BuildTypes build)
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgBuildToMas(build);

                Unit<ButtonVC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void UpgradeWater()
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().UpgWater();

                Unit<ButtonVC>(UnitTypes.Elfemale).SetActiveParent(true);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void Elf()
        {
            if (WhoseMoveC.IsMyMove)
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