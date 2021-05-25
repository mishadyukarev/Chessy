using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class DestroyMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;
    private int[] xyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    internal DestroyMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.CellEnt_CellUnitCom(xyCell).IsHim(info.Sender))
        {
            if (_eGM.CellEnt_CellUnitCom(xyCell).HaveMaxSteps)
            {
                switch (_eGM.CellEnt_CellBuildingCom(xyCell).BuildingType)
                {
                    case BuildingTypes.City:

                        _photonPunRPC.EndGameToMaster(_eGM.CellEnt_CellUnitCom(xyCell).ActorNumber);

                        break;


                    case BuildingTypes.Farm:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellEnt_CellBuildingCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;


                    case BuildingTypes.Woodcutter:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellEnt_CellBuildingCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;

                    case BuildingTypes.Mine:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[_eGM.CellEnt_CellBuildingCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;

                }
            }
        }
    }
}