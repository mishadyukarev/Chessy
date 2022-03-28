using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Linq;

namespace Chessy.Game.System.Model.Master
{
    public sealed class UpdateS_M : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMGame;

        readonly TruceMS _truceS_M;
        readonly FireUpdateMS _fireUpdateS_M;

        public UpdateS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
            _truceS_M = new TruceMS(eMGame);
            _fireUpdateS_M = new FireUpdateMS(sMGame, eMGame);
        }

        internal void Run(in GameModeTC gameModeTC)
        {
            _fireUpdateS_M.Run();


            eMGame.MotionsC.Motions++;
            eMGame.WeatherE.SunC.ToggleNext();


            if (eMGame.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    eMGame.PlayerInfoE(playerT).PeopleInCity += 1;
                }
            }


            for (var i = 0; i < eMGame.WeatherE.WindC.Speed; i++)
            {
                var cell = eMGame.WeatherE.CloudC.Center;
                var xy_next = eMGame.CellEs(cell).AroundCellsEs.AroundCellE(eMGame.WeatherE.WindC.Direct).XyC.Xy;
                var idx_next = eMGame.CellEs(cell).AroundCellsEs.AroundCellE(eMGame.WeatherE.WindC.Direct).IdxC.Idx;


                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        eMGame.WeatherE.CloudC.Center = eMGame.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = eMGame.WeatherE.WindC.Direct;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        eMGame.WeatherE.WindC.Direct = (DirectTypes)newDirInt;

                        break;
                    }
                }


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMGame.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
                }
            }

            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) eMGame.WeatherE.WindC.Speed = UnityEngine.Random.Range(0, 4);



            var cell_0 = eMGame.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                if (!eMGame.MountainC(idx_1).HaveAnyResources)
                {
                    eMGame.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }


            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).HaveUnit)
                {
                    if (eMGame.UnitTC(cell_0).Is(UnitTypes.Snowy))
                    {
                        if (eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                        {
                            eMGame.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }

                    }

                    if (eMGame.UnitTC(cell_0).Is(UnitTypes.Wolf))
                    {
                        var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                        var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE((DirectTypes)randDir).IdxC.Idx;

                        if (eMGame.CellEs(idx_1).IsActiveParentSelf && !eMGame.MountainC(idx_1).HaveAnyResources
                            && !eMGame.UnitTC(idx_1).HaveUnit)
                        {
                            _sMGame.CellSs(idx_1).SetUnitS.Set(eMGame.UnitEs(cell_0));

                            _sMGame.CellSs(cell_0).ClearUnitS.Clear();
                        }
                    }



                    if (!eMGame.UnitTC(cell_0).IsAnimal)
                    {
                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn)) eMGame.ResourcesC(eMGame.UnitPlayerTC(cell_0).Player, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                        if (gameModeTC.Is(GameModes.TrainingOff))
                        {
                            if (eMGame.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                            {
                                eMGame.UnitHpC(cell_0).Health = HpValues.MAX;
                            }
                        }


                        if (eMGame.HaveFire(cell_0))
                        {
                            eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                        }

                        else
                        {
                            if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                            {
                                if (eMGame.UnitHpC(cell_0).Health >= HpValues.MAX)
                                {
                                    if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                    {
                                        if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !eMGame.BuildingTC(cell_0).HaveBuilding)
                                        {
                                            if (gameModeTC.Is(GameModes.TrainingOff))
                                            {
                                                if (eMGame.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                                                {
                                                    if (eMGame.BuildingsInfo(eMGame.UnitPlayerTC(cell_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                    {
                                                        //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                    }


                                                    //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                                }
                                            }
                                            else
                                            {
                                                if (eMGame.BuildingsInfo(eMGame.UnitPlayerTC(cell_0).Player, eMGame.BuildingLevelTC(cell_0).Level, BuildingTypes.Camp).IdxC.HaveAny)
                                                {
                                                    //Es.BuildingE(idx_camp).Destroy(Es);
                                                }

                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                    }
                                }
                            }

                            else if (!eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                            {
                                if (eMGame.UnitStepC(cell_0).HaveAnySteps)
                                {
                                    eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                                }
                            }
                        }
                        eMGame.UnitStepC(cell_0).Steps = StepValues.MAX;
                    }
                }
























                if (eMGame.RiverEs(cell_0).RiverTC.HaveRiverNear)
                {
                    if (!eMGame.MountainC(cell_0).HaveAnyResources)
                    {
                        eMGame.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }

                if (eMGame.FertilizeC(cell_0).HaveAnyResources)
                {
                    eMGame.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE;
                }

                if (eMGame.FarmExtractFertilizeE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.FarmExtractFertilizeE(cell_0).Resources;

                    eMGame.ResourcesC(eMGame.BuildingPlayerTC(cell_0).Player, ResourceTypes.Food).Resources += extract;
                    eMGame.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }

                if (eMGame.WoodcutterExtractE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.WoodcutterExtractE(cell_0).Resources;

                    eMGame.ResourcesC(eMGame.BuildingPlayerTC(cell_0).Player, ResourceTypes.Wood).Resources += extract;
                    _sMGame.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (!eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            eMGame.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                }




                if (eMGame.UnitTC(cell_0).HaveUnit && !eMGame.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (eMGame.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (eMGame.RiverEs(cell_0).RiverTC.HaveRiverNear)
                        {
                            eMGame.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(eMGame.UnitTC(cell_0).Unit);

                            if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            eMGame.UnitWaterC(cell_0).Water -= needWater;

                            if (eMGame.UnitWaterC(cell_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(eMGame.UnitTC(cell_0).Unit);

                                _sMGame.CellSs(cell_0).AttackUnitS.Attack(HpValues.MAX * percent, eMGame.NextPlayer(eMGame.UnitPlayerTC(cell_0)).Player);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(cell_0)).Player, cell_0);
                            }
                        }
                    }
                }



                if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    eMGame.UnitHpC(cell_0).Health = HpValues.MAX;
                }

                if (eMGame.PawnExtractAdultForestE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.PawnExtractAdultForestE(cell_0).Resources;

                    eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    _sMGame.TakeAdultForestResourcesS.Take(extract, cell_0);

                    if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !eMGame.BuildingTC(cell_0).HaveBuilding)
                        {
                            _sMGame.CellSs(cell_0).BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMGame.UnitPlayerTC(cell_0).Player, 1);
                        }

                        else if (!eMGame.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                        eMGame.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
                else if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && eMGame.UnitHpC(cell_0).Health >= HpValues.MAX)
                {
                    eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }


                if (eMGame.PawnExtractHillE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.PawnExtractHillE(cell_0).Resources;

                    eMGame.HillC(cell_0).Resources -= extract;
                    eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Ore).Resources += extract;


                    //if (E.AdultForestC(cell_0).HaveAny)
                    //{

                    //}
                    //else
                    //{
                    //    E.BuildTC(cell_0).Build = BuildingTypes.None;

                    //    E.YoungForestC(cell_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                    //}
                }


                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in eMGame.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (eMGame.AdultForestC(cellE.IdxC.Idx).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                eMGame.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }

                    if (eMGame.RiverEs(cell_0).RiverTC.HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in eMGame.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                    {
                        if (eMGame.BuildingTC(cellE.IdxC.Idx).Is(BuildingTypes.IceWall))
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

                eMGame.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                if (eMGame.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    eMGame.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                    for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitPlayerTC(cell_0).Is(player))
                        {
                            _sMGame.CellSs(cell_0).KillUnitS.Kill(eMGame.NextPlayer(eMGame.UnitPlayerTC(cell_0).Player).Player);

                            _sMGame.CellSs(cell_0).ClearUnitS.Clear();
                            break;
                        }
                    }
                }
            }


            var haveCamel = false;

            for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                cell_0 = (byte)UnityEngine.Random.Range(0, StartValues.CELLS);

                if (eMGame.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (!eMGame.UnitTC(cell_0).HaveUnit && !eMGame.EnvironmentEs(cell_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in eMGame.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                        {
                            if (eMGame.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            _sMGame.CellSs(cell_0).SetNewUnitS.Set(UnitTypes.Wolf, PlayerTypes.None);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }




            if (eMGame.MotionsC.Motions % 5 == 0)
            {
                for (cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (eMGame.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (eMGame.UnitTC(cell_0).HaveUnit)
                        {
                            if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                {
                                    if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        eMGame.UnitEffectFrozenArrawC(cell_0).Shoots++;
                                    }
                                    else
                                    {
                                        eMGame.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    eMGame.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                        }
                        else
                        {
                            if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                            {
                                if (!eMGame.HaveTreeUnit)
                                {
                                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                    {
                                        if (eMGame.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            _sMGame.CellSs(cell_0).SetNewUnitS.Set(UnitTypes.Tree, playerT);

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
                if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                _truceS_M.Run(gameModeTC);
            }
        }
    }
}