using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterReadyUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterReadyUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var readyBut = UIEs.CenterEs.ReadyButtonC;

            readyBut.Image.color = E.PlayerE(E.CurPlayerITC.Player).IsReadyC ? Color.red : Color.white;

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