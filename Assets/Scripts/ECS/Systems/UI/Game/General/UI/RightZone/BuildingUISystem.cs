using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class BuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;

    public void Init()
    {
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.First, delegate { Build(BuildingTypes.Farm); });
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Second, delegate { Build(BuildingTypes.Mine); });
        _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Build(BuildingTypes.City); });
    }

    public void Run()
    {
        if (_selectorFilter.Get1(0).IsSelectedCell && CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                {
                    _unitZoneUIFilter.Get1(0).RemoveAllListenersInBuildButton(BuildingButtonTypes.Third);

                    switch (CellUnitsDataSystem.UnitType(XySelectedCell))
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                            break;

                        case UnitTypes.Pawn:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.PawnSword:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.Rook:
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                            break;

                        case UnitTypes.RookCrossbow:
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                            break;

                        case UnitTypes.Bishop:
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                            break;

                        case UnitTypes.BishopCrossbow:
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                            break;

                        default:
                            throw new Exception();
                    }
                }

                else
                {
                    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                }
            }

            else if (CellUnitsDataSystem.IsBot(XySelectedCell))
            {
                _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
            }

            void PawnAndPawnSword()
            {
                _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, true);

                if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).HaveBuild)
                {
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);

                    if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitsDataSystem.IsMine(XySelectedCell))
                        {
                            if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).Is(BuildingTypes.City))
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

                    else if (CellBuildDataSystem.OwnerBotCom(XySelectedCell).IsBot)
                    {
                        if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).Is(BuildingTypes.City))
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


                    if (MainGameSystem.XyBuildingsCom.IsSettedCity(PhotonNetwork.IsMasterClient))
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
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
        }
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.BuildToMaster(XySelectedCell, buildingType);
    }
    private void Destroy()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.DestroyBuildingToMaster(XySelectedCell);
    }
}
