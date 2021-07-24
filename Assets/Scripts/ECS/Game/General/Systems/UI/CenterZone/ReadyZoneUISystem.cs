using Assets.Scripts;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class ReadyZoneUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        if (_eGGUIM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient))
        {
            _eGGUIM.ReadyEnt_ButtonCom.SetColor(Color.red);
        }
        else
        {
            _eGGUIM.ReadyEnt_ButtonCom.SetColor(Color.white);
        }

        if (_eGGUIM.ReadyEnt_StartedGameCom.IsStartedGame || PhotonNetwork.OfflineMode/*Instance.GameModeType == GameModTypes.WithBot*/)
        {
            _eGGUIM.ReadyEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGGUIM.ReadyEnt_ParentCom.SetActive(true);
        }
    }
}