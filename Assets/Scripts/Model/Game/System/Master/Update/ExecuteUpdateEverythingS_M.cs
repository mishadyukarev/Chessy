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
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void ExecuteUpdateEverythingM()
        {
            FireUpdate();

            _eMG.MotionsC.Motions++;
            _eMG.WeatherE.SunSideTC.ToggleNext();


            if (_eMG.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
                }
            }


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

            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) _eMG.WeatherE.WindC.Speed = UnityEngine.Random.Range(1, 4);



            var cell_0 = _eMG.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = _eMG.AroundCellsE(cell_0).IdxCell(dirT);

                if (!_eMG.MountainC(idx_1).HaveAnyResources)
                {
                    _eMG.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }





            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (_eMG.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!_eMG.LessonTC.HaveLesson)
                        {
                            TryGiveWaterToUnitsAroundRainyM(cell_0);
                        }
                    }

                    if (_eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = _eMG.AroundCellsE(cell_0).IdxCell((DirectTypes)randDir);

                        if (!_eMG.IsBorder(idx_1) && !_eMG.MountainC(idx_1).HaveAnyResources
                            && !_eMG.UnitTC(idx_1).HaveUnit)
                        {
                            UnitSs.CopyUnitFromTo(cell_0, idx_1);

                            UnitSs.ClearUnit(cell_0);
                        }
                    }

                    if (!_eMG.LessonTC.HaveLesson || _eMG.LessonT >= LessonTypes.Build3Farms)
                    {
                        if (_eMG.UnitTC(cell_0).Is(UnitTypes.Pawn)) 
                            _eMG.ResourcesC(_eMG.UnitPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
                    }

                   


                    if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                        {
                            _eMG.HpUnitC(cell_0).Health = HpValues.MAX;
                        }

                        if (_eMG.LessonTC.HaveLesson)
                        {
                            if (_eMG.UnitTC(cell_0).Is(UnitTypes.King, UnitTypes.Snowy))
                            {
                                _eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                            }

                            if (_eMG.LessonT < LessonTypes.Install3WarriorsNextToTheRiver)
                            {
                                if (_eMG.UnitT(cell_0) == UnitTypes.Pawn)
                                {
                                    _eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                                }
                           }
                        }
                    }


                    if (_eMG.HaveFire(cell_0))
                    {
                        _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (_eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                            {
                                if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (_eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !_eMG.BuildingTC(cell_0).HaveBuilding)
                                    {
                                        if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                                        {
                                            if (_eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                                            {
                                                if (_eMG.BuildingsInfo(_eMG.UnitPlayerTC(cell_0).PlayerT, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (_eMG.BuildingsInfo(_eMG.UnitPlayerTC(cell_0).PlayerT, _eMG.BuildingLevelTC(cell_0).LevelT, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {

                        }

                        else
                        {
                            if (_eMG.StepUnitC(cell_0).HaveAnySteps)
                            {
                                _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    _eMG.StepUnitC(cell_0).Steps = StepValues.MAX;
                }






                if (_eMG.RiverTC(cell_0).HaveRiverNear)
                {
                    if (!_eMG.MountainC(cell_0).HaveAnyResources)
                    {
                        _eMG.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                if (_eMG.FertilizeC(cell_0).HaveAnyResources)
                {
                    _eMG.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }

                if (_eMG.FarmExtractFertilizeC(cell_0).HaveAnyResources)
                {
                    var extract = _eMG.FarmExtractFertilizeC(cell_0).Resources;

                    _eMG.ResourcesC(_eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources += extract;
                    _eMG.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }

                if (_eMG.WoodcutterExtractC(cell_0).HaveAnyResources)
                {
                    var extract = _eMG.WoodcutterExtractC(cell_0).Resources;

                    _eMG.ResourcesC(_eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Wood).Resources += extract;
                    TryTakeAdultForestResourcesM(extract, cell_0);

                    if (!_eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;

                        if (_eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }




                if (_eMG.UnitTC(cell_0).HaveUnit && !_eMG.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (_eMG.RiverTC(cell_0).HaveRiverNear)
                        {
                            _eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            _eMG.WaterUnitC(cell_0).Water -= WaterValues.NeedWaterForThirsty(_eMG.UnitT(cell_0));

                            if (_eMG.WaterUnitC(cell_0).Water <= 0)
                            {
                                var percent = HpValues.ThirstyPercent(_eMG.UnitTC(cell_0).UnitT);

                                UnitSs.Attack(HpValues.MAX * percent, _eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }
                }



                if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    _eMG.HpUnitC(cell_0).Health = HpValues.MAX;
                }

                if (_eMG.PawnExtractAdultForestC(cell_0).HaveAnyResources)
                {
                    var extract = _eMG.PawnExtractAdultForestC(cell_0).Resources;

                    _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    TryTakeAdultForestResourcesM(extract, cell_0);

                    if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (_eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !_eMG.BuildingTC(cell_0).HaveBuilding)
                        {
                            BuildingSs.Build(BuildingTypes.Woodcutter, LevelTypes.First, _eMG.UnitPlayerTC(cell_0).PlayerT, 1, cell_0);

                            if (_eMG.LessonT == LessonTypes.RelaxExtractPawn) _eMG.LessonTC.SetNextLesson();
                        }

                        else if (!_eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        BuildingSs.Clear(cell_0);

                        if (_eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }

                else if (_eMG.PawnExtractHillC(cell_0).HaveAnyResources)
                {
                    var extract = _eMG.PawnExtractHillC(cell_0).Resources;

                    _eMG.HillC(cell_0).Resources -= extract;
                    _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (_eMG.LessonTC.Is(LessonTypes.ExtractHill))
                    {
                        _eMG.LessonTC.SetNextLesson();

                        if (_eMG.IsSelectedCity)
                        {
                            _eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (!_eMG.HillC(cell_0).HaveAnyResources)
                    {

                        //else if (_eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        //{
                        //    _eMG.LessonTC.SetNextLesson();
                        //    _eMG.LessonTC.SetNextLesson();
                        //}
                    }
                }

                else if (_eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && _eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                {
                    _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }


                if (_eMG.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in _eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (_eMG.AdultForestC(cellE).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                _eMG.HaveFire(cellE) = true;
                            }
                        }
                    }

                    if (_eMG.RiverTC(cell_0).HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (_eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).CellsAround.Any(cell => cell == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in _eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (_eMG.BuildingTC(cellE).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }


            if (!_eMG.LessonTC.HaveLesson || _eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    var res = ResourceTypes.Food;

                    _eMG.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                    if (_eMG.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                    {
                        _eMG.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                        for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                        {
                            if (_eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && _eMG.UnitPlayerTC(cell_0).Is(player))
                            {
                                UnitSs.KillUnit(_eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                                UnitSs.ClearUnit(cell_0);

                                break;
                            }
                        }
                    }
                }
            }

            


            var haveCamel = false;

            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

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
                            SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }



            if (!_eMG.LessonTC.HaveLesson)
            {
                if (_eMG.MotionsC.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
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
                                                SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

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


            var amountAdultForest = 0;

            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !_eMG.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !_eMG.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                ExecuteTruce();
            }

            //if (!_eMG.LessonTC.HaveLesson)
            //{
            //    _aIBotS.Execute();
            //}


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
    }
}