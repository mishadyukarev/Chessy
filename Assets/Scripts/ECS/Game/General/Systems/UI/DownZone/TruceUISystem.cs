﻿using Assets.Scripts;

internal sealed class TruceUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        //if (_eGM.TruceEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient)) _eGM.TruceEnt_ButtonCom.SetColor(Color.red);
        //else _eGM.TruceEnt_ButtonCom.SetColor(Color.white);
    }
}