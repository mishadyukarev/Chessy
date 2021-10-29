using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class UpdatorMastSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataCom,  OwnerCom> _cellBuildFilt = default;

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

                ref var curCellDatC = ref _cellDataFilt.Get1(curIdxCell);

                ref var curUnitCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curHpUnitC = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(curIdxCell);
                ref var condUnitC = ref _cellUnitOthFilt.Get2(curIdxCell);
                ref var twUnitC = ref _cellUnitOthFilt.Get3(curIdxCell);
                ref var effUnitC = ref _cellUnitOthFilt.Get4(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitOthFilt.Get5(curIdxCell);

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
                                if (!curHpUnitC.HaveCurMaxHpUnit(effUnitC, curUnitCom.UnitType))
                                {
                                    curHpUnitC.AddHp(100);

                                    if (curHpUnitC.StandMaxHpUnit(curUnitCom.UnitType) < curHpUnitC.AmountHp)
                                    {
                                        curHpUnitC.AmountHp = curHpUnitC.StandMaxHpUnit(curUnitCom.UnitType);
                                    }
                                }
                            }
                        }




                        if (curFireC.HaveFire)
                        {
                            condUnitC.CondUnitType = CondUnitTypes.None;
                        }

                        else
                        {
                            if (condUnitC.Is(CondUnitTypes.Relaxed))
                            {
                                if (curHpUnitC.HaveCurMaxHpUnit(effUnitC, curUnitCom.UnitType))
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
                                                    condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                            else
                                            {
                                                curBuilCom.Def();

                                                curEnvrC.Reset(EnvirTypes.AdultForest);
                                                WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);
                                            }
                                        }

                                        else if (twUnitC.ToolWeapType == ToolWeaponTypes.Pick)
                                        {
                                            if (curEnvrC.Have(EnvirTypes.Hill))
                                            {
                                                if (curBuilCom.HaveBuild)
                                                {
                                                    condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (curEnvrC.HaveMaxRes(EnvirTypes.Hill))
                                                    {
                                                        condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                    else
                                                    {
                                                        curEnvrC.SetMaxAmountRes(EnvirTypes.Hill);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                condUnitC.CondUnitType = CondUnitTypes.Protected;
                                            }
                                        }

                                        else
                                        {
                                            condUnitC.CondUnitType = CondUnitTypes.Protected;
                                        }
                                    }

                                    else
                                    {
                                        condUnitC.CondUnitType = CondUnitTypes.Protected;
                                    }
                                }

                                else
                                {
                                    curHpUnitC.AddStandartHp(effUnitC, curUnitCom.UnitType);
                                    if (curHpUnitC.AmountHp > curHpUnitC.CurMaxHpUnit(effUnitC, curUnitCom.UnitType))
                                    {
                                        curHpUnitC.AmountHp = curHpUnitC.CurMaxHpUnit(effUnitC, curUnitCom.UnitType);
                                    }
                                }
                            }

                            else if (condUnitC.Is(CondUnitTypes.Protected))
                            {
                                if (curHpUnitC.HaveCurMaxHpUnit(effUnitC, curUnitCom.UnitType))
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
                                if (curStepUnitC.HaveMinSteps)
                                {
                                    condUnitC.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }
                    }

                    curStepUnitC.SetMaxSteps(curUnitCom.UnitType);
                }

                else
                {
                    if (curBuilCom.Is(BuildingTypes.Camp)) curBuilCom.Def();
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

                            curBuilCom.Def();
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

                            curBuilCom.Def();

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
                            curBuilCom.Def();
                        }
                    }
                }

                if (curCellDatC.IsActiveCell)
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

            for (var playerType = Support.MinPlayerType; playerType < Support.MaxPlayerType; playerType++)
            {
                if (InventResourcesC.AmountRes(playerType, ResourceTypes.Food) < 0)
                {
                    InventResourcesC.Set(playerType, ResourceTypes.Food, 0);

                    for (UnitTypes unitType = UnitTypes.Scout; unitType >= UnitTypes.Pawn; unitType--)
                    {
                        bool isFindedUnit = false;

                        foreach (byte curIdxCell in _xyCellFilter)
                        {
                            ref var curUnitDatC = ref _cellUnitFilter.Get1(curIdxCell);
                            ref var curOwnUnitC = ref _cellUnitOthFilt.Get5(curIdxCell);

                            if (curUnitDatC.Is(unitType))
                            {
                                if (curOwnUnitC.Is(playerType))
                                {
                                    if (curUnitDatC.Is(UnitTypes.Scout))
                                        InventorUnitsC.AddUnitsInInventor(playerType, UnitTypes.Scout, LevelUnitTypes.Wood);
                                    curUnitDatC.DefUnitType();


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