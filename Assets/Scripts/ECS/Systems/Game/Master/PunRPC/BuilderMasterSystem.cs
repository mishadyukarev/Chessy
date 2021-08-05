using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Info;
using System;

internal sealed class BuilderMasterSystem : SystemMasterReduction
{
    private int[] XyCellForBuilding => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes NeededBuildingTypeForBuilding => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellBuildDataSystem.BuildTypeCom(XyCellForBuilding).HaveBuild)
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
                    if (CellUnitsDataSystem.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        bool canSetCity = true;

                        foreach (var xy in CellSpaceWorker.TryGetXyAround(XyCellForBuilding))
                        {
                            if (!CellViewSystem.IsActiveSelfParentCell(xy))
                            {
                                canSetCity = false;
                            }
                        }

                        if (canSetCity)
                        {
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                            CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);
                            MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                            CellUnitsDataSystem.ResetAmountSteps(XyCellForBuilding);

                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding)) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding);
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding)) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
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
                    if (CellUnitsDataSystem.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding) && !CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForBuilding))
                        {
                            if (ResourcesUIDataContainer.CanCreateNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
                            {

                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding))
                                {
                                    CellEnvrDataSystem.AddAmountResources(EnvironmentTypes.Fertilizer, XyCellForBuilding, CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                                }

                                ResourcesUIDataContainer.BuyNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender);

                                CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);
                                MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitsDataSystem.TakeAmountSteps(XyCellForBuilding);

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
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, XyCellForBuilding))
                    {
                        if (ResourcesUIDataContainer.CanCreateNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Building);

                                ResourcesUIDataContainer.BuyNewBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender);
                                MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, RpcMasterDataContainer.InfoFrom.Sender, XyCellForBuilding);

                                CellUnitsDataSystem.ResetAmountSteps(XyCellForBuilding);
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
