using Chessy.Common;
using Chessy.Model.Enum;
using Chessy.Model.Extensions;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Effect;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Model.Model.System
{
    static partial class ExecuteUpdateEverythingMS
    {
        internal static void ExecuteUpdateEverythingM(this EntitiesModel _e, in SystemsModel _s)
        {
            _e.Motions++;
            _e.WeatherE.SunSideT.ToggleNext();

            _e.TryPutOutFireWithClouds();
            _e.BurnAdultForest(_s);
            _e.FireUpdate(_s);

            _e.TryGivePeople();
            _e.TryShiftCloundsOrChangeDirection(_s);
            _e.TryChangeDirectionOfWindRandomly();
            _e.TryPoorWaterToCellsWithClounds();
            _e.TryShiftWolf(_s);
            _e.FeedUnits();
            _e.TryGiveHealthToBots();
            _e.TryGiveWaterAroundRiverToCells();
            _e.DryWaterOnCells();
            _e.TryExtractFoodWithFarm();
            _e.TryExtractWoodWithWoodcutter(_s);
            _e.GiveHealthToUnitsWithRelaxCondition();

            _e.TryGiveWaterToUnitsAroundRainy();
            _e.TryGiveWaterToUnitsDuringLessons();
            _e.TryGiveWaterToBotUnits();
            _e.GiveWaterToUnitsNearWithRiver();
            _e.TakeWaterUnits();

            _e.TryTakeHealthToUnitWithThirsty(_s);
            _e.TryFireAroundHellGod();
            _e.ToggleConditionUnitsIfTheresFire();
            _e.TrySetDefendConditionUnits();
            _e.TryExtractForestWithPawn(_s);
            _e.TryExtractHillsWithPawns();
            _e.TryChangeRelaxConditionPawns();
            _e.GiveFoodAfterUpdate();
            _e.TryExecuteHungry(_s);
            _e.TrySpawnWolf(_s);
            _e.TryActiveGodsUniqueAbilityEveryUpdate(_s);
            _e.TrySkipLessonWithRiver();
            _e.TryExecuteAI();
            _e.RefreshStepsAll();

            _e.TryExecuteTruce(_s);
        }

        static void TryActiveGodsUniqueAbilityEveryUpdate(this EntitiesModel _e, in SystemsModel _s)
        {
            if (!_e.LessonT.HaveLesson())
            {
                if (_e.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    _s.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (!_e.IsBorder(cell_0))
                        {
                            if (_e.UnitT(cell_0).HaveUnit())
                            {
                                if (_e.PlayerInfoE(_e.UnitPlayerT(cell_0)).GodInfoE.UnitT.Is(UnitTypes.Snowy))
                                {
                                    if (_e.UnitT(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (_e.MainToolWeaponT(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            _e.FrozenArrawEffectC(cell_0).Shoots++;
                                        }
                                        else
                                        {
                                            _e.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                        }
                                    }
                                    else
                                    {
                                        _e.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                            }
                            else
                            {
                                if (_e.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (!_e.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (_e.PlayerInfoE(playerT).GodInfoE.UnitT.Is(UnitTypes.Elfemale))
                                            {
                                                _s.SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static void TryShiftCloundsOrChangeDirection(this EntitiesModel _e, in SystemsModel _s)
        {
            for (var i = 0; i < _e.WeatherE.WindC.Speed; i++)
            {
                var cell = _e.WeatherE.CellIdxCenterCloud;
                var xy_next = _e.AroundCellsE(cell).AroundCellE(_e.WeatherE.WindC.DirectT).XyC.Xy();
                var idx_next = _e.AroundCellsE(cell).IdxCell(_e.WeatherE.WindC.DirectT);

                bool isBorder = false;

                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        _e.WeatherE.CellIdxCenterCloud = _e.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = _e.WeatherE.WindC.DirectT;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        _e.WeatherE.WindC.DirectT = (DirectTypes)newDirInt;

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
        static void TryGivePeople(this EntitiesModel _e)
        {
            if (_e.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
                }
            }
        }
        static void TryExecuteTruce(this EntitiesModel _e, in SystemsModel _s)
        {
            var amountAdultForest = 0;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !_e.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !_e.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                _s.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                _e.ExecuteTruce(_s);
            }
        }
        static void TrySkipLessonWithRiver(this EntitiesModel _e)
        {

            if (_e.LessonT == LessonTypes.Install3WarriorsNextToTheRiver)
            {
                var amountUnitsNearRiverForLesson = 0;

                for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
                {
                    if (_e.UnitT(cellIdx0) == UnitTypes.Pawn && _e.UnitPlayerT(cellIdx0) == PlayerTypes.First && _e.RiverT(cellIdx0).HaveRiverNear())
                    {
                        amountUnitsNearRiverForLesson++;
                    }
                }

                if (amountUnitsNearRiverForLesson >= 3)
                {
                    _e.LessonT.SetNextLesson();
                }
            }
        }
        static void TryChangeDirectionOfWindRandomly(this EntitiesModel _e)
        {
            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) _e.WeatherE.WindC.Speed = UnityEngine.Random.Range(1, 4);
        }
        static void TryPoorWaterToCellsWithClounds(this EntitiesModel _e)
        {
            var cell_0 = _e.WeatherE.CellIdxCenterCloud;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = _e.AroundCellsE(cell_0).IdxCell(dirT);

                if (!_e.MountainC(idx_1).HaveAnyResources)
                {
                    _e.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }
        }
        static void TrySpawnWolf(this EntitiesModel _e, in SystemsModel _s)
        {
            var haveCamel = false;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                var cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

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
                            _s.SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
        static void TryShiftWolf(this EntitiesModel _e, in SystemsModel _s)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
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
                            _s.UnitSs.CopyUnitFromTo(cellIdxCurrent, idx_1);

                            _e.UnitEs(cellIdxCurrent).ClearEverything();
                        }
                    }
                }
            }
        }
        static void FeedUnits(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
                    {
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                            _e.ResourcesC(_e.UnitPlayerT(cellIdxCurrent), ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
                    }
                }
            }
        }
        static void TryGiveHealthToBots(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.Second))
                        {
                            _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                        }
                    }
                }
            }
        }
        static void RefreshStepsAll(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.StepUnitC(cellIdxCurrent).Steps = StepValues.MAX;
            }
        }
        static void TryGiveWaterAroundRiverToCells(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                {
                    if (!_e.MountainC(cellIdxCurrent).HaveAnyResources)
                    {
                        _e.FertilizeC(cellIdxCurrent).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }
        }
        static void DryWaterOnCells(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.FertilizeC(cellIdxCurrent).HaveAnyResources)
                {
                    _e.FertilizeC(cellIdxCurrent).Resources -= EnvironmentValues.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }
            }
        }
        static void TryExtractFoodWithFarm(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.FarmExtractFertilizeC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _e.FarmExtractFertilizeC(cellIdxCurrent).Resources;

                    _e.ResourcesC(_e.BuildingPlayerT(cellIdxCurrent), ResourceTypes.Food).Resources += extract;
                    _e.FertilizeC(cellIdxCurrent).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
        static void TryExtractWoodWithWoodcutter(this EntitiesModel _e, in SystemsModel _s)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.WoodcutterExtractC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _e.WoodcutterExtractC(cellIdxCurrent).Resources;

                    _e.ResourcesC(_e.BuildingPlayerT(cellIdxCurrent), ResourceTypes.Wood).Resources += extract;
                    _s.TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        _e.SetBuildingOnCellT(cellIdxCurrent, BuildingTypes.None);

                        if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
        static void GiveHealthToUnitsWithRelaxCondition(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                {
                    _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                }
            }
        }


        #region WaterUnits

        static void TakeWaterUnits(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    _e.WaterUnitC(cellIdxCurrent).Water -= WaterValues.NeedWaterForThirsty(_e.UnitT(cellIdxCurrent));
                }
            }
        }
        static void TryGiveWaterToUnitsDuringLessons(this EntitiesModel _e)
        {
            if (_e.LessonT! >= LessonTypes.Install3WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        _e.WaterUnitC(cellIdxCurrent).Water = WaterValues.MAX;
                    }
                }
            }
        }
        static void TryGiveWaterToBotUnits(this EntitiesModel _e)
        {
            if (_e.GameModeT == GameModeTypes.TrainingOffline)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.Second)
                        {
                            _e.WaterUnitC(cellIdxCurrent).Water = WaterValues.MAX;
                        }
                    }
                }
            }
        }
        static void GiveWaterToUnitsNearWithRiver(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                    {
                        _e.WaterUnitC(cellIdxCurrent).Water = WaterValues.MAX;
                    }
                }
            }
        }

        #endregion


        static void TryTakeHealthToUnitWithThirsty(this EntitiesModel _e, in SystemsModel _s)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                {
                    if (_e.GameModeT.Is(GameModeTypes.TrainingOffline) && _e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.First)
                    {
                        if (_e.WaterUnitC(cellIdxCurrent).Water <= 0)
                        {
                            var percent = HpValues.ThirstyPercent(_e.UnitT(cellIdxCurrent));

                            _s.UnitSs.Attack(HpValues.MAX * percent, _e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                        }
                    }
                }
            }
        }
        static void TryFireAroundHellGod(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_e.AdultForestC(cellE).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                _e.HaveFire(cellE) = true;
                            }
                        }
                    }

                    if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (_e.AroundCellsE(_e.WeatherE.CellIdxCenterCloud).CellsAround.Any(cell => cell == cellIdxCurrent))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_e.BuildingOnCellT(cellE).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }
        }
        static void ToggleConditionUnitsIfTheresFire(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.HaveFire(cellIdxCurrent))
                    {
                        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.None);
                    }
                }
            }
        }
        static void TrySetDefendConditionUnits(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (!_e.UnitConditionT(cellIdxCurrent).HaveCondition())
                    {
                        if (_e.StepUnit(cellIdxCurrent) >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                            _e.StepUnitC(cellIdxCurrent).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        }
                    }
                }
            }
        }
        static void TryExtractForestWithPawn(this EntitiesModel _e, in SystemsModel _s)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _e.PawnExtractAdultForestC(cellIdxCurrent).Resources;

                    _e.PlayerInfoE(_e.UnitPlayerT(cellIdxCurrent)).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    _s.TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        if (_e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Camp) || !_e.HaveBuildingOnCell(cellIdxCurrent))
                        {
                            _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cellIdxCurrent), 1, cellIdxCurrent);

                            if (_e.LessonT == LessonTypes.RelaxExtractPawn) _e.LessonT.SetNextLesson();
                        }

                        else if (!_e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Woodcutter))
                        {
                            _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                        }
                    }
                    else
                    {
                        _e.Clear(cellIdxCurrent);

                        if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
        static void TryExtractHillsWithPawns(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.PawnExtractHillC(cellIdxCurrent).HaveAnyResources && !_e.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _e.PawnExtractHillC(cellIdxCurrent).Resources;

                    _e.HillC(cellIdxCurrent).Resources -= extract;
                    _e.PlayerInfoE(_e.UnitPlayerT(cellIdxCurrent)).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (_e.LessonT.Is(LessonTypes.ExtractHill))
                    {
                        _e.LessonT.SetNextLesson();

                        if (_e.IsSelectedCity)
                        {
                            _e.LessonT.SetNextLesson();
                        }
                    }
                }
            }
        }
        static void TryChangeRelaxConditionPawns(this EntitiesModel _e)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (!_e.PawnExtractHillC(cellIdxCurrent).HaveAnyResources && !_e.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                        && _e.HpUnitC(cellIdxCurrent).Health >= HpValues.MAX)
                    {
                        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                    }
                }
            }
        }
        static void GiveFoodAfterUpdate(this EntitiesModel _e)
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _e.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;
                }
            }
        }
        static void TryExecuteHungry(this EntitiesModel _e, in SystemsModel _s)
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (_e.PlayerInfoE(playerT).ResourcesC(res).Resources < 0)
                    {
                        _e.PlayerInfoE(playerT).ResourcesC(res).Resources = 0;

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
                        {
                            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.UnitPlayerT(cellIdxCurrent).Is(playerT))
                            {
                                _s.UnitSs.KillUnit(_e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                                _e.UnitEs(cellIdxCurrent).ClearEverything();

                                break;
                            }
                        }
                    }
                }
            }
        }
        static void TryExecuteAI(this EntitiesModel _e)
        {
            //if (!_eMG.LessonTC.HaveLesson)
            //{
            //    _aIBotS.Execute();
            //}
        }
    }
}