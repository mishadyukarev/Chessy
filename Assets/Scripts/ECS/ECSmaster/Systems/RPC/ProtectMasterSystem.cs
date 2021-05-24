using Leopotam.Ecs;

internal class ProtectMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private int[] xyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    internal bool isActive => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;

    internal ProtectMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (isActive)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMaxSteps)
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
                if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsProtected = false;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
