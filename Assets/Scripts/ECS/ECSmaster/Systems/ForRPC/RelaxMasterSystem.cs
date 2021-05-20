using Leopotam.Ecs;


internal class RelaxMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    internal bool isActive => _eGM.GeneralRPCEntActiveComponent.IsActived;
    internal int[] xyCell => _eMM.MasterRPCEntXyCellCom.XyCell;

    internal RelaxMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (isActive)
        {
            if (!_eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMaxSteps)
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
                if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).IsRelaxed = false;
                    _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
