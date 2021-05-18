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
        if (_eGM.CellUnitComponent(xyCell).IsHisUnit(info.Sender))
        {
            if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
            {
                if (info.Sender.IsMasterClient)
                {
                    switch (_eGM.CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            _photonPunRPC.EndGame(_eGM.CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _eGM.InfoEntBuildingsInfoCom.AmountFarmDict[info.Sender.IsMasterClient] -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;


                        case BuildingTypes.Woodcutter:

                            _eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[info.Sender.IsMasterClient] -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:

                            _eGM.InfoEntBuildingsInfoCom.AmountMineDict[info.Sender.IsMasterClient] -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                    }
                }

                else
                {
                    switch (_eGM.CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            _photonPunRPC.EndGame(_eGM.CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _eGM.InfoEntBuildingsInfoCom.AmountFarmDict[info.Sender.IsMasterClient] -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Woodcutter:

                            _eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[info.Sender.IsMasterClient] -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:
                            break;
                    }
                }

            }
        }
    }
}