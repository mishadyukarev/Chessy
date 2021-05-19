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
            if (!_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = true;
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
