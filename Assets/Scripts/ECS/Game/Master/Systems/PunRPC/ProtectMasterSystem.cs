using Assets.Scripts;

internal sealed class ProtectMasterSystem : SystemMasterReduction
{
    private int[] xyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    internal bool isActive => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;

    public override void Run()
    {
        base.Run();

        if (isActive)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMinAmountSteps)
                {
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = true;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed = false;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected)
            {
                _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = false;
            }
        }
    }
}
