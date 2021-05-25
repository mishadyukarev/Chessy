using Leopotam.Ecs;


internal class RelaxMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    internal bool isActive => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;
    internal int[] xyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    internal RelaxMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (isActive)
        {
            if (!_eGM.CellEnt_CellUnitCom(xyCell).IsRelaxed)
            {
                if (_eGM.CellEnt_CellUnitCom(xyCell).HaveMaxSteps)
                {
                    _eGM.CellEnt_CellUnitCom(xyCell).IsRelaxed = true;
                    _eGM.CellEnt_CellUnitCom(xyCell).IsProtected = false;
                    _eGM.CellEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellEnt_CellUnitCom(xyCell).IsRelaxed)
            {
                if (_eGM.CellEnt_CellUnitCom(xyCell).HaveMaxSteps)
                {
                    _eGM.CellEnt_CellUnitCom(xyCell).IsRelaxed = false;
                    _eGM.CellEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
