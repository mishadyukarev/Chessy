using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class BuildRighUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    private EcsFilter<BuildsInGameComponent> _idxBuildFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelectedCell;

    public void Init()
    {
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.First, delegate { Build(BuildingTypes.Farm); });
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Second, delegate { Build(BuildingTypes.Mine); });
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Build(BuildingTypes.City); });
    }

    public void Run()
    {
        ref var buildInGameCom = ref _idxBuildFilter.Get1(0);
        //ref var 

        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOwnerCellUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selBotOnwerCellUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(IdxSelCell);


        if (_selectorFilter.Get1(0).IsSelectedCell && selCellUnitDataCom.HaveUnit)
        {
            if (selOwnerCellUnitCom.HaveOwner)
            {
                if (selOwnerCellUnitCom.IsMine)
                {
                    _unitZoneUIFilter.Get1(0).RemoveAllListenersInBuildButton(BuildingButtonTypes.Third);

                    if (selCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))
                    {
                        _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, true);

                        if (selCellBuildDataCom.HaveBuild)
                        {
                            //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                            //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);

                            if (selOwnerCellUnitCom.HaveOwner)
                            {
                                if (selOwnerCellUnitCom.IsMine)
                                {
                                    if (selCellBuildDataCom.IsBuildType(BuildingTypes.City))
                                    {
                                        ///
                                        _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, false);
                                    }
                                    else
                                    {
                                        _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);

                                        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Destroy(); });
                                        _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                    }
                                }

                                else
                                {
                                    _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);

                                    _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Destroy(); });
                                    _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                }
                            }

                            else if (selBotOnwerCellUnitCom.IsBot)
                            {
                                if (selCellBuildDataCom.IsBuildType(BuildingTypes.City))
                                {
                                    _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);

                                    _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Destroy(); });
                                    _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                }
                            }

                        }

                        else
                        {
                            //if (!CellEnvirDataWorker.HaveEnvironments(XySelectedCell, new[] { EnvironmentTypes.AdultForest, EnvironmentTypes.YoungForest }))
                            //{
                            //    UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.First);
                            //}
                            //else
                            //{
                            //    UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                            //}


                            //if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, XySelectedCell))
                            //{
                            //    UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.Second);
                            //}

                            //else
                            //{
                            //    UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);
                            //}


                            if (buildInGameCom.IsSettedCity(PhotonNetwork.IsMasterClient))
                            {
                                _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, false);
                            }
                            else
                            {
                                _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Build(BuildingTypes.City); });
                                _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Build City");
                            }
                        }
                    }
                    else
                    {
                        _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                    }
                }

                else
                {
                    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                }
            }

            else if (selBotOnwerCellUnitCom.IsBot)
            {
                _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
            }
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
        }
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.BuildToMaster(IdxSelCell, buildingType);
    }
    private void Destroy()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.DestroyBuildingToMaster(IdxSelCell);
    }
}
