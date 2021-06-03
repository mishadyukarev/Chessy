using Photon.Pun;

internal sealed class DestroyMasterSystem : RPCMasterSystemReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_CellOwnerCom(XyCell).IsHim(info.Sender))
        {
            if (_cM.CellUnitWorker.HaveMaxSteps(XyCell))
            {
                if (_eGM.CellBuildingEnt_BuildingTypeCom(XyCell).BuildingType == BuildingTypes.City)
                {
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyCell).ActorNumber);
                }
                _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;
                _cM.CellBuildingWorker.ResetBuilding(XyCell);
            }
        }
    }
}