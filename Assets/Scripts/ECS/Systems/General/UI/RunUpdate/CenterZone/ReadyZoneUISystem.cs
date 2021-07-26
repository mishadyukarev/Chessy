using Assets.Scripts;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class ReadyZoneUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        if (UIMiddleWorker.IsReady(Instance.IsMasterClient))
        {
            _eGGUIM.ReadyEnt_ButtonCom.Button.image.color = Color.red;
        }
        else
        {
            _eGGUIM.ReadyEnt_ButtonCom.Button.image.color = Color.white;
        }

        if (_eGGUIM.ReadyEnt_StartedGameCom.IsStartedGame || PhotonNetwork.OfflineMode/*Instance.GameModeType == GameModTypes.WithBot*/)
        {
            _eGGUIM.ReadyEnt_ParentCom.ParentGO.SetActive(false);
        }
        else
        {
            _eGGUIM.ReadyEnt_ParentCom.ParentGO.SetActive(true);
        }
    }
}