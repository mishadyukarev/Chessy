using Assets.Scripts;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class ReadyZoneUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        if (_eGM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient))
        {
            _eGM.ReadyEnt_ButtonCom.SetColor(Color.red);
        }
        else
        {
            _eGM.ReadyEnt_ButtonCom.SetColor(Color.white);
        }

        if (_eGM.ReadyEnt_StartedGameCom.IsStartedGame || Instance.GameType == GameTypes.WithBot)
        {
            _eGM.ReadyEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGM.ReadyEnt_ParentCom.SetActive(true);
        }
    }
}