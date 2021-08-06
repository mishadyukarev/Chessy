using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Realtime;
using System;

internal sealed class BuilderMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _gameWorld;
    private EcsFilter<InfoMasCom> _infoMasterFilter;
    private EcsFilter<ForBuildingMasCom, XyCellForDoingMasCom> _builderFilter;
    private EcsFilter<InventorResourcesComponent> _amountResFilt;

    private Player Sender => _infoMasterFilter.Get1(0).FromInfo.Sender;
    private int[] XyCellForBuilding => _builderFilter.Get2(0).XyCellForDoing;
    private BuildingTypes NeededBuildingTypeForBuilding => _builderFilter.Get1(0).BuildingTypeForBuidling;

    public void Init()
    {
        _gameWorld.NewEntity()
            .Replace(new ForBuildingMasCom())
            .Replace(new XyCellForDoingMasCom(new int[2]));
    }

    public void Run()
    {

        ref var amountResCom = ref _amountResFilt.Get1(0);


        if (CellBuildDataSystem.BuildTypeCom(XyCellForBuilding).HaveBuild)
        {
            RPCGameSystem.MistakeNeedOthePlaceToGeneral(Sender);
            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
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

                        foreach (var xy in CellSpaceSupport.TryGetXyAround(XyCellForBuilding))
                        {
                            if (!CellViewSystem.IsActiveSelfParentCell(xy))
                            {
                                canSetCity = false;
                            }
                        }

                        if (canSetCity)
                        {
                            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Building);

                            CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, Sender, XyCellForBuilding);
                            MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, Sender.IsMasterClient, XyCellForBuilding);

                            CellUnitsDataSystem.ResetAmountSteps(XyCellForBuilding);

                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding)) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding);
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding)) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                        }

                        else
                        {
                            RPCGameSystem.MistakeNeedOthePlaceToGeneral(Sender);
                            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                case BuildingTypes.Farm:
                    if (CellUnitsDataSystem.HaveMinAmountSteps(XyCellForBuilding))
                    {
                        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForBuilding) && !CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForBuilding))
                        {
                            if (amountResCom.CanCreateNewBuilding(NeededBuildingTypeForBuilding, Sender, out bool[] haves))
                            {

                                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Building);

                                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding))
                                {
                                    CellEnvrDataSystem.AddAmountResources(EnvironmentTypes.Fertilizer, XyCellForBuilding, CellEnvrDataSystem.MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.Fertilizer, XyCellForBuilding);
                                }

                                amountResCom.BuyNewBuilding(NeededBuildingTypeForBuilding, Sender);

                                CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, Sender, XyCellForBuilding);
                                MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, Sender.IsMasterClient, XyCellForBuilding);

                                CellUnitsDataSystem.TakeAmountSteps(XyCellForBuilding);

                            }
                            else
                            {
                                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                                RPCGameSystem.MistakeEconomyToGeneral(Sender, haves);
                            }
                        }
                        else
                        {
                            RPCGameSystem.MistakeNeedOthePlaceToGeneral(Sender);
                            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                        RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
                    }
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();


                case BuildingTypes.Mine:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, XyCellForBuilding) && CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, XyCellForBuilding))
                    {
                        if (amountResCom.CanCreateNewBuilding(NeededBuildingTypeForBuilding, Sender, out bool[] haves))
                        {
                            if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForBuilding))
                            {
                                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Building);

                                amountResCom.BuyNewBuilding(NeededBuildingTypeForBuilding, Sender);
                                MainGameSystem.XyBuildingsCom.AddXyBuild(NeededBuildingTypeForBuilding, Sender.IsMasterClient, XyCellForBuilding);
                                CellBuildDataSystem.SetPlayerBuilding(NeededBuildingTypeForBuilding, Sender, XyCellForBuilding);

                                CellUnitsDataSystem.ResetAmountSteps(XyCellForBuilding);
                            }
                            else
                            {
                                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                                RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
                            }
                        }
                        else
                        {
                            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                            RPCGameSystem.MistakeEconomyToGeneral(Sender, haves);
                        }
                    }
                    else
                    {
                        RPCGameSystem.MistakeNeedOthePlaceToGeneral(Sender);
                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
