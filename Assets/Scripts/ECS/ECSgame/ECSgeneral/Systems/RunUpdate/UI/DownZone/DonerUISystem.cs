﻿using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class DonerUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        if (_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient)) _eGM.DonerEnt_ButtonCom.SetColor(Color.red);
        else _eGM.DonerEnt_ButtonCom.SetColor(Color.white);
    }
}
