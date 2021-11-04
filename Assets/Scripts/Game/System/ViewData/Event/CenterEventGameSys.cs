using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class CenterEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            ReadyViewUIC.AddListenerToReadyButton(Ready);
            LeaveViewUIC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            FriendZoneViewUIC.AddListenerReady(ReadyFriend);
            HintViewUIC.AddListHint_But(ExecuteHint);

            //for (PickUpgradeTypes upgBut = (PickUpgradeTypes)1; upgBut < (PickUpgradeTypes)*typeof(PickUpgradeTypes).GetEnumNames().Length; upgBut++)
            //{
            //    PickUpgZoneViewUIC.AddList_But(upgBut, delegate { ExecuteUpg_But(upgBut); });
            //}

            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.King, delegate { ExecuteUpg_But(PickUpgradeTypes.King); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Pawn, delegate { ExecuteUpg_But(PickUpgradeTypes.Pawn); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Archer, delegate { ExecuteUpg_But(PickUpgradeTypes.Archer); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Scout, delegate { ExecuteUpg_But(PickUpgradeTypes.Scout); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Water, delegate { ExecuteUpg_But(PickUpgradeTypes.Water); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Farm, delegate { ExecuteUpg_But(PickUpgradeTypes.Farm); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Woodcutter, delegate { ExecuteUpg_But(PickUpgradeTypes.Woodcutter); });
            PickUpgZoneViewUIC.AddList_But(PickUpgradeTypes.Mine, delegate { ExecuteUpg_But(PickUpgradeTypes.Mine); });
        }

        private void Ready() => RpcSys.ReadyToMaster();
        private void ReadyFriend()
        {
            FriendZoneDataUIC.IsActiveFriendZone = false;
        }
        private void ExecuteHint()
        {
            HintDataUIC.CurStartNumber++;

            if (HintDataUIC.CurStartNumber <= (int)VideoClipTypes.Start4)
            {
                HintDataUIC.SetActive((VideoClipTypes)HintDataUIC.CurStartNumber, true);
                HintViewUIC.SetVideoClip((VideoClipTypes)HintDataUIC.CurStartNumber);
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }
        private void ExecuteUpg_But(PickUpgradeTypes upgBut)
        {
            RpcSys.PickUpgradeToMaster(upgBut);
        }
    }
}