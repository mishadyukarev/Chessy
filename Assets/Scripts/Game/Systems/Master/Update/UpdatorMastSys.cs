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
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            int minus;
            int amountAdultForest = 0;


            var curXy = new byte[0];

            InventResourcesC.AddAmountRes(PlayerTypes.First, ResourceTypes.Food, 3);
            InventResourcesC.AddAmountRes(PlayerTypes.Second, ResourceTypes.Food, 3);

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);

            foreach (byte curIdxCell in _xyCellFilter)
            {
                curXy = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curCellViewCom = ref _cellViewFilter.Get1(curIdxCell);

                ref var curUnitCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var curBuilCom = ref _cellBuildFilt.Get1(curIdxCell);
                ref var curOwnBuilCom = ref _cellBuildFilt.Get2(curIdxCell);

                ref var curFireC = ref _cellFireDataFilter.Get1(curIdxCell);
                ref var curEnvrC = ref _cellEnvDataFilter.Get1(curIdxCell);


                if (curUnitCom.HaveUnit)
                {
                    if (!curUnitCom.Is(UnitTypes.King)) InventResourcesC.TakeAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Food);

                    if (!curUnitCom.Is(UnitTypes.Scout))
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (curOwnUnitCom.Is(PlayerTypes.Second))
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




                        if (curFireC.HaveFire)
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
                                        if (curEnvrC.Have(EnvirTypes.AdultForest))
                                        {
                                            InventResourcesC.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Wood);
                                            curEnvrC.TakeAmountRes(EnvirTypes.AdultForest);

                                            if (curEnvrC.HaveRes(EnvirTypes.AdultForest))
                                            {
                                                if (curBuilCom.Is(BuildingTypes.Camp) || !curBuilCom.HaveBuild)
                                                {
                                                    curBuilCom.BuildType = BuildingTypes.Woodcutter;
                                                    curOwnBuilCom.PlayerType = curOwnUnitCom.PlayerType;
                                                }
                                                else if (curBuilCom.Is(BuildingTypes.Woodcutter))
                                                {

                                                }
                                                else
                                                {
                                                    curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                            else
                                            {
                                                curBuilCom.DefBuildType();

                                                curEnvrC.Reset(EnvirTypes.AdultForest);
                                                WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);
                                            }
                                        }

                                        else if (curUnitCom.TWExtraType == ToolWeaponTypes.Pick)
                                        {
                                            if (curEnvrC.Have(EnvirTypes.Hill))
                                            {
                                                if (curBuilCom.HaveBuild)
                                                {
                                                    curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (curEnvrC.HaveMaxRes(EnvirTypes.Hill))
                                                    {
                                                        curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                    else
                                                    {
                                                        curEnvrC.SetMaxAmountRes(EnvirTypes.Hill);
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

                            else if (curUnitCom.Is(CondUnitTypes.Protected))
                            {
                                if (curUnitCom.HaveMaxAmountHealth)
                                {
                                    if (curUnitCom.Is(UnitTypes.Pawn))
                                    {
                                        if (curEnvrC.Have(EnvirTypes.AdultForest))
                                        {
                                            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                            {
                                                if (curOwnUnitCom.Is(PlayerTypes.First))
                                                {
                                                    curBuilCom.BuildType = BuildingTypes.Camp;
                                                    curOwnBuilCom.PlayerType = curOwnUnitCom.PlayerType;
                                                }
                                            }
                                            else
                                            {
                                                curBuilCom.BuildType = BuildingTypes.Camp;
                                                curOwnBuilCom.PlayerType = curOwnUnitCom.PlayerType;
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                if (curUnitCom.HaveMinAmountSteps)
                                {
                                    curUnitCom.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }
                    }

                    curUnitCom.SetMaxAmountSteps();
                }

                else
                {
                    if (curBuilCom.Is(BuildingTypes.Camp)) curBuilCom.DefBuildType();
                }

                if (curBuilCom.HaveBuild)
                {

                    if (curBuilCom.Is(BuildingTypes.Farm))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Farm);

                        curEnvrC.TakeAmountRes(EnvirTypes.Fertilizer, minus);
                        InventResourcesC.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Food, minus);

                        if (!curEnvrC.HaveRes(EnvirTypes.Fertilizer))
                        {
                            curEnvrC.Reset(EnvirTypes.Fertilizer);
                            WhereEnvironmentC.Remove(EnvirTypes.Fertilizer, curIdxCell);

                            curBuilCom.DefBuildType();
                        }
                    }

                    else if (curBuilCom.Is(BuildingTypes.Woodcutter))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Woodcutter);

                        curEnvrC.TakeAmountRes(EnvirTypes.AdultForest, minus);
                        InventResourcesC.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Wood, minus);

                        if (!curEnvrC.HaveRes(EnvirTypes.AdultForest))
                        {
                            curEnvrC.Reset(EnvirTypes.AdultForest);
                            WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);

                            SpawnNewSeed(curIdxCell);

                            curBuilCom.DefBuildType();

                            if (curFireC.HaveFire)
                            {
                                curFireC.HaveFire = false;
                            }
                        }
                    }

                    else if (curBuilCom.Is(BuildingTypes.Mine))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(curOwnUnitCom.PlayerType, BuildingTypes.Mine);

                        curEnvrC.TakeAmountRes(EnvirTypes.Hill, minus);
                        InventResourcesC.AddAmountRes(curOwnUnitCom.PlayerType, ResourceTypes.Ore, minus);

                        if (!curEnvrC.HaveRes(EnvirTypes.Hill))
                        {
                            curBuilCom.DefBuildType();
                        }
                    }
                }

                if (curCellViewCom.IsActiveParent)
                {
                    if (curEnvrC.Have(EnvirTypes.AdultForest))
                    {
                        ++amountAdultForest;
                    }
                }
            }


            if (amountAdultForest <= 6)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
                GameMasSysDataM.TruceSystems.Run();
            }

            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                if (InventResourcesC.AmountRes(playerType, ResourceTypes.Food) < 0)
                {
                    InventResourcesC.Set(playerType, ResourceTypes.Food, 0);

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
                                        InventorUnitsC.AddUnitsInInventor(playerType, UnitTypes.Scout, LevelUnitTypes.Wood);
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

            if (MotionsDataUIC.AmountMotions % 3 == 0)
            {
                foreach (byte curIdxCell in _xyCellFilter)
                {
                    ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                    ref var curBuildDatCom = ref _cellBuildFilt.Get1(curIdxCell);

                    if (curEnvrDatCom.Have(EnvirTypes.Hill))
                    {
                        if (!curBuildDatCom.Is(BuildingTypes.Mine))
                        {
                            if (!curEnvrDatCom.HaveMaxRes(EnvirTypes.Hill))
                            {
                                curEnvrDatCom.AddAmountRes(EnvirTypes.Hill);
                            }
                        }
                    }
                }
            }

            MotionsDataUIC.AmountMotions += 1;
        }

        private void SpawnNewSeed(byte idxCellStart)
        {
            if (UnityEngine.Random.Range(0, 100) < 70)
            {
                ref var envDatCom = ref _cellEnvDataFilter.Get1(idxCellStart);

                envDatCom.SetNew(EnvirTypes.YoungForest);
                WhereEnvironmentC.Add(EnvirTypes.YoungForest, idxCellStart);
            }
        }
    }
}