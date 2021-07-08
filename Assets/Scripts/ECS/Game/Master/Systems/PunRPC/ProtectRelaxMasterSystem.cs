using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using System;

internal sealed class ProtectRelaxMasterSystem : SystemMasterReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private ProtectRelaxTypes ProtectRelaxType => _eMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;

    public override void Run()
    {
        base.Run();

        switch (ProtectRelaxType)
        {
            case ProtectRelaxTypes.None:
                throw new Exception();

            case ProtectRelaxTypes.Protected:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCell).IsProtected)
                {
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).ResetProtectedRelaxedType();
                }
                else
                {
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).SetProtectedRelaxedType(ProtectRelaxType);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                }
                break;

            case ProtectRelaxTypes.Relaxed:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCell).IsRelaxed)
                {
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).ResetProtectedRelaxedType();
                }
                else
                {
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).SetProtectedRelaxedType(ProtectRelaxType);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                }
                break;

            default:
                throw new Exception();
        }



        //if (isActive)
        //{
        //    if (!_eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected)
        //    {
        //        if (CellUnitWorker.HaveMaxSteps(xyCell))
        //        {
        //            _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = true;
        //            _eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed = false;
        //            _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
        //        }
        //    }
        //}

        //else
        //{
        //    if (_eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected)
        //    {
        //        _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = false;
        //    }
        //}
    }
}
