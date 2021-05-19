using Leopotam.Ecs;
using Photon.Pun;

internal class ProtectMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private int[] xyCell => _eMM.MasterRPCEntXyCellCom.XyCell;
    internal bool isActive => _eGM.GeneralRPCEntActiveComponent.IsActived;

    internal ProtectMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (isActive)
        {
            if (!_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = true;
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }
    }
}
