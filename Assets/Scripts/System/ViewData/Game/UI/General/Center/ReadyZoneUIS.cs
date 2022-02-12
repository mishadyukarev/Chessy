using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class ReadyZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ReadyZoneUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            ref var readyBut = ref Ready<ButtonUIC>();

            readyBut.Color = Es.Ready(Es.WhoseMoveE.CurPlayerI).IsReadyC.IsReady ? Color.red : Color.white;

            if (Es.GameInfoE.IsStartedGameC.IsStartedGame || PhotonNetwork.OfflineMode)
            {
                readyBut.SetActiveParent(false);
            }
            else
            {
                readyBut.SetActiveParent(true);
            }
        }
    }
}