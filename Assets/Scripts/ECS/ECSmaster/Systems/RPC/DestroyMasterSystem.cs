using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class DestroyMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    internal DestroyMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.CellEnt_CellUnitCom(XyCell).IsHim(info.Sender))
        {
            if (_eGM.CellEnt_CellUnitCom(XyCell).HaveMaxSteps)
            {
                if(_eGM.CellBuildingEnt_BuildingTypeCom(XyCell).BuildingType == BuildingTypes.City)
                {
                    _photonPunRPC.EndGameToMaster(_eGM.CellEnt_CellUnitCom(XyCell).ActorNumberOwner);
                }
                _eGM.CellEnt_CellUnitCom(XyCell).AmountSteps = 0;
                _cellWorker.ResetBuilding(XyCell);
            }
        }
    }
}