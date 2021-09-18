using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

internal sealed class BuildRighUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<BuildAbilitUICom> _buildAbilViewCom = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellBuildFilt = default;
    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelCell;

    public void Run()
    {
        //ref var selUnitDatCom = ref _cellUnitFilter.Get1(IdxSelCell);
        //ref var selOnUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        //ref var selOffUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);
        //ref var selBotUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        //ref var selBuildDatCom = ref _cellBuildFilt.Get1(IdxSelCell);
        //ref var selOnBuildCom = ref _cellBuildFilt.Get2(IdxSelCell);
        //ref var selOffBuildCom = ref _cellBuildFilt.Get3(IdxSelCell);
        //ref var selBotBuildCom = ref _cellBuildFilt.Get4(IdxSelCell);

        //ref var buildAbilViewCom = ref _buildAbilViewCom.Get1(0);


        //buildAbilViewCom.SetTextInfo(LanguageComComp.GetText(GameLanguageTypes.BuildingAbilities));

        //buildAbilViewCom.SetText_Button(BuildButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.BuildFarm));
        //buildAbilViewCom.SetText_Button(BuildButtonTypes.Second, LanguageComComp.GetText(GameLanguageTypes.BuildMine));


        //var needActUnitZone = false;

        //if (_selectorFilter.Get1(0).IsSelectedCell)
        //{
        //    if (selUnitDatCom.HaveUnit)
        //    {
        //        var canCome = false;
        //        var isMastMain = false;

        //        if (selOnUnitCom.HaveOwner)
        //        {
        //            if (selOnUnitCom.IsMine)
        //            {
        //                canCome = true;
        //                isMastMain = selOnUnitCom.IsMasterClient;
        //            }
        //        }
        //        else if (selOffUnitCom.HaveLocPlayer)
        //        {
        //            if (selOffUnitCom.IsMine)
        //            {
        //                canCome = true;
        //                isMastMain = selOffUnitCom.IsMastMain;
        //            }
        //        }

        //        if (canCome)
        //        {
        //            if (selUnitDatCom.IsUnitType(new[] { UnitTypes.Pawn }))
        //            {
        //                needActUnitZone = true;

        //                if (selBuildDatCom.HaveBuild)
        //                {
        //                    if (selOnBuildCom.HaveOwner)
        //                    {
        //                        if (selOnBuildCom.IsMine)
        //                        {
        //                            if (selBuildDatCom.IsBuildType(BuildingTypes.City))
        //                            {
        //                                buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, false);
        //                            }
        //                            else
        //                            {
        //                                buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                                buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
        //                            }
        //                        }

        //                        else
        //                        {
        //                            buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                            buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
        //                        }
        //                    }

        //                    else if(selOffBuildCom.HaveLocPlayer)
        //                    {
        //                        if (selOffBuildCom.IsMine)
        //                        {
        //                            if (selBuildDatCom.IsBuildType(BuildingTypes.City))
        //                            {
        //                                buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, false);
        //                            }
        //                            else
        //                            {
        //                                buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                                buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                            buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
        //                        }
        //                    }

        //                    else if (selBotBuildCom.IsBot)
        //                    {
        //                        if (selBuildDatCom.IsBuildType(BuildingTypes.City))
        //                        {
        //                            buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                            buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.DestroyBuilding));
        //                        }
        //                    }

        //                }

        //                else
        //                {
        //                    buildAbilViewCom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.BuildCity));

        //                    foreach (var idx in _cellBuildFilt)
        //                    {
        //                        if (_cellBuildFilt.Get1(idx).IsBuildType(BuildingTypes.City))
        //                        {
        //                            if (_cellBuildFilt.Get3(idx).HaveLocPlayer)
        //                            {
        //                                if (_cellBuildFilt.Get3(idx).IsMastMain == isMastMain)
        //                                {
        //                                    buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, false);
        //                                }
        //                                else buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                            }
        //                            else buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                        }
        //                        else buildAbilViewCom.SetActive_Button(BuildButtonTypes.Third, true);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //buildAbilViewCom.SetActiveZone(needActUnitZone);
    }
}
