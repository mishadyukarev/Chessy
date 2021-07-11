using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Pun;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (_eGM.CellBuildEnt_BuilTypeCom(XyCell).HaveBuilding)
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }

        else
        {
            var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCell).UnitType;

            bool canSet = false;
            switch (BuildingType)
            {
                case BuildingTypes.None:
                    break;

                case BuildingTypes.City:
                    if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                        CellBuildingWorker.SetPlayerBuilding(true, BuildingType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();

                        _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[InfoFrom.Sender.IsMasterClient] = true;
                        _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[InfoFrom.Sender.IsMasterClient] = XyCell;

                        if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.AdultForest)) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.AdultForest);
                        if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Fertilizer)) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.Fertilizer);
                    }
                    else
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;

                case BuildingTypes.Farm:
                    canSet = !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.AdultForest) && !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.YoungForest);
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
                if (BuildingType != BuildingTypes.Farm)
                {
                    if (EconomyManager.CanCreateBuilding(BuildingType, InfoFrom.Sender, out bool[] haves))
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                            EconomyManager.CreateBuilding(BuildingType, InfoFrom.Sender);
                            CellBuildingWorker.SetPlayerBuilding(true, BuildingType, InfoFrom.Sender, XyCell);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                        }
                        else
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                    }
                }
                else
                {
                    if (EconomyManager.CanCreateBuilding(BuildingType, InfoFrom.Sender, out bool[] haves))
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMinAmountSteps)
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                            if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Fertilizer))
                            {
                                _eGM.CellEnvEnt_CellEnvCom(XyCell).AddAmountResources(ResourceTypes.Food, _eGM.CellEnvEnt_CellEnvCom(XyCell).MaxAmountResources(EnvironmentTypes.Fertilizer));
                            }
                            else
                            {
                                _eGM.CellEnvEnt_CellEnvCom(XyCell).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                            }

                            EconomyManager.CreateBuilding(BuildingType, InfoFrom.Sender);
                            CellBuildingWorker.SetPlayerBuilding(true, BuildingType, InfoFrom.Sender, XyCell);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).TakeAmountSteps();
                        }
                        else
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                    }
                }
            }
        }
    }
}
