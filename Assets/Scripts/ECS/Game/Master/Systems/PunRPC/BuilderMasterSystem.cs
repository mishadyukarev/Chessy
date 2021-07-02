using Assets.Scripts;
using Assets.Scripts.Static;
using Photon.Pun;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;
    private BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellUnitWorker.HaveMaxSteps(XyCell) && !_eGM.CellBuildEnt_BuilTypeCom(XyCell).HaveBuilding)
        {
            bool canSet = false;
            switch (BuildingType)
            {
                case BuildingTypes.None:
                    break;

                case BuildingTypes.City:
                    CellBuildingWorker.SetPlayerBuilding(true, BuildingType, Info.Sender, XyCell);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;

                    _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Info.Sender.IsMasterClient] = true;
                    _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Info.Sender.IsMasterClient] = XyCell;

                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.AdultForest);
                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.Fertilizer);
                    break;

                case BuildingTypes.Farm:
                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer && _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizerResources;
                    break;

                case BuildingTypes.Woodcutter:
                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree && _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveForestResources;
                    break;

                case BuildingTypes.Mine:
                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveHill && _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveOreResources;
                    break;

                default:
                    break;
            }
            if (canSet)
            {
                if (EconomyManager.CanCreateBuilding(BuildingType, Info.Sender, out bool[] haves))
                {
                    EconomyManager.CreateBuilding(BuildingType, Info.Sender);
                    CellBuildingWorker.SetPlayerBuilding(true, BuildingType, Info.Sender, XyCell);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;
                }
                else
                {
                    _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
                }
            }
        }
    }
}
