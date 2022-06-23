using Chessy.Model.Model.Entity;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Model
{
    sealed class CenterReadyUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal CenterReadyUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
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