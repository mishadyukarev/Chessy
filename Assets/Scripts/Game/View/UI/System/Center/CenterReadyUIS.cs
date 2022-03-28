using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterReadyUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterReadyUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var readyBut = eUI.CenterEs.ReadyButtonC;

            readyBut.Image.color = e.PlayerInfoE(e.CurPlayerITC.Player).IsReadyC ? Color.red : Color.white;

            if (e.IsStartedGame || PhotonNetwork.OfflineMode)
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