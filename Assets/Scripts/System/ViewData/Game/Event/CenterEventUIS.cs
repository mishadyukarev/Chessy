using Game.Common;
using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class CenterEventUIS
    {
        internal CenterEventUIS()
        {
            EntityUIPool.ReadyCenter<ButtonC>().AddList(Ready);
            EntityUIPool.JoinDiscordCenter<ButtonC>().AddList(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            EntityUIPool.LeaveUp<ButtonC>().AddList(delegate { PhotonNetwork.LeaveRoom(); });
            FriendZoneUIC.AddListenerReady(FriendReady);
            HintViewUIC.AddListHint_But(Hint);

            PickUpgUIC.AddList(UnitTypes.King, delegate { UpgradeUnit(UnitTypes.King); });
            PickUpgUIC.AddList(UnitTypes.Pawn, delegate { UpgradeUnit(UnitTypes.Pawn); });
            PickUpgUIC.AddList(UnitTypes.Archer, delegate { UpgradeUnit(UnitTypes.Archer); });
            PickUpgUIC.AddList(UnitTypes.Scout, delegate { UpgradeUnit(UnitTypes.Scout); });

            PickUpgUIC.AddList(BuildTypes.Farm, delegate { UpgradeBuild(BuildTypes.Farm); });
            PickUpgUIC.AddList(BuildTypes.Woodcutter, delegate { UpgradeBuild(BuildTypes.Woodcutter); });
            PickUpgUIC.AddList(BuildTypes.Mine, delegate { UpgradeBuild(BuildTypes.Mine); });

            PickUpgUIC.AddListWater(UpgradeWater);


            HeroesViewUIC.AddListElf(Elf);
            HeroesViewUIC.AddListPremium(OpenShop);
        }

        private void Ready() => EntityPool.Rpc<RpcC>().ReadyToMaster();
        private void FriendReady()
        {
            FriendC.IsActiveFriendZone = false;
        }
        private void Hint()
        {
            HintC.CurStartNumber++;

            if (HintC.CurStartNumber <= (int)VideoClipTypes.Start5)
            {
                HintC.SetWasActived((VideoClipTypes)HintC.CurStartNumber, true);
                HintViewUIC.SetVideoClip((VideoClipTypes)HintC.CurStartNumber);
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }

        private void UpgradeUnit(UnitTypes unit)
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgUnitToMas(unit);

                HeroesViewUIC.SetActiveZone(true);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void UpgradeBuild(BuildTypes build)
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().PickUpgBuildToMas(build);

                HeroesViewUIC.SetActiveZone(true);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void UpgradeWater()
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().UpgWater();

                HeroesViewUIC.SetActiveZone(true);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void Elf()
        {
            if (WhoseMoveC.IsMyMove)
            {
                EntityPool.Rpc<RpcC>().GetHero(UnitTypes.Elfemale);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}