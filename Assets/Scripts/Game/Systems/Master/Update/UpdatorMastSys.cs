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
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataC,  OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            int minus;
            int amountAdultForest = 0;


            var curXy = new byte[0];

            InventResC.AddAmountRes(PlayerTypes.First, ResourceTypes.Food, 3);
            InventResC.AddAmountRes(PlayerTypes.Second, ResourceTypes.Food, 3);

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);

            foreach (byte idx_0 in _xyCellFilter)
            {
                curXy = _xyCellFilter.GetXyCell(idx_0);

                ref var curCellDatC = ref _cellDataFilt.Get1(idx_0);

                ref var curUnitCom = ref _cellUnitFilter.Get1(idx_0);
                ref var curHpUnitC = ref _cellUnitFilter.Get2(idx_0);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(idx_0);
                ref var condUnitC = ref _cellUnitOthFilt.Get2(idx_0);
                ref var twUnitC = ref _cellUnitOthFilt.Get3(idx_0);
                ref var effUnitC = ref _cellUnitOthFilt.Get4(idx_0);
                ref var ownUnitC_0 = ref _cellUnitOthFilt.Get5(idx_0);

                ref var builC_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuilC_0 = ref _cellBuildFilt.Get2(idx_0);

                ref var curFireC = ref _cellFireDataFilter.Get1(idx_0);
                ref var envrC_0 = ref _cellEnvDataFilter.Get1(idx_0);


                if (curUnitCom.HaveUnit)
                {
                    if (!curUnitCom.Is(UnitTypes.King)) InventResC.TakeAmountRes(ownUnitC_0.Owner, ResourceTypes.Food);

                    if (!curUnitCom.Is(UnitTypes.Scout))
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (ownUnitC_0.Is(PlayerTypes.Second))
                            {
                                if (!curHpUnitC.HaveMaxHpUnit(effUnitC, curUnitCom.UnitType))
                                {
                                    curHpUnitC.AddHp(100);

                                    if (curHpUnitC.MaxHpUnit(effUnitC, curUnitCom.UnitType) < curHpUnitC.AmountHp)
                                    {
                                        curHpUnitC.AmountHp = curHpUnitC.MaxHpUnit(effUnitC, curUnitCom.UnitType);
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
                                if (curHpUnitC.HaveMaxHpUnit(effUnitC, curUnitCom.UnitType))
                                {
                                    if (curUnitCom.Is(UnitTypes.Pawn))
                                    {
                                        if (envrC_0.Have(EnvirTypes.AdultForest))
                                        {
                                            InventResC.AddAmountRes(ownUnitC_0.Owner, ResourceTypes.Wood);
                                            envrC_0.TakeAmountRes(EnvirTypes.AdultForest);

                                            if (envrC_0.HaveRes(EnvirTypes.AdultForest))
                                            {
                                                if (builC_0.Is(BuildTypes.Camp) || !builC_0.HaveBuild)
                                                {
                                                    builC_0.SetBuild(BuildTypes.Woodcutter);
                                                    ownBuilC_0.SetOwner(ownUnitC_0.Owner);
                                                    WhereBuildsC.Add(ownUnitC_0.Owner, BuildTypes.Woodcutter, idx_0);
                                                }
                                                else if (builC_0.Is(BuildTypes.Woodcutter))
                                                {

                                                }
                                                else
                                                {
                                                    condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                            else
                                            {
                                                builC_0.NoneBuild();

                                                envrC_0.Reset(EnvirTypes.AdultForest);
                                                WhereEnvironmentC.Remove(EnvirTypes.AdultForest, idx_0);
                                            }
                                        }

                                        else if (twUnitC.ToolWeapType == ToolWeaponTypes.Pick)
                                        {
                                            if (envrC_0.Have(EnvirTypes.Hill))
                                            {
                                                if (builC_0.HaveBuild)
                                                {
                                                    condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (envrC_0.HaveMaxRes(EnvirTypes.Hill))
                                                    {
                                                        condUnitC.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                    else
                                                    {
                                                        envrC_0.SetMaxAmountRes(EnvirTypes.Hill);
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
                                    curHpUnitC.AddHealHp(effUnitC, curUnitCom.UnitType);
                                    if (curHpUnitC.AmountHp > curHpUnitC.MaxHpUnit(effUnitC, curUnitCom.UnitType))
                                    {
                                        curHpUnitC.AmountHp = curHpUnitC.MaxHpUnit(effUnitC, curUnitCom.UnitType);
                                    }
                                }
                            }

                            else if (condUnitC.Is(CondUnitTypes.Protected))
                            {
                                if (curHpUnitC.HaveMaxHpUnit(effUnitC, curUnitCom.UnitType))
                                {
                                    if (curUnitCom.Is(UnitTypes.Pawn))
                                    {
                                        if (envrC_0.Have(EnvirTypes.AdultForest))
                                        {
                                            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                            {
                                                if (ownUnitC_0.Is(PlayerTypes.First))
                                                {
                                                    builC_0.SetBuild(BuildTypes.Camp);
                                                    ownBuilC_0.SetOwner(ownUnitC_0.Owner);
                                                    WhereBuildsC.Add(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
                                                }
                                            }
                                            else
                                            {
                                                builC_0.SetBuild(BuildTypes.Camp);
                                                ownBuilC_0.SetOwner(ownUnitC_0.Owner);
                                                WhereBuildsC.Add(ownUnitC_0.Owner, builC_0.BuildType, idx_0);
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

                    curStepUnitC.SetMaxSteps(effUnitC, curUnitCom.UnitType);
                }

                else
                {
                    if (builC_0.Is(BuildTypes.Camp)) builC_0.NoneBuild();
                }

                if (builC_0.HaveBuild)
                {
                    if (builC_0.Is(BuildTypes.Farm))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Farm);

                        envrC_0.TakeAmountRes(EnvirTypes.Fertilizer, minus);
                        InventResC.AddAmountRes(ownUnitC_0.Owner, ResourceTypes.Food, minus);

                        if (!envrC_0.HaveRes(EnvirTypes.Fertilizer))
                        {
                            envrC_0.Reset(EnvirTypes.Fertilizer);
                            WhereEnvironmentC.Remove(EnvirTypes.Fertilizer, idx_0);

                            builC_0.NoneBuild();
                        }
                    }

                    else if (builC_0.Is(BuildTypes.Woodcutter))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Woodcutter);

                        envrC_0.TakeAmountRes(EnvirTypes.AdultForest, minus);
                        InventResC.AddAmountRes(ownUnitC_0.Owner, ResourceTypes.Wood, minus);

                        if (!envrC_0.HaveRes(EnvirTypes.AdultForest))
                        {
                            envrC_0.Reset(EnvirTypes.AdultForest);
                            WhereEnvironmentC.Remove(EnvirTypes.AdultForest, idx_0);

                            SpawnNewSeed(idx_0);

                            builC_0.NoneBuild();

                            if (curFireC.HaveFire)
                            {
                                curFireC.HaveFire = false;
                            }
                        }
                    }

                    else if (builC_0.Is(BuildTypes.Mine))
                    {
                        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Mine);

                        envrC_0.TakeAmountRes(EnvirTypes.Hill, minus);
                        InventResC.AddAmountRes(ownUnitC_0.Owner, ResourceTypes.Ore, minus);

                        if (!envrC_0.HaveRes(EnvirTypes.Hill))
                        {
                            builC_0.NoneBuild();
                        }
                    }
                }

                if (curCellDatC.IsActiveCell)
                {
                    if (envrC_0.Have(EnvirTypes.AdultForest))
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
                if (InventResC.AmountRes(playerType, ResourceTypes.Food) < 0)
                {
                    InventResC.Set(playerType, ResourceTypes.Food, 0);

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
                                    curUnitDatC.NoneUnit();


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
                        if (!curBuildDatCom.Is(BuildTypes.Mine))
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