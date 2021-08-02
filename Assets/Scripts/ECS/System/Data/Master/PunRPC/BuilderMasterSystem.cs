using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Info;
using System;
using static Assets.Scripts.CellEnvirDataContainer;

internal sealed class BuilderMasterSystem : SystemMasterReduction
{
    private int[] XyCellForBuilding => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes NeededBuildingTypeForBuilding => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellBuildDataContainer.HaveAnyBuilding(XyCellForBuilding))
        {
            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
        }

        else
        {
            switch (NeededBuildingTypeForBuilding)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (CellUnitsDataContainer.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        bool canSetCity = true;

                        foreach (var xy in CellSpaceWorker.TryGetXyAround(XyCellForBuilding))
                        {
                            if (!CellViewContainer.IsActiveSelfParentCell(xy))
                            {
                                canSetCity = false;
                            }
                        }

                        if (canSetCity)
                        {
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                            CellBuildDataContainer.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);
                            InfoBuidlingsDataContainer.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                            CellUnitsDataContainer.ResetAmountSteps(XyCellForBuilding);

                            if (HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding);
                            if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                        }

                        else
                        {
                            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                case BuildingTypes.Farm:
                    if (CellUnitsDataContainer.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        if (!HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding) && !HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForBuilding))
                        {
                            if (ResourcesUIDataContainer.CanCreateNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
                            {

                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                                if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding))
                                {
                                    AddAmountResources(EnvironmentTypes.Fertilizer, XyCellForBuilding, MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                                }

                                ResourcesUIDataContainer.BuyNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender);

                                CellBuildDataContainer.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);
                                InfoBuidlingsDataContainer.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitsDataContainer.TakeAmountSteps(XyCellForBuilding);

                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                    }
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();


                case BuildingTypes.Mine:
                    if (HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && HaveResources(EnvironmentTypes.Hill, XyCellForBuilding))
                    {
                        if (ResourcesUIDataContainer.CanCreateNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitsDataContainer.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                                ResourcesUIDataContainer.BuyNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender);
                                InfoBuidlingsDataContainer.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildDataContainer.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);

                                CellUnitsDataContainer.ResetAmountSteps(XyCellForBuilding);
                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
                        }
                    }
                    else
                    {
                        PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
