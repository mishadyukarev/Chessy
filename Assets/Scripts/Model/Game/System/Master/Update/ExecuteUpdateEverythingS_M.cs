using Chessy.Common;
using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Game.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModel
    {
        internal ExecuteUpdateEverythingMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void ExecuteUpdateEverythingM()
        {
            _eMG.MotionsC.Motions++;
            _eMG.WeatherE.SunSideTC.ToggleNext();

            FireUpdate();
            TryGivePeople();
            TryShiftCloundsOrChangeDirection();
            TryChangeDirectionOfWindRandomly();
            TryPoorWaterWithClounds();
            TryGiveWaterToUnitsAroundRainy();
            TryShiftWolf();
            FeedUnits();
            TryGiveHealthToBots();

            TryGiveWaterAroundRiverToCells();
            DryWaterOnCells();
            TryExtractFoodWithFarm();
            TryExtractWoodWithWoodcutter();
            GiveHealthToUnitsWithRelaxCondition();
            TakeWaterUnits();
            TryTakeHealthToUnitWithThirsty();
            TryFireAroundHellGod();
            ToggleConditionUnitsIfTheresFire();
            TrySetDefendConditionUnits();
            TryExtractForestWithPawn();
            TryExtractHillsWithPawns();
            TryChangeRelaxConditionPawns();
            GiveFoodAfterUpdate();
            TryExecuteHungry();
            TrySpawnWolf();
            TryActiveGodsUniqueAbilityEveryUpdate();
            TrySkipLessonWithRiver();
            TryExecuteAI();
            RefreshStepsAll();

            TryExecuteTruce();
        }

        void TryActiveGodsUniqueAbilityEveryUpdate()
        {
            if (!_eMG.LessonTC.HaveLesson)
            {
                if (_eMG.MotionsC.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                        _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                        if (!_eMG.IsBorder(cell_0))
                        {
                            if (_eMG.UnitTC(cell_0).HaveUnit)
                            {
                                if (_eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy))
                                {
                                    if (_eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            _eMG.FrozenArrawEffectC(cell_0).Shoots++;
                                        }
                                        else
                                        {
                                            _eMG.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                        }
                                    }
                                    else
                                    {
                                        _eMG.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                            }
                            else
                            {
                                if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (!_eMG.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (_eMG.PlayerInfoE(playerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                                            {
                                                _sMG.SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

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
        void TryShiftCloundsOrChangeDirection()
        {
            for (var i = 0; i < _eMG.WeatherE.WindC.Speed; i++)
            {
                var cell = _eMG.WeatherE.CloudC.Center;
                var xy_next = _eMG.AroundCellsE(cell).AroundCellE(_eMG.WeatherE.WindC.DirectT).XyC.Xy;
                var idx_next = _eMG.AroundCellsE(cell).IdxCell(_eMG.WeatherE.WindC.DirectT);

                bool isBorder = false;

                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        _eMG.WeatherE.CloudC.Center = _eMG.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = _eMG.WeatherE.WindC.DirectT;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        _eMG.WeatherE.WindC.DirectT = (DirectTypes)newDirInt;

                        isBorder = true;

                        break;
                    }
                }

                if (isBorder) break;


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    _eMG.HealthTrail(idx_next).Health(dirT) = 0;
                }
            }
        }
        void TryGivePeople()
        {
            if (_eMG.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
                }
            }
        }
        void TryExecuteTruce()
        {
            var amountAdultForest = 0;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !_eMG.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !_eMG.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                _sMG.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                _sMG.TruceS.ExecuteTruce();
            }
        }
        void TrySkipLessonWithRiver()
        {

            if (_eMG.LessonT == LessonTypes.Install3WarriorsNextToTheRiver)
            {
                var amountUnitsNearRiverForLesson = 0;

                for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
                {
                    if (_eMG.UnitT(cellIdx0) == UnitTypes.Pawn && _eMG.UnitPlayerT(cellIdx0) == PlayerTypes.First && _eMG.RiverTC(cellIdx0).HaveRiverNear)
                    {
                        amountUnitsNearRiverForLesson++;
                    }
                }

                if (amountUnitsNearRiverForLesson >= 3)
                {
                    _eMG.LessonTC.SetNextLesson();
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) _eMG.WeatherE.WindC.Speed = UnityEngine.Random.Range(1, 4);
        }
        void TryPoorWaterWithClounds()
        {
            var cell_0 = _eMG.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = _eMG.AroundCellsE(cell_0).IdxCell(dirT);

                if (!_eMG.MountainC(idx_1).HaveAnyResources)
                {
                    _eMG.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }
        }
        void TrySpawnWolf()
        {
            var haveCamel = false;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                var cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

                if (!_eMG.IsBorder(cell_0))
                {
                    if (!_eMG.UnitTC(cell_0).HaveUnit && !_eMG.MountainC(cell_0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in _eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            if (_eMG.UnitTC(cell_1).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            _sMG.SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
        void TryShiftWolf()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = _eMG.AroundCellsE(cellIdxCurrent).IdxCell((DirectTypes)randDir);

                        if (!_eMG.IsBorder(idx_1) && !_eMG.MountainC(idx_1).HaveAnyResources
                            && !_eMG.UnitTC(idx_1).HaveUnit)
                        {
                            _sMG.UnitSs.CopyUnitFromTo(cellIdxCurrent, idx_1);

                            _sMG.UnitSs.ClearUnit(cellIdxCurrent);
                        }
                    }
                }
            }
        }
        void FeedUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    if (!_eMG.LessonTC.HaveLesson || _eMG.LessonT >= LessonTypes.Build3Farms)
                    {
                        if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn))
                            _eMG.ResourcesC(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
                    }
                }
            }
        }
        void TryGiveHealthToBots()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_eMG.UnitPlayerTC(cellIdxCurrent).Is(PlayerTypes.Second))
                        {
                            _eMG.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                        }
                    }
                }
            }
        }
        void RefreshStepsAll()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.StepUnitC(cellIdxCurrent).Steps = StepValues.MAX;
            }
        }
        void TryGiveWaterAroundRiverToCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.RiverTC(cellIdxCurrent).HaveRiverNear)
                {
                    if (!_eMG.MountainC(cellIdxCurrent).HaveAnyResources)
                    {
                        _eMG.FertilizeC(cellIdxCurrent).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }
        }
        void DryWaterOnCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.FertilizeC(cellIdxCurrent).HaveAnyResources)
                {
                    _eMG.FertilizeC(cellIdxCurrent).Resources -= EnvironmentValues.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }
            }
        }
        void TryExtractFoodWithFarm()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.FarmExtractFertilizeC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _eMG.FarmExtractFertilizeC(cellIdxCurrent).Resources;

                    _eMG.ResourcesC(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT, ResourceTypes.Food).Resources += extract;
                    _eMG.FertilizeC(cellIdxCurrent).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
        void TryExtractWoodWithWoodcutter()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.WoodcutterExtractC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _eMG.WoodcutterExtractC(cellIdxCurrent).Resources;

                    _eMG.ResourcesC(_eMG.BuildingPlayerTC(cellIdxCurrent).PlayerT, ResourceTypes.Wood).Resources += extract;
                    _sMG.TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (!_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        _eMG.BuildingTC(cellIdxCurrent).BuildingT = BuildingTypes.None;

                        if (_eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
        void GiveHealthToUnitsWithRelaxCondition()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitConditionTC(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                {
                    _eMG.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                }
            }
        }
        void TakeWaterUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit && !_eMG.UnitTC(cellIdxCurrent).IsAnimal)
                {
                    if (!_eMG.RiverTC(cellIdxCurrent).HaveRiverNear)
                    {
                        if (!_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline) || 
                            _eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline) && 
                            _eMG.UnitPlayerT(cellIdxCurrent) == PlayerTypes.First && 
                            !_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.King, UnitTypes.Snowy) &&
                            _eMG.LessonT >= LessonTypes.Install3WarriorsNextToTheRiver)
                        {
                            _eMG.WaterUnitC(cellIdxCurrent).Water -= WaterValues.NeedWaterForThirsty(_eMG.UnitT(cellIdxCurrent));
                        }
                    }
                }
            }
        }
        void TryTakeHealthToUnitWithThirsty()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit && !_eMG.UnitTC(cellIdxCurrent).IsAnimal)
                {
                    if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline) && _eMG.UnitPlayerT(cellIdxCurrent) == PlayerTypes.First)
                    {
                        if (_eMG.WaterUnitC(cellIdxCurrent).Water <= 0)
                        {
                            var percent = HpValues.ThirstyPercent(_eMG.UnitTC(cellIdxCurrent).UnitT);

                            _sMG.UnitSs.Attack(HpValues.MAX * percent, _eMG.UnitPlayerTC(cellIdxCurrent).PlayerT.NextPlayer(), cellIdxCurrent);
                        }
                    }
                }
            }
        }
        void TryFireAroundHellGod()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in _eMG.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_eMG.AdultForestC(cellE).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                _eMG.HaveFire(cellE) = true;
                            }
                        }
                    }

                    if (_eMG.RiverTC(cellIdxCurrent).HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (_eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).CellsAround.Any(cell => cell == cellIdxCurrent))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in _eMG.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_eMG.BuildingTC(cellE).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }
        }
        void ToggleConditionUnitsIfTheresFire()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    if (_eMG.HaveFire(cellIdxCurrent))
                    {
                        _eMG.UnitConditionTC(cellIdxCurrent).Condition = ConditionUnitTypes.None;
                    }
                }
            }
        }
        void TrySetDefendConditionUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    if (!_eMG.UnitConditionTC(cellIdxCurrent).HaveCondition)
                    {
                        if (_eMG.StepUnit(cellIdxCurrent) >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            _eMG.UnitConditionTC(cellIdxCurrent).Condition = ConditionUnitTypes.Protected;
                            _eMG.StepUnitC(cellIdxCurrent).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        }
                    }
                }
            }
        }
        void TryExtractForestWithPawn()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _eMG.PawnExtractAdultForestC(cellIdxCurrent).Resources;

                    _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    _sMG.TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        if (_eMG.BuildingTC(cellIdxCurrent).Is(BuildingTypes.Camp) || !_eMG.BuildingTC(cellIdxCurrent).HaveBuilding)
                        {
                            _sMG.BuildingSs.Build(BuildingTypes.Woodcutter, LevelTypes.First, _eMG.UnitPlayerTC(cellIdxCurrent).PlayerT, 1, cellIdxCurrent);

                            if (_eMG.LessonT == LessonTypes.RelaxExtractPawn) _eMG.LessonTC.SetNextLesson();
                        }

                        else if (!_eMG.BuildingTC(cellIdxCurrent).Is(BuildingTypes.Woodcutter))
                        {
                            _eMG.UnitConditionTC(cellIdxCurrent).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        _sMG.BuildingSs.Clear(cellIdxCurrent);

                        if (_eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
        void TryExtractHillsWithPawns()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_eMG.PawnExtractHillC(cellIdxCurrent).HaveAnyResources && !_eMG.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    var extract = _eMG.PawnExtractHillC(cellIdxCurrent).Resources;

                    _eMG.HillC(cellIdxCurrent).Resources -= extract;
                    _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (_eMG.LessonTC.Is(LessonTypes.ExtractHill))
                    {
                        _eMG.LessonTC.SetNextLesson();

                        if (_eMG.IsSelectedCity)
                        {
                            _eMG.LessonTC.SetNextLesson();
                        }
                    }
                }
            }
        }
        void TryChangeRelaxConditionPawns()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (!_eMG.PawnExtractHillC(cellIdxCurrent).HaveAnyResources && !_eMG.PawnExtractAdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    if (_eMG.UnitConditionTC(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                        && _eMG.HpUnitC(cellIdxCurrent).Health >= HpValues.MAX)
                    {
                        _eMG.UnitConditionTC(cellIdxCurrent).Condition = ConditionUnitTypes.Protected;
                    }
                }
            }
        }
        void GiveFoodAfterUpdate()
        {
            if (!_eMG.LessonTC.HaveLesson || _eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _eMG.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;
                }
            }
        }
        void TryExecuteHungry()
        {
            if (!_eMG.LessonTC.HaveLesson || _eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (_eMG.PlayerInfoE(playerT).ResourcesC(res).Resources < 0)
                    {
                        _eMG.PlayerInfoE(playerT).ResourcesC(res).Resources = 0;

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
                        {
                            if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn) && _eMG.UnitPlayerTC(cellIdxCurrent).Is(playerT))
                            {
                                _sMG.UnitSs.KillUnit(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT.NextPlayer(), cellIdxCurrent);
                                _sMG.UnitSs.ClearUnit(cellIdxCurrent);

                                break;
                            }
                        }
                    }
                }
            }
        }
        void TryExecuteAI()
        {
            //if (!_eMG.LessonTC.HaveLesson)
            //{
            //    _aIBotS.Execute();
            //}
        }
    }
}