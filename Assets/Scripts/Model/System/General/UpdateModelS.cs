using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        DateTime _dateTimeLastUpdate;

        public void Update()
        {
            var timeDeltaTime = Time.deltaTime;

            _runs.ForEach((Action action) => action());

            if (PhotonNetwork.IsMasterClient)
            {
                if ((DateTime.Now - _dateTimeLastUpdate).Seconds >= 1)
                {
                    SunC.SecondsForChangingSideSun--;

                    if (SunC.SecondsForChangingSideSun <= 0)
                    {
                        SunC.ToggleNextSunSideT();
                        SunC.SecondsForChangingSideSun = 20;
                    }

                    for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                    {
                        if (GodInfoC(playerT).CooldownInSecondsForNextAppearance > 0)
                        {
                            GodInfoC(playerT).CooldownInSecondsForNextAppearance--;
                        }
                    }

                    for (byte currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
                    {
                        if (CloudC(currentIdxCell).IsCenter)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                _hpTrailCs[currentIdxCell].Health(dirT) = 0;
                            }

                            foreach (var item in _idxsAroundCellCs[currentIdxCell].IdxCellsAroundArray)
                            {
                                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                {
                                    _hpTrailCs[item].Health(dirT) = 0;
                                }
                            }
                        }
                    }


                    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                    {
                        if (_unitCs[cellIdxCurrent].HaveUnit)
                        {
                            for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                            {
                                _cooldownAbilityCs[cellIdxCurrent].Take(abilityT, 1);
                            }

                            _effectsUnitCs[cellIdxCurrent].StunHowManyUpdatesNeedStay--;

                            if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Snowy)
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

                            _unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition++;


                            if (_unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                            {
                                if (_hpUnitCs[cellIdxCurrent].Health >= HpUnitValues.MAX)
                                {
                                    if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
                                    {
                                        if (!_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest) && !_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill))
                                        {
                                            _unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
                                        }
                                    }
                                    else
                                    {
                                        _unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
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
                    DryWaterOnCells();
                    TryExtractFoodWithFarm();
                    TryExtractHillsWithPawns();
                    GiveFoodAfterUpdate();
                    TryExecuteHungry();
                    TryChangeDirectionOfWindRandomly();
                    TryGiveHealthToBots();
                    ToggleConditionUnitsIfTheresFire();
                    TryGiveHealthToUnitsWithRelaxCondition();
                    TryGiveWaterToUnitsAroundRainy();
                    TryActiveGodsUniqueAbilityEveryUpdate();
                    TrySetDefendWithoutConditionUnits();
                    TryExecuteTruce();



                    GetDataCellsS.GetDataCells();

                    _dateTimeLastUpdate = DateTime.Now;
                }

                TryExecuteShiftingUnitS.TryExecute();
                TryShiftCloudsMS.TryShift();
            }
        }



        void TrySetDefendWithoutConditionUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (_unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.None)
                    {
                        if (!_shiftingUnitCs[cellIdxCurrent].IsShifting)
                        {
                            if (_unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition >= 3)
                            {
                                _unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.Protected;
                                _unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition = 0;
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

            if (!AboutGameC.LessonT.HaveLesson())
            {
                if (_secondsForGodsAbilities >= 60)
                {
                    RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_cellCs[cell_0].IsBorder) continue;

                        if (_unitCs[cell_0].HaveUnit)
                        {
                            if (PlayerInfoE(_unitCs[cell_0].PlayerType).GodInfoC.UnitType.Is(UnitTypes.Snowy))
                            {
                                if (_unitCs[cell_0].UnitT == UnitTypes.Pawn)
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
                            if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                if (!_buildingCs[cell_0].HaveBuilding)
                                {
                                    if (!AboutGameC.HaveTreeUnitInGame)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                SetNewUnitOnCellS.Set(UnitTypes.Tree, playerT, cell_0);

                                                _environmentCs[cell_0].Set(EnvironmentTypes.AdultForest, 0);
                                                _buildingCs[cell_0].BuildingT = BuildingTypes.None;

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
                                if (_cellCs[curCell_0].IsBorder) continue;

                                if (_environmentCs[curCell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    if (_unitCs[curCell_0].HaveUnit && _unitCs[curCell_0].PlayerType != playerT)
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
                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (_fireCs[cellIdxCurrent].HaveFire)
                    {
                        _unitCs[cellIdxCurrent].ConditionT = ConditionUnitTypes.None;
                    }
                }
            }
        }
        void TryAttackUnitsWithoutWater()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitCs[cellIdxCurrent].HaveUnit && !_unitCs[cellIdxCurrent].IsAnimal)
                {
                    if (_unitWaterCs[cellIdxCurrent].Water <= 0)
                    {
                        //var percent = Time.deltaTime;// HpValues.ThirstyPercent(_unitCs[cellIdxCurrent));

                        AttackUnitOnCell(HpUnitValues.MAX * 0.05, _unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
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
                    if (!_environmentCs[curCellIdx].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        _environmentCs[curCellIdx].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }

                    foreach (var item in _idxsAroundCellCs[curCellIdx].IdxCellsAroundArray)
                    {
                        if (!_environmentCs[item].HaveEnvironment(EnvironmentTypes.Mountain))
                        {
                            _environmentCs[item].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        }
                    }
                }
            }
        }
        void TryExtractWoodWithWoodcutter()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_cellCs[cellIdxCurrent].IsBorder) continue;
                if (_buildingCs[cellIdxCurrent].BuildingT != BuildingTypes.Woodcutter) continue;

                GetDataCellsS.GetWoodcutterExtractCells(cellIdxCurrent);

                var extract = _extractionBuildingCs[cellIdxCurrent].HowManyWoodcutterCanExtractWood;

                ResourcesInInventoryC(_buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, (float)extract);
                TryTakeAdultForestResourcesM((float)extract, cellIdxCurrent);

                if (!_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _buildingCs[cellIdxCurrent].BuildingT = BuildingTypes.None;

                    if (AboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                    {
                        if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            AboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
                        }
                    }
                }
            }
        }
        void FeedUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (!AboutGameC.LessonT.HaveLesson() || AboutGameC.LessonT >= LessonTypes.Build1Farms)
                    {
                        if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
                            ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Subtract(ResourceTypes.Food, EconomyValues.FOOD_FOR_FEEDING_ONE_UNIT_AFTER_EVERY_UPDATE);
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

                    ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, extract);
                    TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        if (_buildingCs[cellIdxCurrent].BuildingT != BuildingTypes.Woodcutter)
                        {
                            if (_unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition >= 10)
                            {
                                Build(BuildingTypes.Woodcutter, LevelTypes.First, _unitCs[cellIdxCurrent].PlayerT, cellIdxCurrent);

                                if (AboutGameC.LessonT == LessonTypes.RelaxExtractPawn) SetNextLesson();

                                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.BuildingWoodcutterWithWarrior);
                            }
                        }
                    }
                    else
                    {
                        //_e.ClearBuildingOnCell(cellIdxCurrent);

                        if (AboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                AboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
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
                if (_riverCs[cellIdxCurrent].HaveRiverNear)
                {
                    if (!_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        _environmentCs[cellIdxCurrent].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                    }
                }
            }
        }
        void DryWaterOnCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer))
                {
                    _environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Fertilizer) -= ValuesChessy.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }
            }
        }
        void TryExtractFoodWithFarm()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_extractionBuildingCs[cellIdxCurrent].CanFarmExtact)
                {
                    var extract = _extractionBuildingCs[cellIdxCurrent].HowManyFarmCanExtractFood;

                    ResourcesInInventoryC(_buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Food, (float)extract);
                    _environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Fertilizer) -= extract;

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
                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                    amountAdultForest++;
            }

            var can = !PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= ValuesChessy.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                TruceS.ExecuteTruce();
            }
        }
        void TryExecuteHungry()
        {
            if (!AboutGameC.LessonT.HaveLesson() || AboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (ResourcesInInventoryC(playerT).ResourcesRef(res) < 0)
                    {
                        ResourcesInInventoryC(playerT).Set(res, 0);

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && _unitCs[cellIdxCurrent].PlayerT == playerT)
                            {
                                _hpUnitCs[cellIdxCurrent].Health -= HpUnitValues.MAX * 0.05f;
                                if (_hpUnitCs[cellIdxCurrent].Health <= 0)
                                {
                                    KillUnit(_unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
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

                    _environmentCs[cellIdxCurrent].ResourcesRef(EnvironmentTypes.Hill) -= extract;
                    ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Ore, extract);

                    if (AboutGameC.LessonT.Is(LessonTypes.ExtractHill))
                    {
                        SetNextLesson();

                        if (AboutGameC.IsSelectedCity)
                        {
                            SetNextLesson();
                        }
                    }
                }
            }
        }
        void GiveFoodAfterUpdate()
        {
            if (!AboutGameC.LessonT.HaveLesson() || AboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    ResourcesInInventoryC(player).Add(ResourceTypes.Food, EconomyValues.ADDING_FOOD_AFTER_UPDATE);
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= ValuesChessy.PERCENT_FOR_CHANGING_WIND) WindC.Speed = (byte)UnityEngine.Random.Range(1, 4);
        }
        void TryGiveHealthToBots()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (AboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_unitCs[cellIdxCurrent].PlayerT == PlayerTypes.Second)
                        {
                            if (_hpUnitCs[cellIdxCurrent].Health < HpUnitValues.MAX)
                            {
                                _hpUnitCs[cellIdxCurrent].Health += HpUnitValues.MAX * 0.05;

                                if (_hpUnitCs[cellIdxCurrent].Health > HpUnitValues.MAX)
                                {
                                    _hpUnitCs[cellIdxCurrent].Health = HpUnitValues.MAX;
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
                if (_unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                {
                    if (_hpUnitCs[cellIdxCurrent].Health < HpUnitValues.MAX)
                    {
                        if (_unitWaterCs[cellIdxCurrent].HaveAnyWater)
                        {
                            if (ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).ResourcesRef(ResourceTypes.Food) > 0)
                            {
                                _hpUnitCs[cellIdxCurrent].Health += HpUnitValues.MAX * 0.05f;

                                if (_hpUnitCs[cellIdxCurrent].Health > HpUnitValues.MAX)
                                {
                                    _hpUnitCs[cellIdxCurrent].Health = HpUnitValues.MAX;
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
                if (_unitCs[cell_0].HaveUnit)
                {
                    if (_unitCs[cell_0].UnitT == UnitTypes.Snowy)
                    {
                        if (!AboutGameC.LessonT.HaveLesson())
                        {
                            RainyGiveWaterToUnitsAround(cell_0);
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
                if (_fireCs[cell_0].HaveFire)
                {
                    if (_unitCs[cell_0].HaveUnit)
                    {
                        if (_unitCs[cell_0].UnitT == UnitTypes.Hell)
                        {
                            _hpUnitCs[cell_0].Health = HpUnitValues.MAX;
                        }
                        else
                        {
                            if (_unitCs[cell_0].PlayerT == PlayerTypes.None)
                            {
                                AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, _unitCs[cell_0].PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        _buildingCs[cell_0].BuildingT = BuildingTypes.None;


                        _fireCs[cell_0].HaveFire = false;


                        foreach (var cell_1 in _idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                        {
                            needForFireNext.Add(cell_1);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!_cellCs[cell_0].IsBorder)
                {
                    if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        _fireCs[cell_0].HaveFire = true;
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
                    _fireCs[curCellIdx].HaveFire = false;

                    foreach (var item in _idxsAroundCellCs[curCellIdx].IdxCellsAroundArray)
                    {
                        _fireCs[item].HaveFire = false;
                    }
                }
            }
        }
        void BurnAdultForest()
        {
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (_fireCs[cell_0].HaveFire)
                {
                    TryTakeAdultForestResourcesM(ValuesChessy.FIRE_ADULT_FOREST, cell_0);
                }
            }
        }


        #region WaterUnits

        void TakeWaterUnits()
        {
            if (!AboutGameC.LessonT.HaveLesson() || AboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    var speed = 0.01f;
                    if (_unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (AboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
                        {
                            if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn)
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
            if (AboutGameC.GameModeT == GameModeTypes.TrainingOffline)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (_unitCs[cellIdxCurrent].PlayerT == PlayerTypes.Second)
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
                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    if (_riverCs[cellIdxCurrent].HaveRiverNear)
                    {
                        TryExecuteAddingUnitAnimationM(cellIdxCurrent);

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
                if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Wolf)
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                var cell_0 = (byte)UnityEngine.Random.Range(0, IndexCellsValues.CELLS);

                if (!_cellCs[cell_0].IsBorder)
                {
                    if (!_unitCs[cell_0].HaveUnit && !_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in _idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                        {
                            if (_unitCs[cell_1].HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            SetNewUnitOnCellS.Set(UnitTypes.Wolf, PlayerTypes.None, cell_0);

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
                    if (_unitCs[cellIdxCurrent].HaveUnit)
                    {
                        if (_unitCs[cellIdxCurrent].UnitT == UnitTypes.Wolf && _shiftingUnitCs[cellIdxCurrent].WhereNeedShiftIdxCell == 0)
                        {
                            var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                            var idx_1 = _cellsByDirectAroundC[cellIdxCurrent].Get((DirectTypes)randDir);

                            if (!_cellCs[idx_1].IsBorder && !_environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.Mountain)
                                && !_unitCs[idx_1].HaveUnit)
                            {
                                _shiftingUnitCs[cellIdxCurrent].WhereNeedShiftIdxCell = idx_1;
                                _shiftingUnitCs[cellIdxCurrent].Distance = 0;


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