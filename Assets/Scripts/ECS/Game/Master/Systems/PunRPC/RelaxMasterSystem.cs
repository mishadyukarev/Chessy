using Assets.Scripts;

internal sealed class RelaxMasterSystem : SystemMasterReduction
{
    private bool NeedActiveRelax => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;
    private int[] XyCellForRelax => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (NeedActiveRelax)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).IsRelaxed)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).HaveMinAmountSteps)
                {
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).IsRelaxed = true;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).IsProtected = false;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).IsRelaxed)
            {
                _eGM.CellUnitEnt_CellUnitCom(XyCellForRelax).IsRelaxed = false;
            }
        }
    }
}
