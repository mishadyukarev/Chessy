using Chessy.Game.Entity.Model;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterReadyUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal CenterReadyUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
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