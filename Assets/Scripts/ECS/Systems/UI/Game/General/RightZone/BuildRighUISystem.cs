using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class BuildRighUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerComponent, OwnerBotComponent> _cellBuildFilter = default;
    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelectedCell;

    public void Run()
    {
        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOwnerCellUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selBotOnwerCellUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(IdxSelCell);
        ref var selOwnerBuildCom = ref _cellBuildFilter.Get2(IdxSelCell);
        ref var selBotBuildCom = ref _cellBuildFilter.Get3(IdxSelCell);


        if (_selectorFilter.Get1(0).IsSelectedCell)
        {
            if (selCellUnitDataCom.HaveUnit)
            {
                if (selOwnerCellUnitCom.HaveOwner)
                {
                    if (selOwnerCellUnitCom.IsMine)
                    {
                        if (selCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))
                        {
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, true);

                            if (selCellBuildDataCom.HaveBuild)
                            {
                                if (selOwnerBuildCom.HaveOwner)
                                {
                                    if (selOwnerBuildCom.IsMine)
                                    {
                                        if (selCellBuildDataCom.IsBuildType(BuildingTypes.City))
                                        {
                                            _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, false);
                                        }
                                        else
                                        {
                                            _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                            _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                        }
                                    }

                                    else
                                    {
                                        _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                        _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                    }
                                }

                                else if (selBotBuildCom.IsBot)
                                {
                                    if (selCellBuildDataCom.IsBuildType(BuildingTypes.City))
                                    {
                                        _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                        _unitZoneUIFilter.Get1(0).SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                                    }
                                }

                            }

                            else
                            {
                                if (_cellBuildFilter.IsSettedCity(PhotonNetwork.IsMasterClient))
                                {
                                    _unitZoneUIFilter.Get1(0).SetActiveBuilButton(BuildingButtonTypes.Third, false);
                                }
                                else
                                {
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
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Building, false);
        }
    }
}
