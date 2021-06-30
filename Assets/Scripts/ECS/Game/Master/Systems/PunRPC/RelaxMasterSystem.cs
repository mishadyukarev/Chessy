using Assets.Scripts;

internal sealed class RelaxMasterSystem : SystemMasterReduction
{
    internal bool isActive => _eGM.RpcGeneralEnt_RPCCom.IsActived;
    internal int[] xyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (isActive)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed)
            {
                if (_cellM.CellUnitWorker.HaveMaxSteps(xyCell))
                {
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed = true;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = false;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed)
            {
                if (_cellM.CellUnitWorker.HaveMaxSteps(xyCell))
                {
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed = false;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
