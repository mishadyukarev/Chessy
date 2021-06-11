using UnityEngine;
using static Main;

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

        if (_eGM.ReadyEnt_ActivatedDictCom.IsActivatedAll || Instance.TestType == TestTypes.Standart)
        {
            _eGM.ReadyEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGM.ReadyEnt_ParentCom.SetActive(true);
        }
    }
}