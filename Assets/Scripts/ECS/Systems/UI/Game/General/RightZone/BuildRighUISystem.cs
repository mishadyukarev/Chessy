using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class BuildRighUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellBuildFilter = default;
    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelCell;

    public void Run()
    {
        ref var selUnitDatCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOwnUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        ref var selBuildDatCom = ref _cellBuildFilter.Get1(IdxSelCell);
        ref var selOwnBuildCom = ref _cellBuildFilter.Get2(IdxSelCell);
        ref var selBotBuildCom = ref _cellBuildFilter.Get3(IdxSelCell);

        ref var unitViewUICom = ref _unitZoneUIFilter.Get1(0);


        unitViewUICom.SetTextBuildInfo(LanguageComComp.GetText(GameLanguageTypes.BuildingAbilities));

        unitViewUICom.SetTextBuildButton(BuildingButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.BuildFarm));
        unitViewUICom.SetTextBuildButton(BuildingButtonTypes.Second, LanguageComComp.GetText(GameLanguageTypes.BuildMine));


        if (_selectorFilter.Get1(0).IsSelectedCell)
        {
            if (selUnitDatCom.HaveUnit)
            {
                if (selOwnUnitCom.HaveOwner)
                {
                    if (selOwnUnitCom.IsMine)
                    {
                        if (selUnitDatCom.IsUnitType(new[] { UnitTypes.Pawn }))
                        {
                            unitViewUICom .SetActiveUnitZone(UnitUIZoneTypes.Building, true);

                            if (selBuildDatCom.HaveBuild)
                            {
                                if (selOwnBuildCom.HaveOwner)
                                {
                                    if (selOwnBuildCom.IsMine)
                                    {
                                        if (selBuildDatCom.IsBuildType(BuildingTypes.City))
                                        {
                                            unitViewUICom .SetActiveBuilButton(BuildingButtonTypes.Third, false);
                                        }
                                        else
                                        {
                                            unitViewUICom .SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                            unitViewUICom.SetTextBuildButton(BuildingButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
                                        }
                                    }

                                    else
                                    {
                                        unitViewUICom.SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                        unitViewUICom.SetTextBuildButton(BuildingButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
                                    }
                                }

                                else if (selBotBuildCom.IsBot)
                                {
                                    if (selBuildDatCom.IsBuildType(BuildingTypes.City))
                                    {
                                        unitViewUICom.SetActiveBuilButton(BuildingButtonTypes.Third, true);
                                        unitViewUICom.SetTextBuildButton(BuildingButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
                                    }
                                }

                            }

                            else
                            {
                                if (_cellBuildFilter.IsSettedCity(PhotonNetwork.IsMasterClient))
                                {
                                    unitViewUICom.SetActiveBuilButton(BuildingButtonTypes.Third, false);
                                }
                                else
                                {
                                    unitViewUICom.SetTextBuildButton(BuildingButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.BuildCity));
                                }
                            }
                        }
                        else
                        {
                            unitViewUICom.SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                        }
                    }

                    else
                    {
                        unitViewUICom.SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                    }
                }

                else if (selBotUnitCom.IsBot)
                {
                    unitViewUICom.SetActiveUnitZone(UnitUIZoneTypes.Building, false);
                }
            }
        }

        else
        {
            unitViewUICom.SetActiveUnitZone(UnitUIZoneTypes.Building, false);
        }
    }
}
