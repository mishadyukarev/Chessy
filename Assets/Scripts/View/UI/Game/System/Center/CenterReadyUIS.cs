using Chessy.Game.Model.Entity;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterReadyUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal CenterReadyUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var readyBut = eUI.CenterEs.ReadyButtonC;

            readyBut.Image.color = _e.PlayerInfoE(_e.CurPlayerIT).IsReadyForStartOnlineGame ? Color.red : Color.white;

            if (_e.IsStartedGame || PhotonNetwork.OfflineMode)
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