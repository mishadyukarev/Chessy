using Assets.Scripts;
using Assets.Scripts.Workers.Game.UI;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class DonerUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        if (UIDownWorker.IsDoned(Instance.IsMasterClient)) _eGGUIM.DonerUIEnt_ButtonCom.Button.image.color = Color.red;
        else _eGGUIM.DonerUIEnt_ButtonCom.Button.image.color = Color.white;
    }
}
