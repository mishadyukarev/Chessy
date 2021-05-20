using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class DestroyMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;
    private int[] xyCell => _eMM.MasterRPCEntXyCellCom.XyCell;
    private PhotonMessageInfo info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    internal DestroyMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
    }

    public void Run()
    {
        if (_eGM.CellUnitEnt_OwnerCom(xyCell).IsHim(info.Sender))
        {
            if (_eGM.CellUnitEnt_CellUnitCom(xyCell).HaveMaxSteps)
            {
                switch (_eGM.CellBuildingEnt_BuildingTypeCom(xyCell).BuildingType)
                {
                    case BuildingTypes.City:

                        _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_OwnerCom(xyCell).ActorNumber);

                        break;


                    case BuildingTypes.Farm:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellBuildingEnt_OwnerCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellBuildingEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;


                    case BuildingTypes.Woodcutter:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellBuildingEnt_OwnerCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellBuildingEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;

                    case BuildingTypes.Mine:

                        _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[_eGM.CellBuildingEnt_OwnerCom(xyCell).Owner.IsMasterClient] -= 1;
                        _eGM.CellUnitEnt_CellUnitCom(xyCell).AmountSteps = 0;
                        _eGM.CellBuildingEnt_CellBuildingCom(xyCell).ResetBuilding();

                        break;

                }
            }
        }
    }
}