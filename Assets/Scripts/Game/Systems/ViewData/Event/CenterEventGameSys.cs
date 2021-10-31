using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
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
                HintViewUIC.SetPos(new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f)));
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }
    }
}