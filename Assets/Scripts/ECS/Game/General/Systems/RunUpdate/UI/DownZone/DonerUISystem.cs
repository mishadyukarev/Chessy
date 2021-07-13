using Assets.Scripts;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class DonerUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        if (_eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient)) _eGM.DonerUIEnt_ButtonCom.SetColor(Color.red);
        else _eGM.DonerUIEnt_ButtonCom.SetColor(Color.white);
    }
}
