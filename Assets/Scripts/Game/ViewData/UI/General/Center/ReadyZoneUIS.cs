using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class ReadyZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ReadyZoneUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            ref var readyBut = ref Ready<ButtonUIC>();

            readyBut.Color = E.PlayerE(E.CurPlayerITC.Player).IsReadyC ? Color.red : Color.white;

            if (E.IsStartedGame || PhotonNetwork.OfflineMode)
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