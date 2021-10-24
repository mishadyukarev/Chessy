using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class UpdatorMastSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellWeatherDataCom> _cellWeatherFilt = default;

        private EcsFilter<UpgradesBuildsCom> _upgradeBuildsFilter = default;
        private EcsFilter<InventResourCom> _invResFilt = default;
        private EcsFilter<InventorUnitsCom> _invUnitsFilt = default;
        private EcsFilter<MotionsDataUIComponent> _motionsUIFilt = default;
        private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilt = default;
        private EcsFilter<WindCom> _windFilt = default;

        public void Run()
        {
            ref var invResCom = ref _invResFilt.Get1(0);
            ref var invUnitsCom = ref _invUnitsFilt.Get1(0);
            ref var amountUpgradesCom = ref _upgradeBuildsFilter.Get1(0);
            int minus;
            int amountAdultForest = 0;


            invResCom.AddAmountRes(PlayerTypes.First, ResourceTypes.Food, 3);
            invResCom.AddAmountRes(PlayerTypes.Second, ResourceTypes.Food, 3);

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);

            foreach (byte curIdxCell in _xyCellFilter)
            {
                var curXy = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curCellViewCom = ref _cellViewFilter.Get1(curIdxCell);

                ref var curUnitCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var curBuilCom = ref _cellBuildFilt.Get1(curIdxCell);
                ref var curOnBuilCom = ref _cellBuildFilt.Get2(curIdxCell);

                ref var curFireDatCom = ref _cellFireDataFilter.Get1(curIdxCell);
                ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);

                ref var curWeathCom = ref _cellWeatherFilt.Get1(curIdxCell);

                if (curUnitCom.HaveUnit)
                {
                    if (!curUnitCom.Is(UnitTypes.King)) invResCom.TakeAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Food);

                    if (!curUnitCom.Is(UnitTypes.Scout))
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (curOwnUnitCom.IsPlayerType(PlayerTypes.Second))
                            {
                                if (!curUnitCom.HaveMaxAmountHealth)
                                {
                                    curUnitCom.AddAmountHealth(100);

                                    if (curUnitCom.MaxAmountHealth < curUnitCom.AmountHealth)
                                    {
                                        curUnitCom.AmountHealth = curUnitCom.MaxAmountHealth;
                                    }
                                }
                            }
                        }




                        if (curFireDatCom.HaveFire)
                        {
                            curUnitCom.CondUnitType = CondUnitTypes.None;
                        }

                        else
                        {
                            if (curUnitCom.Is(CondUnitTypes.Relaxed))
                            {
                                if (curUnitCom.HaveMaxAmountHealth)
                                {
                                    if (curUnitCom.Is(UnitTypes.Pawn))
                                    {
                                        if (curEnvrDatCom.Have(EnvirTypes.AdultForest))
                                        {
                                            invResCom.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Wood);
                                            curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest);

                                            if (curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                                            {
                                                if (curBuilCom.HaveBuild)
                                                {
                                                    if (!curBuilCom.HaveBuild)
                                                    {
                                                        curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                }
                                                else
                                                {
                                                    curBuilCom.BuildType = BuildingTypes.Woodcutter;
                                                    curOnBuilCom.PlayerType = curOwnUnitCom.PlayerType;
                                                }
                                            }
                                            else
                                            {
                                                curBuilCom.DefBuildType();
                                                curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);
                                            }
                                        }

                                        else if (curUnitCom.TWExtraType == ToolWeaponTypes.Pick)
                                        {
                                            if (curEnvrDatCom.Have(EnvirTypes.Hill))
                                            {
                                                if (curBuilCom.HaveBuild)
                                                {
                                                    curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (curEnvrDatCom.HaveMaxRes(EnvirTypes.Hill))
                                                    {
                                                        curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                    else
                                                    {
                                                        curEnvrDatCom.SetAmountResources(EnvirTypes.Hill, curEnvrDatCom.MaxAmountRes(EnvirTypes.Hill));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                            }
                                        }

                                        else
                                        {
                                            curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                        }
                                    }

                                    else
                                    {
                                        curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                    }
                                }

                                else
                                {
                                    curUnitCom.AddStandartHeal();
                                    if (curUnitCom.AmountHealth > curUnitCom.MaxAmountHealth)
                                    {
                                        curUnitCom.AmountHealth = curUnitCom.MaxAmountHealth;
                                    }
                                }
                            }
                            else if (curUnitCom.Is(CondUnitTypes.None))
                            {
                                if (curUnitCom.HaveMaxAmountSteps)
                                {
                                    curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }
                    }

                    curUnitCom.SetMaxAmountSteps();
                }

                if (curBuilCom.HaveBuild)
                {

                    if (curBuilCom.IsBuildType(BuildingTypes.Farm))
                    {
                        minus = amountUpgradesCom.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Farm);

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.Fertilizer, minus);
                        invResCom.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Food, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.Fertilizer))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvirTypes.Fertilizer);

                            curBuilCom.DefBuildType();
                        }
                    }

                    else if (curBuilCom.IsBuildType(BuildingTypes.Woodcutter))
                    {
                        minus = amountUpgradesCom.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Woodcutter);

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, minus);
                        invResCom.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Wood, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);
                            SpawnNewSeed(curIdxCell);

                            curBuilCom.DefBuildType();

                            if (curFireDatCom.HaveFire)
                            {
                                curFireDatCom.HaveFire = false;
                            }
                        }
                    }

                    else if (curBuilCom.IsBuildType(BuildingTypes.Mine))
                    {
                        minus = amountUpgradesCom.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Mine);

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.Hill, minus);
                        invResCom.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Ore, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.Hill))
                        {
                            curBuilCom.DefBuildType();
                        }
                    }
                }

                if (curCellViewCom.IsActiveParent)
                {
                    if (curEnvrDatCom.Have(EnvirTypes.AdultForest))
                    {
                        ++amountAdultForest;
                    }
                }

                if (curFireDatCom.HaveFire)
                {
                    curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, 2);

                    if (curUnitCom.HaveUnit)
                    {
                        curUnitCom.TakeAmountHealth(40);

                        if (!curUnitCom.HaveAmountHealth)
                        {
                            if (curUnitCom.Is(UnitTypes.King))
                            {
                                if (curOwnUnitCom.IsPlayerType(PlayerTypes.First))
                                {
                                    _endGameDataUIFilt.Get1(0).PlayerWinner = PlayerTypes.Second;
                                }
                                else
                                {
                                    _endGameDataUIFilt.Get1(0).PlayerWinner = PlayerTypes.First;
                                }

                            }

                            curUnitCom.DefUnitType();
                        }
                    }



                    if (!curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                    {
                        if (curBuilCom.HaveBuild)
                        {
                            curBuilCom.BuildType = default;
                        }

                        curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);
                        SpawnNewSeed(curIdxCell);

                        curFireDatCom.HaveFire = false;


                        var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

                            if (_cellViewFilter.Get1(curIdxCell1).IsActiveParent)
                            {
                                if (_cellEnvDataFilter.Get1(curIdxCell1).Have(EnvirTypes.AdultForest))
                                {
                                    _cellFireDataFilter.Get1(curIdxCell1).HaveFire = true;
                                }
                            }
                        }
                    }
                }

                if(curWeathCom.IsCenter)
                {
                    //var newXy = CellSpaceSupport.GetXyCellByDirect(curXy, _windFilt.Get1(0).DirectWind);
                    //var newIdxCell = _xyCellFilter.GetIdxCell(newXy);

                    //ref var newWeathCom = ref _cellWeatherFilt.Get1(newIdxCell);
                    //newWeathCom.EnabledCloud = true;
                    //newWeathCom.CloudWidthType = curWeathCom.CloudWidthType;

                    //curWeathCom.CloudWidthType = default;
                    //curWeathCom.EnabledCloud = default;
                }
            }

            if (amountAdultForest <= 6)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
                GameMasterSystemManager.TruceSystems.Run();
            }

            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                if (invResCom.AmountRes(playerType, ResourceTypes.Food) < 0)
                {
                    invResCom.SetAmountRes(playerType, ResourceTypes.Food, 0);

                    for (UnitTypes unitType = UnitTypes.Scout; unitType >= UnitTypes.Pawn; unitType--)
                    {
                        bool isFindedUnit = false;

                        foreach (byte curIdxCell in _xyCellFilter)
                        {
                            ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                            ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                            if (curUnitDatCom.Is(unitType))
                            {
                                if (curOnUnitCom.PlayerType == playerType)
                                {
                                    if (curUnitDatCom.Is(UnitTypes.Scout))
                                        invUnitsCom.AddUnitsInInventor(playerType, UnitTypes.Scout, LevelUnitTypes.Wood);
                                    curUnitDatCom.DefUnitType();


                                    isFindedUnit = true;
                                    break;
                                }
                            }


                        }
                        if (isFindedUnit) break;
                    }
                }
            }

            if (_motionsUIFilt.Get1(0).AmountMotions % 3 == 0)
            {
                foreach (byte curIdxCell in _xyCellFilter)
                {
                    ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                    ref var curBuildDatCom = ref _cellBuildFilt.Get1(curIdxCell);

                    if (curEnvrDatCom.Have(EnvirTypes.Hill))
                    {
                        if (!curBuildDatCom.IsBuildType(BuildingTypes.Mine))
                        {
                            if (!curEnvrDatCom.HaveMaxRes(EnvirTypes.Hill))
                            {
                                curEnvrDatCom.AddAmountRes(EnvirTypes.Hill);
                            }
                        }
                    }
                }
            }

            _motionsUIFilt.Get1(0).AmountMotions += 1;

            var rand = UnityEngine.Random.Range(0, 100);
            if (rand <= 30) _windFilt.Get1(0).DirectWind = (DirectTypes)UnityEngine.Random.Range(1, Enum.GetNames(typeof(DirectTypes)).Length);         
        }


        private void SpawnNewSeed(byte idxCellStart)
        {
            ref var windCom = ref _windFilt.Get1(0);

            if (UnityEngine.Random.Range(0, 100) < 30)
            {
                ref var envDatCom = ref _cellEnvDataFilter.Get1(idxCellStart);
                envDatCom.SetEnvironment(EnvirTypes.YoungForest);
            }
        }
    }
}