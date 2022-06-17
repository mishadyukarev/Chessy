using Chessy.Common;
using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Game.Model.System.Master
{
    sealed class ExecuteUpdateEverythingS_M : SystemModel
    {
        readonly TruceS_M _truceS;
        readonly FireUpdateS_M _fireUpdateS;
        readonly ExecuteAIBotLogicAfterUpdateS_M _aIBotS;

        internal ExecuteUpdateEverythingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _truceS = new TruceS_M(sMG, eMG);
            _fireUpdateS = new FireUpdateS_M(sMG, eMG);
            _aIBotS = new ExecuteAIBotLogicAfterUpdateS_M(sMG, eMG);
        }

        internal void Run()
        {
            _fireUpdateS.Run();


            

            eMG.MotionsC.Motions++;
            eMG.WeatherE.SunSideTC.ToggleNext();


            if (eMG.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
                }
            }


            for (var i = 0; i < eMG.WeatherE.WindC.Speed; i++)
            {
                var cell = eMG.WeatherE.CloudC.Center;
                var xy_next = eMG.AroundCellsE(cell).AroundCellE(eMG.WeatherE.WindC.DirectT).XyC.Xy;
                var idx_next = eMG.AroundCellsE(cell).IdxCell(eMG.WeatherE.WindC.DirectT);

                bool isBorder = false;

                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        eMG.WeatherE.CloudC.Center = eMG.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = eMG.WeatherE.WindC.DirectT;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        eMG.WeatherE.WindC.DirectT = (DirectTypes)newDirInt;

                        isBorder = true;

                        break;
                    }
                }

                if (isBorder) break;


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMG.HealthTrail(idx_next).Health(dirT) = 0;
                }
            }

            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) eMG.WeatherE.WindC.Speed = UnityEngine.Random.Range(1, 4);



            var cell_0 = eMG.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                if (!eMG.MountainC(idx_1).HaveAnyResources)
                {
                    eMG.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }





            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (eMG.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!eMG.LessonTC.HaveLesson)
                        {
                            sMG.MasterSs.RainyGiveWaterToUnitsAroundS_M.TryGive(cell_0);
                        }
                    }

                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = eMG.AroundCellsE(cell_0).IdxCell((DirectTypes)randDir);

                        if (!eMG.IsBorder(idx_1) && !eMG.MountainC(idx_1).HaveAnyResources
                            && !eMG.UnitTC(idx_1).HaveUnit)
                        {
                            sMG.UnitSs.CopyUnitFromToS.Copy(cell_0, idx_1);

                            sMG.UnitSs.ClearUnit(cell_0);
                        }
                    }

                    if (!eMG.LessonTC.HaveLesson || eMG.LessonT >= LessonTypes.Build3Farms)
                    {
                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn)) 
                            eMG.ResourcesC(eMG.UnitPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
                    }

                   


                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                        {
                            eMG.HpUnitC(cell_0).Health = HpValues.MAX;
                        }

                        if (eMG.LessonTC.HaveLesson)
                        {
                            if (eMG.UnitTC(cell_0).Is(UnitTypes.King, UnitTypes.Snowy))
                            {
                                eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                            }

                            if (eMG.LessonT < LessonTypes.Install3WarriorsNextToTheRiver)
                            {
                                if (eMG.UnitT(cell_0) == UnitTypes.Pawn)
                                {
                                    eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                                }
                           }
                        }
                    }


                    if (eMG.HaveFire(cell_0))
                    {
                        eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                            {
                                if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !eMG.BuildingTC(cell_0).HaveBuilding)
                                    {
                                        if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                                        {
                                            if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                                            {
                                                if (eMG.BuildingsInfo(eMG.UnitPlayerTC(cell_0).PlayerT, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (eMG.BuildingsInfo(eMG.UnitPlayerTC(cell_0).PlayerT, eMG.BuildingLevelTC(cell_0).LevelT, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {

                        }

                        else
                        {
                            if (eMG.StepUnitC(cell_0).HaveAnySteps)
                            {
                                eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    eMG.StepUnitC(cell_0).Steps = StepValues.MAX;
                }






                if (eMG.RiverTC(cell_0).HaveRiverNear)
                {
                    if (!eMG.MountainC(cell_0).HaveAnyResources)
                    {
                        eMG.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                if (eMG.FertilizeC(cell_0).HaveAnyResources)
                {
                    eMG.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }

                if (eMG.FarmExtractFertilizeC(cell_0).HaveAnyResources)
                {
                    var extract = eMG.FarmExtractFertilizeC(cell_0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources += extract;
                    eMG.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }

                if (eMG.WoodcutterExtractC(cell_0).HaveAnyResources)
                {
                    var extract = eMG.WoodcutterExtractC(cell_0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Wood).Resources += extract;
                    sMG.MasterSs.TryTakeAdultForestResourcesS.TryTake(extract, cell_0);

                    if (!eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;

                        if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }




                if (eMG.UnitTC(cell_0).HaveUnit && !eMG.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (eMG.RiverTC(cell_0).HaveRiverNear)
                        {
                            eMG.WaterUnitC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            eMG.WaterUnitC(cell_0).Water -= WaterValues.NeedWaterForThirsty(eMG.UnitT(cell_0));

                            if (eMG.WaterUnitC(cell_0).Water <= 0)
                            {
                                var percent = HpValues.ThirstyPercent(eMG.UnitTC(cell_0).UnitT);

                                sMG.UnitSs.AttackUnitS.Attack(HpValues.MAX * percent, eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }
                }



                if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    eMG.HpUnitC(cell_0).Health = HpValues.MAX;
                }

                if (eMG.PawnExtractAdultForestC(cell_0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractAdultForestC(cell_0).Resources;

                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    sMG.MasterSs.TryTakeAdultForestResourcesS.TryTake(extract, cell_0);

                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !eMG.BuildingTC(cell_0).HaveBuilding)
                        {
                            sMG.BuildingSs.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMG.UnitPlayerTC(cell_0).PlayerT, 1, cell_0);

                            if (eMG.LessonT == LessonTypes.RelaxExtractPawn) eMG.LessonTC.SetNextLesson();
                        }

                        else if (!eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        sMG.BuildingSs.ClearS.Clear(cell_0);

                        if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cell_0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }

                else if (eMG.PawnExtractHillC(cell_0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractHillC(cell_0).Resources;

                    eMG.HillC(cell_0).Resources -= extract;
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (eMG.LessonTC.Is(LessonTypes.ExtractHill))
                    {
                        eMG.LessonTC.SetNextLesson();

                        if (eMG.IsSelectedCity)
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (!eMG.HillC(cell_0).HaveAnyResources)
                    {

                        //else if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        //{
                        //    eMG.LessonTC.SetNextLesson();
                        //    eMG.LessonTC.SetNextLesson();
                        //}
                    }
                }

                else if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && eMG.HpUnitC(cell_0).Health >= HpValues.MAX)
                {
                    eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }


                if (eMG.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (eMG.AdultForestC(cellE).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                eMG.HaveFire(cellE) = true;
                            }
                        }
                    }

                    if (eMG.RiverTC(cell_0).HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).CellsAround.Any(cell => cell == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (eMG.BuildingTC(cellE).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }


            if (!eMG.LessonTC.HaveLesson || eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    var res = ResourceTypes.Food;

                    eMG.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                    if (eMG.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                    {
                        eMG.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                        for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                        {
                            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitPlayerTC(cell_0).Is(player))
                            {
                                sMG.UnitSs.KillUnitS.Kill(eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                                sMG.UnitSs.ClearUnit(cell_0);

                                break;
                            }
                        }
                    }
                }
            }

            


            var haveCamel = false;

            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

                if (!eMG.IsBorder(cell_0))
                {
                    if (!eMG.UnitTC(cell_0).HaveUnit && !eMG.MountainC(cell_0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            if (eMG.UnitTC(cell_1).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            sMG.SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }



            if (!eMG.LessonTC.HaveLesson)
            {
                if (eMG.MotionsC.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                        eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                        if (!eMG.IsBorder(cell_0))
                        {
                            if (eMG.UnitTC(cell_0).HaveUnit)
                            {
                                if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy))
                                {
                                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            eMG.FrozenArrawEffectC(cell_0).Shoots++;
                                        }
                                        else
                                        {
                                            eMG.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                        }
                                    }
                                    else
                                    {
                                        eMG.ShieldUnitEffectC(cell_0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                            }
                            else
                            {
                                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (!eMG.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (eMG.PlayerInfoE(playerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                                            {
                                                sMG.SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

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
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !eMG.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !eMG.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                _truceS.Truce();
            }

            //if (!eMG.LessonTC.HaveLesson)
            //{
            //    _aIBotS.Execute();
            //}


            if (eMG.LessonT == LessonTypes.Install3WarriorsNextToTheRiver)
            {
                var amountUnitsNearRiverForLesson = 0;

                for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
                {
                    if (eMG.UnitT(cellIdx0) == UnitTypes.Pawn && eMG.UnitPlayerT(cellIdx0) == PlayerTypes.First && eMG.RiverTC(cellIdx0).HaveRiverNear)
                    {
                        amountUnitsNearRiverForLesson++;
                    }
                }

                if (amountUnitsNearRiverForLesson >= 3)
                {
                    eMG.LessonTC.SetNextLesson();
                }
            }


            
        }
    }
}