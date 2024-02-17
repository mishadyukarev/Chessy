using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public sealed class UpdateModelS : SystemModelAbstract
    {
        readonly Action[] _runs;
        DateTime _dateTimeLastUpdate;

        readonly BotAIS _botAIS;


        internal UpdateModelS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _runs = new Action[]
{
                new InputS(sMG, eMG).Update,
                new CheatsS(sMG, eMG).Update,
                new RayS(sMG, eMG).Update,
                new SelectorS(sMG, eMG).Update,

                new MistakeS(sMG, eMG).Update,
            };
            _dateTimeLastUpdate = DateTime.Now;


            _botAIS = new BotAIS(sMG, eMG);
        }


        internal void Update()
        {
            var timeDeltaTime = Time.deltaTime;

            for (int i = 0; i < _runs.Length; i++)
            {
                _runs[i].Invoke();//.ForEach((Action action) => action());
            }


            _botAIS.Update();




            if (PhotonNetwork.IsMasterClient)
            {
                if ((DateTime.Now - _dateTimeLastUpdate).Seconds >= 1)
                {
                    sunC.SecondsForChangingSideSun--;

                    if (sunC.SecondsForChangingSideSun <= 0)
                    {
                        sunC.ToggleNextSunSideT();
                        sunC.SecondsForChangingSideSun = 20;
                    }

                    for (byte player_byte = 1; player_byte < (byte)PlayerTypes.End; player_byte++)
                    {
                        if (godInfoCs[player_byte].CooldownInSecondsForNextAppearance > 0)
                        {
                            godInfoCs[player_byte].CooldownInSecondsForNextAppearance--;
                        }
                    }

                    for (byte currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
                    {
                        if (CloudC(currentIdxCell).IsCenter)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                hpTrailCs[currentIdxCell].Health(dirT) = 0;
                            }

                            foreach (var item in IdxsAroundCellC(currentIdxCell).IdxCellsAroundArray)
                            {
                                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                {
                                    hpTrailCs[item].Health(dirT) = 0;
                                }
                            }
                        }
                    }


                    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                    {
                        if (unitCs[cellIdxCurrent].HaveUnit)
                        {
                            for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                            {
                                _cooldownAbilityCs[cellIdxCurrent].Take(abilityT, 1);
                            }

                            _effectsUnitCs[cellIdxCurrent].StunHowManyUpdatesNeedStay--;

                            if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Snowy)
                            {
                                if (!_effectsUnitCs[cellIdxCurrent].HaveFrozenArrawArcher)
                                {
                                    _effectsUnitCs[cellIdxCurrent].SecondForSnowyFrozenArraw--;

                                    if (_effectsUnitCs[cellIdxCurrent].SecondForSnowyFrozenArraw <= 0)
                                    {
                                        _effectsUnitCs[cellIdxCurrent].HaveFrozenArrawArcher = true;
                                        _effectsUnitCs[cellIdxCurrent].SecondForSnowyFrozenArraw = 5;
                                    }
                                }
                            }

                            unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition++;


                            if (unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                            {
                                if (unitHpCs[cellIdxCurrent].Health >= HpUnitValues.MAX)
                                {
                                    if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
                                    {
                                        if (!environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest) && !environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill))
                                        {
                                            unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
                                        }
                                    }
                                    else
                                    {
                                        unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
                                    }

                                }

                            }
                        }

                        UnitAttackC(cellIdxCurrent).CooldownForAttackAnyUnitInSeconds--;
                    }

                    PutOutFireWithClouds();
                    BurnAdultForest();
                    FireUpdate();
                    TryExtractForestWithPawn();
                    TryExtractWoodWithWoodcutter();
                    TryGiveWaterToBotUnits();
                    GiveWaterToUnitsNearWithRiver();
                    TakeWaterUnits();
                    TryAttackUnitsWithoutWater();
                    FeedUnits();
                    TrySpawnWolf();
                    TryShiftWolf();
                    TryPoorWaterToCellsWithClounds();
                    TryGiveWaterAroundRiverToCells();
                    DryWaterOnCells();//
                    TryExtractFoodWithFarm();//
                    TryExtractHillsWithPawns();
                    GiveFoodAfterUpdate();
                    TryExecuteHungry();
                    TryChangeDirectionOfWindRandomly();
                    TryGiveHealthToBots();
                    ToggleConditionUnitsIfTheresFire();
                    TryGiveHealthToUnitsWithRelaxCondition();//
                    TryGiveWaterToUnitsAroundRainy();
                    TryActiveGodsUniqueAbilityEveryUpdate();
                    TrySetDefendWithoutConditionUnits();
                    TryExecuteTruce();



                    s.GetDataCellsS.GetDataCells();

                    _dateTimeLastUpdate = DateTime.Now;
                }

                s.TryExecuteShiftingUnitS.TryExecute();
                s.TryShiftCloudsMS.TryShift();
            }
        }



        void TrySetDefendWithoutConditionUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.None)
                    {
                        if (!shiftingUnitCs[cellIdxCurrent].IsShifting)
                        {
                            if (unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition >= 3)
                            {
                                unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
                                unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition = 0;
                            }
                        }
                    }
                }

                //if (!_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractHill && !_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                //{
                //    if (_unitCs[cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                //        && _hpUnitCs[cellIdxCurrent).Health >= HpValues.MAX)
                //    {
                //        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                //    }
                //}
            }
        }

        int _secondsForGodsAbilities;

        void TryActiveGodsUniqueAbilityEveryUpdate()
        {
            _secondsForGodsAbilities++;

            if (!aboutGameC.LessonT.HaveLesson())
            {
                if (_secondsForGodsAbilities >= 60)
                {
                    s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (CellC(cell_0).IsBorder) continue;

                        if (unitCs[cell_0].HaveUnit)
                        {
                            if (PlayerInfoE(unitCs[cell_0].PlayerType).GodInfoC.UnitType.Is(UnitTypes.Snowy))
                            {
                                if (unitCs[cell_0].UnitT == UnitTypes.Pawn)
                                {
                                    if (_mainTWC[cell_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                                    {
                                        _effectsUnitCs[cell_0].HaveFrozenArrawArcher = true;
                                    }
                                    else
                                    {
                                        _effectsUnitCs[cell_0].ProtectionRainyMagicShield = ValuesChessy.PROTECTION_MAGIC_SHIELD_AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                                else
                                {
                                    _effectsUnitCs[cell_0].ProtectionRainyMagicShield = ValuesChessy.PROTECTION_MAGIC_SHIELD_AFTER_5_MOTIONS_RAINY;
                                }
                            }
                        }
                        else
                        {
                            if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                if (!buildingCs[cell_0].HaveBuilding)
                                {
                                    if (!aboutGameC.HaveTreeUnitInGame)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                s.SetNewUnitOnCellS.Set(UnitTypes.Tree, playerT, cell_0);

                                                environmentCs[cell_0].Set(EnvironmentTypes.AdultForest, 0);
                                                buildingCs[cell_0].BuildingT = BuildingTypes.None;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                    {
                        if (PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                        {

                            for (byte curCell_0 = 0; curCell_0 < IndexCellsValues.CELLS; curCell_0++)
                            {
                                if (CellC(curCell_0).IsBorder) continue;

                                if (environmentCs[curCell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    if (unitCs[curCell_0].HaveUnit && unitCs[curCell_0].PlayerType != playerT)
                                    {
                                        _effectsUnitCs[curCell_0].StunHowManyUpdatesNeedStay = StunUnitValues.AFTER_ELFEMALE_BLOWOUT;
                                    }
                                }
                            }
                        }
                    }


                    _secondsForGodsAbilities = 0;
                }
            }
        }
        void ToggleConditionUnitsIfTheresFire()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (FireC(cellIdxCurrent).HaveFire)
                    {
                        unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.None;
                    }
                }
            }
        }
        void TryAttackUnitsWithoutWater()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit && !unitCs[cellIdxCurrent].IsAnimal)
                {
                    if (_unitWaterCs[cellIdxCurrent].Water <= 0)
                    {
                        //var percent = Time.deltaTime;// HpValues.ThirstyPercent(_unitCs[cellIdxCurrent));

                        s.AttackUnitOnCell(HpUnitValues.MAX * 0.05, unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
                    }
                }
            }
        }

        void TryPoorWaterToCellsWithClounds()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (CloudC(curCellIdx).IsCenter)
                {
                    if (!environmentCs[curCellIdx].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        environmentCs[curCellIdx].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }

                    foreach (var item in IdxsAroundCellC(curCellIdx).IdxCellsAroundArray)
                    {
                        if (!environmentCs[item].HaveEnvironment(EnvironmentTypes.Mountain))
                        {
                            environmentCs[item].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        }
                    }
                }
            }
        }
        void TryExtractWoodWithWoodcutter()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (CellC(cellIdxCurrent).IsBorder) continue;
                if (buildingCs[cellIdxCurrent].BuildingT != BuildingTypes.Woodcutter) continue;

                s.GetDataCellsS.GetWoodcutterExtractCells(cellIdxCurrent);

                var extract = extractionBuildingCs[cellIdxCurrent].HowManyWoodcutterCanExtractWood;

                ResourcesInInventoryC(buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, (float)extract);
                s.TryTakeAdultForestResourcesM((float)extract, cellIdxCurrent);

                if (!environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    buildingCs[cellIdxCurrent].BuildingT = BuildingTypes.None;

                    if (aboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                    {
                        if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            aboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
                        }
                    }
                }
            }
        }
        void FeedUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (!aboutGameC.LessonT.HaveLesson() || aboutGameC.LessonT >= LessonTypes.Build1Farms)
                    {
                        if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
                            ResourcesInInventoryC(unitCs[cellIdxCurrent].PlayerT).Subtract(ResourceTypes.Food, EconomyValues.FOOD_FOR_FEEDING_ONE_UNIT_AFTER_EVERY_UPDATE);
                    }
                }
            }
        }
        void TryExtractForestWithPawn()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_extractionResourcesWithUnitCs[cellIdxCurrent].CanExtractAdultForest)
                {
                    var extract = _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractAdultForest;

                    ResourcesInInventoryC(unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, extract);
                    s.TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        if (buildingCs[cellIdxCurrent].BuildingT != BuildingTypes.Woodcutter)
                        {
                            if (unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition >= 10)
                            {
                                s.Build(BuildingTypes.Woodcutter, LevelTypes.First, unitCs[cellIdxCurrent].PlayerT, cellIdxCurrent);

                                if (aboutGameC.LessonT == LessonTypes.RelaxExtractPawn) s.SetNextLesson();

                                s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.BuildingWoodcutterWithWarrior);
                            }
                        }
                    }
                    else
                    {
                        //_e.ClearBuildingOnCell(cellIdxCurrent);

                        if (aboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                aboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }
            }
        }
        void TryGiveWaterAroundRiverToCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (RiverC(cellIdxCurrent).HaveRiverNear)
                {
                    if (!environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        environmentCs[cellIdxCurrent].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }
                }
            }
        }
        void DryWaterOnCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer))
                {
                    environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Fertilizer) -= ValuesChessy.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }
            }
        }
        void TryExtractFoodWithFarm()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (extractionBuildingCs[cellIdxCurrent].CanFarmExtact)
                {
                    var extract = extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood;

                    ResourcesInInventoryC(buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Food, (float)extract);
                    environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Fertilizer) -= extract;

                    //if (!E.FertilizeC(cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
        void TryExecuteTruce()
        {
            var amountAdultForest = 0;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                    amountAdultForest++;
            }

            var can = !PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= ValuesChessy.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                s.TruceS.ExecuteTruce();
            }
        }
        void TryExecuteHungry()
        {
            if (!aboutGameC.LessonT.HaveLesson() || aboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (ResourcesInInventoryC(playerT).ResourcesRef(res) < 0)
                    {
                        ResourcesInInventoryC(playerT).Set(res, 0);

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && unitCs[cellIdxCurrent].PlayerT == playerT)
                            {
                                unitHpCs[cellIdxCurrent].Health -= HpUnitValues.MAX * 0.05f;
                                if (unitHpCs[cellIdxCurrent].Health <= 0)
                                {
                                    s.KillUnit(unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
                                    _unitEs[cellIdxCurrent].Dispose();
                                }
                            }
                        }
                    }
                }
            }
        }
        void TryExtractHillsWithPawns()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_extractionResourcesWithUnitCs[cellIdxCurrent].CanExtractHill && !_extractionResourcesWithUnitCs[cellIdxCurrent].CanExtractAdultForest)
                {
                    var extract = _extractionResourcesWithUnitCs[cellIdxCurrent].HowManyWarriourCanExtractHill;

                    environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Hill) -= extract;
                    ResourcesInInventoryC(unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Ore, extract);

                    if (aboutGameC.LessonT.Is(LessonTypes.ExtractHill))
                    {
                        s.SetNextLesson();

                        if (aboutGameC.IsSelectedCity)
                        {
                            s.SetNextLesson();
                        }
                    }
                }
            }
        }
        void GiveFoodAfterUpdate()
        {
            if (!aboutGameC.LessonT.HaveLesson() || aboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    ResourcesInInventoryC(player).Add(ResourceTypes.Food, EconomyValues.ADDING_FOOD_AFTER_UPDATE);
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= ValuesChessy.PERCENT_FOR_CHANGING_WIND) windC.Speed = (byte)UnityEngine.Random.Range(1, 4);
        }
        void TryGiveHealthToBots()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (aboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (unitCs[cellIdxCurrent].PlayerT == PlayerTypes.Second)
                        {
                            if (unitHpCs[cellIdxCurrent].Health < HpUnitValues.MAX)
                            {
                                unitHpCs[cellIdxCurrent].Health += HpUnitValues.MAX * 0.05;

                                if (unitHpCs[cellIdxCurrent].Health > HpUnitValues.MAX)
                                {
                                    unitHpCs[cellIdxCurrent].Health = HpUnitValues.MAX;
                                }
                            }

                        }
                    }
                }
            }
        }
        void TryGiveHealthToUnitsWithRelaxCondition()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                {
                    if (unitHpCs[cellIdxCurrent].Health < HpUnitValues.MAX)
                    {
                        if (_unitWaterCs[cellIdxCurrent].HaveAnyWater)
                        {
                            if (ResourcesInInventoryC(unitCs[cellIdxCurrent].PlayerT).ResourcesRef(ResourceTypes.Food) > 0)
                            {
                                unitHpCs[cellIdxCurrent].Health += HpUnitValues.MAX * 0.05f;

                                if (unitHpCs[cellIdxCurrent].Health > HpUnitValues.MAX)
                                {
                                    unitHpCs[cellIdxCurrent].Health = HpUnitValues.MAX;
                                }
                            }
                        }
                    }
                }
            }
        }
        void TryGiveWaterToUnitsAroundRainy()
        {
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (unitCs[cell_0].HaveUnit)
                {
                    if (unitCs[cell_0].UnitT == UnitTypes.Snowy)
                    {
                        if (!aboutGameC.LessonT.HaveLesson())
                        {
                            s.RainyGiveWaterToUnitsAround(cell_0);
                        }
                    }
                }
            }
        }

        void FireUpdate()
        {
            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (FireC(cell_0).HaveFire)
                {
                    if (unitCs[cell_0].HaveUnit)
                    {
                        if (unitCs[cell_0].UnitT == UnitTypes.Hell)
                        {
                            unitHpCs[cell_0].Health = HpUnitValues.MAX;
                        }
                        else
                        {
                            if (unitCs[cell_0].PlayerT == PlayerTypes.None)
                            {
                                s.AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                s.AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, unitCs[cell_0].PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        buildingCs[cell_0].BuildingT = BuildingTypes.None;


                        FireC(cell_0).HaveFire = false;


                        foreach (var cell_1 in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                        {
                            needForFireNext.Add(cell_1);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!CellC(cell_0).IsBorder)
                {
                    if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        FireC(cell_0).HaveFire = true;
                    }
                }
            }
        }
        void PutOutFireWithClouds()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (CloudC(curCellIdx).IsCenter)
                {
                    FireC(curCellIdx).HaveFire = false;

                    foreach (var item in IdxsAroundCellC(curCellIdx).IdxCellsAroundArray)
                    {
                        FireC(item).HaveFire = false;
                    }
                }
            }
        }
        void BurnAdultForest()
        {
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (FireC(cell_0).HaveFire)
                {
                    s.TryTakeAdultForestResourcesM(ValuesChessy.FIRE_ADULT_FOREST, cell_0);
                }
            }
        }


        #region WaterUnits

        void TakeWaterUnits()
        {
            if (!aboutGameC.LessonT.HaveLesson() || aboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    var speed = 0.01f;
                    if (unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (aboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
                        {
                            if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
                            {
                                _unitWaterCs[cellIdxCurrent].Water -= speed;
                            }
                        }
                        else
                        {
                            _unitWaterCs[cellIdxCurrent].Water -= speed;
                        }
                    }
                }
            }
        }

        void TryGiveWaterToBotUnits()
        {
            if (aboutGameC.GameModeT == GameModeTypes.TrainingOffline)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (unitCs[cellIdxCurrent].PlayerT == PlayerTypes.Second)
                        {
                            _unitWaterCs[cellIdxCurrent].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                        }
                    }
                }
            }
        }
        void GiveWaterToUnitsNearWithRiver()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (RiverC(cellIdxCurrent).HaveRiverNear)
                    {
                        s.TryExecuteAddingUnitAnimationM(cellIdxCurrent);

                        _unitWaterCs[cellIdxCurrent].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
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
                if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Wolf)
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                var cell_0 = (byte)UnityEngine.Random.Range(0, IndexCellsValues.CELLS);

                if (!CellC(cell_0).IsBorder)
                {
                    if (!unitCs[cell_0].HaveUnit && !environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                        {
                            if (unitCs[cell_1].HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            s.SetNewUnitOnCellS.Set(UnitTypes.Wolf, PlayerTypes.None, cell_0);

                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }

        void TryShiftWolf()
        {
            if (UnityEngine.Random.Range(0f, 1f) >= 0.9f)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Wolf && shiftingUnitCs[cellIdxCurrent].WhereNeedShiftIdxCell == 0)
                        {
                            var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                            var idx_1 = CellsByDirectAroundC(cellIdxCurrent).Get((DirectTypes)randDir);

                            if (!CellC(idx_1).IsBorder && !environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.Mountain)
                                && !unitCs[idx_1].HaveUnit)
                            {
                                shiftingUnitCs[cellIdxCurrent].WhereNeedShiftIdxCell = idx_1;
                                shiftingUnitCs[cellIdxCurrent].Distance = 0;


                                break;
                                //UnitSs.CopyUnitFromTo(cellIdxCurrent, idx_1);

                                //_e.UnitE(cellIdxCurrent).ClearEverything();
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}