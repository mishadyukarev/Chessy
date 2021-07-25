using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System;
using static Assets.Scripts.CellEnvirDataWorker;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCellForBuilding => _eMM.BuildEnt_XyCellCom.XyCell;
    private BuildingTypes NeededBuildingTypeForBuilding => _eMM.BuildEnt_BuildingTypeCom.BuildingType;

    public override void Run()
    {
        base.Run();


        if (CellBuildingsDataWorker.HaveAnyBuilding(XyCellForBuilding))
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }

        else
        {
            switch (NeededBuildingTypeForBuilding)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForBuilding))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                        CellBuildingsDataWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                        InfoBuidlingsWorker.AddXyBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForBuilding);

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
                        if (InfoResourcesDataWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitsDataWorker.HaveMinAmountSteps(XyCellForBuilding))
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

                                InfoResourcesDataWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);

                                CellBuildingsDataWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);
                                InfoBuidlingsWorker.AddXyBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitsDataWorker.TakeAmountSteps(XyCellForBuilding);
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
                    if (HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && HaveResources(EnvironmentTypes.Hill, XyCellForBuilding))
                    {
                        if (InfoResourcesDataWorker.CanCreateNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, out bool[] haves))
                        {
                            if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Building);

                                InfoResourcesDataWorker.BuyNewBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender);
                                InfoBuidlingsWorker.AddXyBuildingsInGame(NeededBuildingTypeForBuilding, InfoFrom.Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildingsDataWorker.CreatePlayerBuilding(NeededBuildingTypeForBuilding, InfoFrom.Sender, XyCellForBuilding);

                                CellUnitsDataWorker.ResetAmountSteps(XyCellForBuilding);
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
