using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Game.Model.System.Master
{
    sealed class UpdateS_M : SystemModelGameAbs
    {
        readonly TruceMS _truceS_M;
        readonly FireUpdateMS _fireUpdateS_M;

        internal UpdateS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
            _truceS_M = new TruceMS(sMC, eMC, sMG, eMG);
            _fireUpdateS_M = new FireUpdateMS(sMC, eMC, sMG, eMG);
        }

        internal void Run(in GameModeTC gameModeTC)
        {
            _fireUpdateS_M.Run();


            eMG.MotionsC.Motions++;
            eMG.WeatherE.SunSideTC.ToggleNext();


            if (eMG.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    eMG.PlayerInfoE(playerT).PeopleInCity += 1;
                }
            }


            for (var i = 0; i < eMG.WeatherE.WindC.Speed; i++)
            {
                var cell = eMG.WeatherE.CloudC.Center;
                var xy_next = eMG.CellEs(cell).AroundCellsEs.AroundCellE(eMG.WeatherE.WindC.DirectT).XyC.Xy;
                var idx_next = eMG.CellEs(cell).AroundCellsEs.AroundCellE(eMG.WeatherE.WindC.DirectT).IdxC.Idx;


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

                        break;
                    }
                }


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMG.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
                }
            }

            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) eMG.WeatherE.WindC.Speed = UnityEngine.Random.Range(0, 4);



            var cell_0 = eMG.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                if (!eMG.MountainC(idx_1).HaveAnyResources)
                {
                    eMG.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }


            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Snowy))
                    {
                        if (eMG.CellEs(eMG.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                        {
                            eMG.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }

                    }

                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE((DirectTypes)randDir).IdxC.Idx;

                        if (eMG.CellEs(idx_1).IsActiveParentSelf && !eMG.MountainC(idx_1).HaveAnyResources
                            && !eMG.UnitTC(idx_1).HaveUnit)
                        {
                            sMG.UnitSs.SetUnitS.Set(cell_0, idx_1);

                            sMG.UnitSs.ClearUnitS.Clear(cell_0);
                        }
                    }

                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn)) eMG.ResourcesC(eMG.UnitPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;


                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                        {
                            eMG.UnitHpC(cell_0).Health = HpValues.MAX;
                        }

                        if (eMG.LessonTC.HaveLesson)
                        {
                            if (eMG.UnitTC(cell_0).Is(UnitTypes.King))
                            {
                                eMG.UnitWaterC(cell_0).Water = WaterValues.MAX;
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
                            if (eMG.UnitHpC(cell_0).Health >= HpValues.MAX)
                            {
                                if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !eMG.BuildingTC(cell_0).HaveBuilding)
                                    {
                                        if (gameModeTC.Is(GameModes.TrainingOff))
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
                            if (eMG.UnitStepC(cell_0).HaveAnySteps)
                            {
                                eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    eMG.UnitStepC(cell_0).Steps = StepValues.MAX;
                }

























                if (eMG.RiverEs(cell_0).RiverTC.HaveRiverNear)
                {
                    if (!eMG.MountainC(cell_0).HaveAnyResources)
                    {
                        eMG.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                if (eMG.FertilizeC(cell_0).HaveAnyResources)
                {
                    eMG.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE;
                }

                if (eMG.FarmExtractFertilizeE(cell_0).HaveAnyResources)
                {
                    var extract = eMG.FarmExtractFertilizeE(cell_0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Food).Resources += extract;
                    eMG.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }

                if (eMG.WoodcutterExtractE(cell_0).HaveAnyResources)
                {
                    var extract = eMG.WoodcutterExtractE(cell_0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cell_0).PlayerT, ResourceTypes.Wood).Resources += extract;
                    sMG.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (!eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            eMG.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                }




                if (eMG.UnitTC(cell_0).HaveUnit && !eMG.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (eMG.RiverEs(cell_0).RiverTC.HaveRiverNear)
                        {
                            eMG.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(eMG.UnitTC(cell_0).UnitT);

                            if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            eMG.UnitWaterC(cell_0).Water -= needWater;

                            if (eMG.UnitWaterC(cell_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(eMG.UnitTC(cell_0).UnitT);

                                sMG.UnitSs.AttackUnitS.Attack(HpValues.MAX * percent, eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(cell_0)).Player, cell_0);
                            }
                        }
                    }
                }



                if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    eMG.UnitHpC(cell_0).Health = HpValues.MAX;
                }

                if (eMG.PawnExtractAdultForestE(cell_0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractAdultForestE(cell_0).Resources;

                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    sMG.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !eMG.BuildingTC(cell_0).HaveBuilding)
                        {
                            sMG.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMG.UnitPlayerTC(cell_0).PlayerT, 1, cell_0);
                        }

                        else if (!eMG.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        sMG.ClearBuildingS.Clear(cell_0);

                        eMG.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                else if (eMG.PawnExtractHillE(cell_0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractHillE(cell_0).Resources;

                    eMG.HillC(cell_0).Resources -= extract;
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (!eMG.HillC(cell_0).HaveAnyResources)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ExtractHillPawnHere))
                        {
                            eMG.LessonTC.SetNextLesson();

                            if (eMG.IsSelectedCity)
                            {
                                eMG.LessonTC.SetNextLesson();
                            }
                        }
                    }
                }

                else if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && eMG.UnitHpC(cell_0).Health >= HpValues.MAX)
                {
                    eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }


                if (eMG.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in eMG.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (eMG.AdultForestC(cellE.IdxC.Idx).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                eMG.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }

                    if (eMG.RiverEs(cell_0).RiverTC.HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (eMG.CellEs(eMG.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in eMG.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (eMG.BuildingTC(cellE.IdxC.Idx).Is(BuildingTypes.IceWall))
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

                eMG.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                if (eMG.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    eMG.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                    for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitPlayerTC(cell_0).Is(player))
                        {
                            sMG.UnitSs.KillUnitS.Kill(eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);

                            sMG.UnitSs.ClearUnitS.Clear(cell_0);
                            break;
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

                if (eMG.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (!eMG.UnitTC(cell_0).HaveUnit && !eMG.EnvironmentEs(cell_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in eMG.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                        {
                            if (eMG.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            sMG.UnitSs.SetNewUnitS.Set(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }




            if (eMG.MotionsC.Motions % 5 == 0)
            {
                for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (eMG.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (eMG.UnitTC(cell_0).HaveUnit)
                        {
                            if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                {
                                    if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        eMG.UnitEffectFrozenArrawC(cell_0).Shoots++;
                                    }
                                    else
                                    {
                                        eMG.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    eMG.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
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
                                        if (eMG.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            sMG.UnitSs.SetNewUnitS.Set(UnitTypes.Tree, playerT, cell_0);

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
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                _truceS_M.Run(gameModeTC);
            }
        }
    }
}