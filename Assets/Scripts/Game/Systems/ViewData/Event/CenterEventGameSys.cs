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
            HintDataUIC.CurNumber++;

            if (HintDataUIC.CurNumber < System.Enum.GetNames(typeof(VideoClipTypes)).Length)
            {
                HintViewUIC.SetVideoClip((VideoClipTypes)HintDataUIC.CurNumber);
                HintViewUIC.SetPos(new Vector3(UnityEngine.Random.Range(-500f, 500f), UnityEngine.Random.Range(-300f, 300f)));
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }
        private void ExecuteUpg_But(PickUpgradeTypes upgBut)
        {
            PickUpgZoneDataUIC.SetActiveParent(WhoseMoveC.CurPlayer, false);
            PickUpgZoneDataUIC.SetActive(WhoseMoveC.CurPlayer, upgBut, false);

            //switch (upgBut)
            //{
            //    case PickUpgradeTypes.None: throw new Exception();
            //    case PickUpgradeTypes.King:
                    
            //        break;

            //    case PickUpgradeTypes.Pawn:
            //        break;

            //    case PickUpgradeTypes.Archer:
            //        break;

            //    case PickUpgradeTypes.Scout:
            //        break;

            //    case PickUpgradeTypes.Water:
            //        break;

            //    case PickUpgradeTypes.Farm:
            //        RpcSys.UpgradeBuildingToMaster(BuildTypes.Farm);
            //        break;

            //    case PickUpgradeTypes.Woodcutter:
            //        RpcSys.UpgradeBuildingToMaster(BuildTypes.Woodcutter);
            //        break;

            //    case PickUpgradeTypes.Mine:
            //        RpcSys.UpgradeBuildingToMaster(BuildTypes.Mine);
            //        break;

            //    default: throw new Exception();
            //}
            RpcSys.PickUpgradeToMaster(upgBut);
        }
    }
}