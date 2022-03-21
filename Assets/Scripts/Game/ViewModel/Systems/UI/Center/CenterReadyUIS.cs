using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterReadyUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterReadyUIS( in EntitiesViewUI entsUI, in Chessy.Game.Entity.Model.EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var readyBut = UIE.CenterEs.ReadyButtonC;

            readyBut.Image.color = E.PlayerInfoE(E.CurPlayerITC.Player).IsReadyC ? Color.red : Color.white;

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