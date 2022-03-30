using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Game.System.Model.Master
{
    sealed class UpdateS_M : SystemModelGameAbs
    {
        readonly TruceMS _truceS_M;
        readonly FireUpdateMS _fireUpdateS_M;

        public UpdateS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame)
        {
            _truceS_M = new TruceMS(sMGame, eMGame);
            _fireUpdateS_M = new FireUpdateMS(sMGame, eMGame);
        }

        internal void Run(in GameModeTC gameModeTC)
        {
            _fireUpdateS_M.Run();


            e.MotionsC.Motions++;
            e.WeatherE.SunC.ToggleNext();


            if (e.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    e.PlayerInfoE(playerT).PeopleInCity += 1;
                }
            }


            for (var i = 0; i < e.WeatherE.WindC.Speed; i++)
            {
                var cell = e.WeatherE.CloudC.Center;
                var xy_next = e.CellEs(cell).AroundCellsEs.AroundCellE(e.WeatherE.WindC.Direct).XyC.Xy;
                var idx_next = e.CellEs(cell).AroundCellsEs.AroundCellE(e.WeatherE.WindC.Direct).IdxC.Idx;


                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        e.WeatherE.CloudC.Center = e.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = e.WeatherE.WindC.Direct;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        e.WeatherE.WindC.Direct = (DirectTypes)newDirInt;

                        break;
                    }
                }


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    e.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
                }
            }

            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) e.WeatherE.WindC.Speed = UnityEngine.Random.Range(0, 4);



            var cell_0 = e.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                if (!e.MountainC(idx_1).HaveAnyResources)
                {
                    e.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }


            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.UnitTC(cell_0).HaveUnit)
                {
                    if (e.UnitTC(cell_0).Is(UnitTypes.Snowy))
                    {
                        if (e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                        {
                            e.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }

                    }

                    if (e.UnitTC(cell_0).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE((DirectTypes)randDir).IdxC.Idx;

                        if (e.CellEs(idx_1).IsActiveParentSelf && !e.MountainC(idx_1).HaveAnyResources
                            && !e.UnitTC(idx_1).HaveUnit)
                        {
                            s.SetUnitS.Set(cell_0, idx_1);

                            s.ClearUnitS.Clear(cell_0);
                        }
                    }




                    if (e.UnitTC(cell_0).Is(UnitTypes.Pawn)) e.ResourcesC(e.UnitPlayerTC(cell_0).Player, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                        {
                            e.UnitHpC(cell_0).Health = HpValues.MAX;
                        }
                    }


                    if (e.HaveFire(cell_0))
                    {
                        e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (e.UnitHpC(cell_0).Health >= HpValues.MAX)
                            {
                                if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (e.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !e.BuildingTC(cell_0).HaveBuilding)
                                    {
                                        if (gameModeTC.Is(GameModes.TrainingOff))
                                        {
                                            if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                                            {
                                                if (e.BuildingsInfo(e.UnitPlayerTC(cell_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (e.BuildingsInfo(e.UnitPlayerTC(cell_0).Player, e.BuildingLevelTC(cell_0).Level, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {

                        }

                        else
                        {
                            if (e.UnitStepC(cell_0).HaveAnySteps)
                            {
                                e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    e.UnitStepC(cell_0).Steps = StepValues.MAX;
                }

























                if (e.RiverEs(cell_0).RiverTC.HaveRiverNear)
                {
                    if (!e.MountainC(cell_0).HaveAnyResources)
                    {
                        e.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                if (e.FertilizeC(cell_0).HaveAnyResources)
                {
                    e.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE;
                }

                if (e.FarmExtractFertilizeE(cell_0).HaveAnyResources)
                {
                    var extract = e.FarmExtractFertilizeE(cell_0).Resources;

                    e.ResourcesC(e.BuildingPlayerTC(cell_0).Player, ResourceTypes.Food).Resources += extract;
                    e.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }

                if (e.WoodcutterExtractE(cell_0).HaveAnyResources)
                {
                    var extract = e.WoodcutterExtractE(cell_0).Resources;

                    e.ResourcesC(e.BuildingPlayerTC(cell_0).Player, ResourceTypes.Wood).Resources += extract;
                    s.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (!e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        e.BuildingT(cell_0) = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            e.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                }




                if (e.UnitTC(cell_0).HaveUnit && !e.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (e.RiverEs(cell_0).RiverTC.HaveRiverNear)
                        {
                            e.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(e.UnitTC(cell_0).Unit);

                            if (e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            e.UnitWaterC(cell_0).Water -= needWater;

                            if (e.UnitWaterC(cell_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(e.UnitTC(cell_0).Unit);

                                s.AttackUnitS.Attack(HpValues.MAX * percent, e.NextPlayer(e.UnitPlayerTC(cell_0)).Player, cell_0);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(cell_0)).Player, cell_0);
                            }
                        }
                    }
                }



                if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    e.UnitHpC(cell_0).Health = HpValues.MAX;
                }

                if (e.PawnExtractAdultForestE(cell_0).HaveAnyResources)
                {
                    var extract = e.PawnExtractAdultForestE(cell_0).Resources;

                    e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    s.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (e.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !e.BuildingTC(cell_0).HaveBuilding)
                        {
                            s.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, e.UnitPlayerTC(cell_0).Player, 1, cell_0);
                        }

                        else if (!e.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        s.ClearBuildingS.Clear(cell_0);

                        e.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                else if (e.PawnExtractHillE(cell_0).HaveAnyResources)
                {
                    var extract = e.PawnExtractHillE(cell_0).Resources;

                    e.HillC(cell_0).Resources -= extract;
                    e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (!e.HillC(cell_0).HaveAnyResources)
                    {
                        if (e.LessonTC.Is(LessonTypes.ExtractHillPawnHere)) e.LessonTC.SetNextLesson();

                    }

                }

                else if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && e.UnitHpC(cell_0).Health >= HpValues.MAX)
                {
                    e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }


                if (e.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in e.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (e.AdultForestC(cellE.IdxC.Idx).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                e.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }

                    if (e.RiverEs(cell_0).RiverTC.HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in e.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (e.BuildingTC(cellE.IdxC.Idx).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }



            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                e.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                if (e.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    e.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                    for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitPlayerTC(cell_0).Is(player))
                        {
                            s.KillUnitS.Kill(e.NextPlayer(e.UnitPlayerTC(cell_0).Player).Player, cell_0);

                            s.ClearUnitS.Clear(cell_0);
                            break;
                        }
                    }
                }
            }


            var haveCamel = false;

            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

                if (e.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (!e.UnitTC(cell_0).HaveUnit && !e.EnvironmentEs(cell_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in e.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                        {
                            if (e.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            s.SetNewUnitS.Set(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }




            if (e.MotionsC.Motions % 5 == 0)
            {
                for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (e.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (e.UnitTC(cell_0).HaveUnit)
                        {
                            if (e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                {
                                    if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        e.UnitEffectFrozenArrawC(cell_0).Shoots++;
                                    }
                                    else
                                    {
                                        e.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    e.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                        }
                        else
                        {
                            if (e.AdultForestC(cell_0).HaveAnyResources)
                            {
                                if (!e.HaveTreeUnit)
                                {
                                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                    {
                                        if (e.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            s.SetNewUnitS.Set(UnitTypes.Tree, playerT, cell_0);

                                            break;
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
                if (e.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                _truceS_M.Run(gameModeTC);
            }
        }
    }
}