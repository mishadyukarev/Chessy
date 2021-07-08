using Assets.Scripts;
using Assets.Scripts.Static;
using Photon.Pun;

internal sealed class DestroyMasterSystem : RPCMasterSystemReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_CellOwnerCom(XyCell).IsHim(info.Sender))
        {
            var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCell).UnitType;

            if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
            {
                if (_eGM.CellBuildEnt_BuilTypeCom(XyCell).BuildingType == BuildingTypes.City)
                {
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyCell).ActorNumber);
                }
                _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();

                CellBuildingWorker.ResetBuilding(true, XyCell);
            }
        }
    }
}