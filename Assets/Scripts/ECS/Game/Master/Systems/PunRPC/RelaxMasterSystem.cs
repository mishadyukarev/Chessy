using Assets.Scripts;

internal sealed class RelaxMasterSystem : SystemMasterReduction
{
    private bool NeedActiveRelaxExtract => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;
    private int[] XyCellForRelaxExtract => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (NeedActiveRelaxExtract)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).IsRelaxed)
            {
                if (CellUnitWorker.HaveMaxSteps(XyCellForRelaxExtract))
                {
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).IsRelaxed = true;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).IsProtected = false;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).IsRelaxed)
            {
                _eGM.CellUnitEnt_CellUnitCom(XyCellForRelaxExtract).IsRelaxed = false;
            }
        }
    }
}
