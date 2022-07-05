using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        float _timeForUpdateWind;

        public void Update()
        {
            var timeDeltaTime = Time.deltaTime;

            _runs.ForEach((Action action) => action());


            _e.TimeForUpdateEverything += timeDeltaTime;

            if (_e.TimeForUpdateEverything >= 1)
            {
                _e.NeedUpdateView = true;


                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                        {
                            _e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInRelax++;
                        }
                        
                    }
                }

                TryExtractForestWithPawn();
                TryExtractWoodWithWoodcutter();




                _e.TimeForUpdateEverything = 0;
                GetDataCellsS.GetDataCells();
                SyncDataM();
            }


            _timeForUpdateWind += timeDeltaTime;

            if (_timeForUpdateWind >= 3)
            {
                TryShiftCloundsOrChangeDirection();
                TryPoorWaterToCellsWithClounds();
                _timeForUpdateWind = 0;
            }

            //TryGiveWaterToUnitsDuringLessons();
            TryGiveWaterToBotUnits();
            GiveWaterToUnitsNearWithRiver();
            TakeWaterUnits();
            TryTakeHealthToUnitWithThirsty();
            TryTakeHealthToUnitWithThirsty();



            TrySpawnWolf();
            TryShiftWolf();






            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var cell0 = cellIdxCurrent;
                var cell1 = _e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell;

                if (_e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell != 0)
                {
                    if (_e.UnitMainC(cell1).UnitT.HaveUnit())
                    {
                        _e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell = 0;
                    }
                    else
                    {
                        _e.UnitMainC(cellIdxCurrent).DistanceForShiftingOnOtherCell += Time.deltaTime;

                        if (_e.UnitMainC(cellIdxCurrent).DistanceForShiftingOnOtherCell >= _e.HowManyEnergyNeedForShiftingUnitC(cellIdxCurrent).HowManyEnergyNeedForShiftingToHere(_e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell))
                        {
                            ShiftUnitOnOtherCellM(cellIdxCurrent, _e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell);

                            _e.UnitMainC(cellIdxCurrent).IdxWhereNeedShiftUnitOnOtherCell = 0;
                            _e.UnitMainC(cellIdxCurrent).DistanceForShiftingOnOtherCell = 0;

                            //RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.ClickToTable);
                        }
                    }
                }
            }
        }

        void TryTakeHealthToUnitWithThirsty()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                {
                    if (_e.GameModeT.Is(GameModeTypes.TrainingOffline) && _e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.First)
                    {
                        if (_e.WaterUnitC(cellIdxCurrent).Water <= 0)
                        {
                            var percent = Time.deltaTime;// HpValues.ThirstyPercent(_e.UnitT(cellIdxCurrent));

                            AttackUnitOnCell(HpValues.MAX * percent, _e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                        }
                    }
                }
            }
        }
        void TryShiftCloundsOrChangeDirection()
        {
            for (var i = 0; i < _e.SpeedWind; i++)
            {
                var cell = _e.CenterCloudCellIdx;
                var xy_next = _e.AroundCellsE(cell).AroundCellE(_e.DirectWindT).XyC.Xy;
                var idx_next = _e.AroundCellsE(cell).IdxCell(_e.DirectWindT);

                bool isBorder = false;

                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        _e.CenterCloudCellIdx = _e.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = _e.DirectWindT;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        _e.DirectWindT = (DirectTypes)newDirInt;

                        isBorder = true;

                        break;
                    }
                }

                if (isBorder) break;


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    _e.HealthTrail(idx_next).Health(dirT) = 0;
                }
            }
        }
        void TryPoorWaterToCellsWithClounds()
        {
            var cell_0 = _e.CenterCloudCellIdx;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = _e.AroundCellsE(cell_0).IdxCell(dirT);

                if (!_e.MountainC(idx_1).HaveAnyResources)
                {
                    _e.WaterOnCellC(idx_1).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                }
            }
        }

        void TryExtractWoodWithWoodcutter()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;
                if (_e.BuildingOnCellT(cellIdxCurrent) != BuildingTypes.Woodcutter) continue;

                GetDataCellsS.GetWoodcutterExtractCells(cellIdxCurrent);

                var extract = _e.WoodcutterExtract(cellIdxCurrent);

                _e.ResourcesInInventoryC(_e.BuildingPlayerT(cellIdxCurrent)).Add(ResourceTypes.Wood, extract);
                TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    _e.SetBuildingOnCellT(cellIdxCurrent, BuildingTypes.None);

                    if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                    {
                        if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
                        }
                    }
                }
            }
        }


        #region Water

        void TakeWaterUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    var speed = 0.01f;

                    _e.WaterUnitC(cellIdxCurrent).Water -= Time.deltaTime * speed;// ValuesChessy.NeedWaterForThirsty(_e.UnitT(cellIdxCurrent));
                }
            }
        }
        void TryGiveWaterToUnitsDuringLessons()
        {
            if (_e.LessonT! >= LessonTypes.Install3WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }
        void TryGiveWaterToBotUnits()
        {
            if (_e.GameModeT == GameModeTypes.TrainingOffline)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.Second)
                        {
                            _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                        }
                    }
                }
            }
        }
        void GiveWaterToUnitsNearWithRiver()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                    {
                        _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }

        #endregion


        #region Wolf

        void TrySpawnWolf()
        {
            var haveCamel = false;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                var cell_0 = (byte)UnityEngine.Random.Range(0, IndexCellsValues.CELLS);

                if (!_e.IsBorder(cell_0))
                {
                    if (!_e.UnitT(cell_0).HaveUnit() && !_e.MountainC(cell_0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in _e.AroundCellsE(cell_0).CellsAround)
                        {
                            if (_e.UnitT(cell_1).HaveUnit())
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }

        float _timeForShiftWolf;
        void TryShiftWolf()
        {
            _timeForShiftWolf += Time.deltaTime;

            if (_timeForShiftWolf >= 5)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf))
                        {
                            var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                            var idx_1 = _e.AroundCellsE(cellIdxCurrent).IdxCell((DirectTypes)randDir);

                            if (!_e.IsBorder(idx_1) && !_e.MountainC(idx_1).HaveAnyResources
                                && !_e.UnitT(idx_1).HaveUnit())
                            {
                                UnitSs.CopyUnitFromTo(cellIdxCurrent, idx_1);

                                _e.UnitE(cellIdxCurrent).ClearEverything();
                            }
                        }
                    }
                }

                GetDataCellsS.GetDataCells();
                SyncDataM();

                _timeForShiftWolf = 0;
            }
        }

        #endregion

        void TryExtractForestWithPawn()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                {
                    var extract = _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractAdultForest;

                    _e.ResourcesInInventoryC(_e.UnitPlayerT(cellIdxCurrent)).Add(ResourceTypes.Wood, extract);
                    TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        if (!_e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Woodcutter))
                        {
                            if (_e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInRelax >= 10)
                            {
                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cellIdxCurrent), 1, cellIdxCurrent);

                                if (_e.LessonT == LessonTypes.RelaxExtractPawn) SetNextLesson();
                            }
                        }
                    }
                    else
                    {
                        //_e.ClearBuildingOnCell(cellIdxCurrent);

                        if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
    }
}