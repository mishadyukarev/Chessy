using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System;
using static Assets.Scripts.CellEnvirDataWorker;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;

    private int[] XyCellForBuilding => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes NeededBuildingTypeForBuilding => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellBuildingsDataWorker.HaveAnyBuilding(XyCellForBuilding))
        {
            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }

        else
        {
            switch (NeededBuildingTypeForBuilding)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (CellUnitsDataWorker.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        bool canSetCity = true;

                        foreach (var xy in CellSpaceWorker.TryGetXYAround(XyCellForBuilding))
                        {
                            if (!CellViewWorker.IsActiveSelfParentCell(xy))
                            {
                                canSetCity = false;
                            }
                        }

                        if (canSetCity)
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                            CellBuildingsDataWorker.SetPlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                            InfoBuidlingsWorker.AddXyBuild(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                            CellUnitsDataWorker.ResetAmountSteps(XyCellForBuilding);

                            if (HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding);
                            if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                        }

                        else
                        {
                            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(InfoFrom.Sender);
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                case BuildingTypes.Farm:
                    if (CellUnitsDataWorker.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        if (!HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding) && !HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForBuilding))
                        {
                            if (ResourcesDataUIWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                            {

                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                                if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding))
                                {
                                    AddAmountResources(EnvironmentTypes.Fertilizer, XyCellForBuilding, MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                                }

                                ResourcesDataUIWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);

                                CellBuildingsDataWorker.SetPlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                                InfoBuidlingsWorker.AddXyBuild(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitsDataWorker.TakeAmountSteps(XyCellForBuilding);

                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                                PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(InfoFrom.Sender);
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                    }
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();


                case BuildingTypes.Mine:
                    if (HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && HaveResources(EnvironmentTypes.Hill, XyCellForBuilding))
                    {
                        if (ResourcesDataUIWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                                ResourcesDataUIWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);
                                InfoBuidlingsWorker.AddXyBuild(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildingsDataWorker.SetPlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);

                                CellUnitsDataWorker.ResetAmountSteps(XyCellForBuilding);
                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                                PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.MistakeNeedOthePlaceToGeneral(InfoFrom.Sender);
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
