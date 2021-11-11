using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class CenterEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            ReadyViewUIC.AddListenerToReadyButton(Ready);
            LeaveViewUIC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            FriendZoneViewUIC.AddListenerReady(FriendReady);
            HintViewUIC.AddListHint_But(Hint);

            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.King, delegate { Upgrade(PickUpgradeTypes.King); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Pawn, delegate { Upgrade(PickUpgradeTypes.Pawn); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Archer, delegate { Upgrade(PickUpgradeTypes.Archer); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Scout, delegate { Upgrade(PickUpgradeTypes.Scout); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Water, delegate { Upgrade(PickUpgradeTypes.Water); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Farm, delegate { Upgrade(PickUpgradeTypes.Farm); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Woodcutter, delegate { Upgrade(PickUpgradeTypes.Woodcutter); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Mine, delegate { Upgrade(PickUpgradeTypes.Mine); });

            HeroesViewUIC.AddListElf(Elf);
            HeroesViewUIC.AddListPremium(OpenShop);
        }

        private void Ready() => RpcSys.ReadyToMaster();
        private void FriendReady()
        {
            FriendZoneDataUIC.IsActiveFriendZone = false;
        }
        private void Hint()
        {
            HintDataUIC.CurStartNumber++;

            if (HintDataUIC.CurStartNumber <= (int)VideoClipTypes.Start5)
            {
                HintDataUIC.SetWasActived((VideoClipTypes)HintDataUIC.CurStartNumber, true);
                HintViewUIC.SetVideoClip((VideoClipTypes)HintDataUIC.CurStartNumber);
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }
        private void Upgrade(PickUpgradeTypes upgBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                RpcSys.PickUpgradeToMaster(upgBut);

                HeroesViewUIC.SetActiveZone(true);
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void Elf()
        {
            if (WhoseMoveC.IsMyMove)
            {
                RpcSys.GetHero(UnitTypes.Elfemale);
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}