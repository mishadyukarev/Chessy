using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Pun;
using static Assets.Scripts.CellEnvironmentWorker;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCell => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes BuildingType => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

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

                        if (HaveEnvironment(EnvironmentTypes.AdultForest, XyCell)) ResetEnvironment(EnvironmentTypes.AdultForest, XyCell);
                        if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCell)) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);
                    }
                    else
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;

                case BuildingTypes.Farm:
                    canSet = !HaveEnvironment(EnvironmentTypes.AdultForest, XyCell) && !HaveEnvironment(EnvironmentTypes.YoungForest, XyCell);
                    break;

                case BuildingTypes.Woodcutter:
                    canSet = HaveEnvironment(EnvironmentTypes.AdultForest, XyCell) && HaveResources(ResourceTypes.Wood, XyCell);
                    break;

                case BuildingTypes.Mine:
                    canSet = HaveEnvironment(EnvironmentTypes.Hill, XyCell) && HaveResources(ResourceTypes.Ore, XyCell);
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

                            if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCell))
                            {
                                AddAmountResources(ResourceTypes.Food, XyCell, MaxAmountResources(EnvironmentTypes.Fertilizer));
                            }
                            else
                            {
                                SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCell);
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
