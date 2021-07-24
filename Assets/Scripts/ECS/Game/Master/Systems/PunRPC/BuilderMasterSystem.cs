using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System;
using static Assets.Scripts.CellEnvironmentWorker;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCellForBuilding => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes NeededBuildingTypeForBuilding => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellBuildingWorker.HaveBuilding(XyCellForBuilding))
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }

        else
        {
            var unitType = CellUnitWorker.UnitType(XyCellForBuilding);

            switch (NeededBuildingTypeForBuilding)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (CellUnitWorker.HaveMaxAmountSteps(XyCellForBuilding))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                        CellBuildingWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                        InfoBuidlingsWorker.AddAmountBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                        CellUnitWorker.ResetAmountSteps(XyCellForBuilding);

                        if (HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding);
                        if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding)) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                case BuildingTypes.Farm:
                    if (!HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding) && !HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForBuilding))
                    {
                        if (InfoResourcesWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitWorker.HaveMinAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                                if (HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding))
                                {
                                    AddAmountResources(ResourceTypes.Food, XyCellForBuilding, MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                                }

                                InfoResourcesWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);

                                CellBuildingWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                                InfoBuidlingsWorker.AddAmountBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitWorker.TakeAmountSteps(XyCellForBuilding);
                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                    }
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();


                case BuildingTypes.Mine:
                    if (HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && HaveResources(ResourceTypes.Ore, XyCellForBuilding))
                    {
                        if (InfoResourcesWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitWorker.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                                InfoResourcesWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);
                                InfoBuidlingsWorker.AddAmountBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildingWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);

                                CellUnitWorker.ResetAmountSteps(XyCellForBuilding);
                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
