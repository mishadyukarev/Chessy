﻿using Assets.Scripts;
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


        if (!_eGM.CellBuildEnt_BuilTypeCom(XyCell).HaveBuilding)
        {
            bool canSet = false;
            switch (BuildingType)
            {
                case BuildingTypes.None:
                    break;

                case BuildingTypes.City:
                    CellBuildingWorker.SetPlayerBuilding(true, BuildingType, Info.Sender, XyCell);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();

                    _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Info.Sender.IsMasterClient] = true;
                    _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Info.Sender.IsMasterClient] = XyCell;

                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.AdultForest)) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.AdultForest);
                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Fertilizer)) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.Fertilizer);
                    break;

                case BuildingTypes.Farm:
                    canSet = !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.AdultForest);
                    break;

                case BuildingTypes.Woodcutter:
                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.AdultForest) && _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveResources(ResourceTypes.Wood);
                    break;

                case BuildingTypes.Mine:
                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Hill) && _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveResources(ResourceTypes.Ore);
                    break;

                default:
                    break;
            }
            if (canSet)
            {
                if(BuildingType != BuildingTypes.Farm )
                {
                    if (EconomyManager.CanCreateBuilding(BuildingType, Info.Sender, out bool[] haves))
                    {
                        var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCell).UnitType;

                        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
                        {
                            EconomyManager.CreateBuilding(BuildingType, Info.Sender);
                            CellBuildingWorker.SetPlayerBuilding(true, BuildingType, Info.Sender, XyCell);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                        }
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
                    }
                }
                else
                {
                    if (EconomyManager.CanCreateBuilding(BuildingType, Info.Sender, out bool[] haves))
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMinAmountSteps)
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Fertilizer))
                            {
                                _eGM.CellEnvEnt_CellEnvCom(XyCell).AddAmountResources(ResourceTypes.Food, _eGM.CellEnvEnt_CellEnvCom(XyCell).MaxAmountResources(EnvironmentTypes.Fertilizer));
                            }
                            else
                            {
                                _eGM.CellEnvEnt_CellEnvCom(XyCell).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                            }

                            EconomyManager.CreateBuilding(BuildingType, Info.Sender);
                            CellBuildingWorker.SetPlayerBuilding(true, BuildingType, Info.Sender, XyCell);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                        }
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
                    }
                }
            }
        }
    }
}
