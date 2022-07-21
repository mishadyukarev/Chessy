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
                    _sunC.SecondsForChangingSideSun--;

                    if (_sunC.SecondsForChangingSideSun <= 0)
                    {
                        _sunC.ToggleNextSunSideT();
                        _sunC.SecondsForChangingSideSun = 20;
                    }

                    for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                    {
                        if (_e.GodInfoC(playerT).CooldownInSecondsForNextAppearance > 0)
                        {
                            _e.GodInfoC(playerT).CooldownInSecondsForNextAppearance--;
                        }
                    }

                    for (byte currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
                    {
                        if (_cloudCs[currentIdxCell].IsCenter)
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                _hpTrailCs[currentIdxCell].Health(dirT) = 0;
                            }

                            foreach (var item in _e.IdxsCellsAround(currentIdxCell))
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
                        if (_e.UnitT(cellIdxCurrent).HaveUnit())
                        {
                            for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                            {
                                _cooldownAbilityCs[cellIdxCurrent].Take(abilityT, 1);
                            }

                            _effectsUnitCs[cellIdxCurrent].StunHowManyUpdatesNeedStay--;

                            if (_e.UnitT(cellIdxCurrent) == UnitTypes.Snowy)
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
                                    if (_e.UnitT(cellIdxCurrent) == UnitTypes.Pawn)
                                    {
                                        if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources && !_e.HillC(cellIdxCurrent).HaveAnyResources)
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

                        _unitCs[cellIdxCurrent].CooldownForAttackAnyUnitInSeconds--;
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
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
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

            if (!_aboutGameC.LessonT.HaveLesson())
            {
                if(_secondsForGodsAbilities >= 60)
                {
                    RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_cellCs[cell_0].IsBorder) continue;

                        if (_e.UnitT(cell_0).HaveUnit())
                        {
                            if (_e.PlayerInfoE(_unitCs[cell_0].PlayerType).GodInfoC.UnitType.Is(UnitTypes.Snowy))
                            {
                                if (_e.UnitT(cell_0).Is(UnitTypes.Pawn))
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
                            if (_e.AdultForestC(cell_0).HaveAnyResources)
                            {
                                if (!_buildingCs[cell_0].HaveBuilding)
                                {
                                    if (!_e.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (_e.PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                SetNewUnitOnCellS.Set(UnitTypes.Tree, playerT, cell_0);

                                                _e.AdultForestC(cell_0).Resources = 0;
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
                        if (_e.PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                        {

                            for (byte curCell_0 = 0; curCell_0 < IndexCellsValues.CELLS; curCell_0++)
                            {
                                if (_cellCs[curCell_0].IsBorder) continue;

                                if (_e.AdultForestC(curCell_0).HaveAnyResources)
                                {
                                    if (_e.UnitT(curCell_0).HaveUnit() && _unitCs[curCell_0].PlayerType != playerT)
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
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
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
                if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                {
                    if (_unitWaterCs[cellIdxCurrent].Water <= 0)
                    {
                        //var percent = Time.deltaTime;// HpValues.ThirstyPercent(_e.UnitT(cellIdxCurrent));

                        AttackUnitOnCell(HpUnitValues.MAX * 0.05, _unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
                    }
                }
            }
        }

        void TryPoorWaterToCellsWithClounds()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (_cloudCs[curCellIdx].IsCenter)
                {
                    if (!_e.MountainC(curCellIdx).HaveAnyResources)
                    {
                        _e.WaterOnCellC(curCellIdx).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                    }

                    foreach (var item in _e.IdxsCellsAround(curCellIdx))
                    {
                        if (!_e.MountainC(item).HaveAnyResources)
                        {
                            _e.WaterOnCellC(item).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
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

                _e.ResourcesInInventoryC(_buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, extract);
                TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    _buildingCs[cellIdxCurrent].BuildingT = BuildingTypes.None;

                    if (_aboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                    {
                        if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            _aboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
                        }
                    }
                }
            }
        }
        void FeedUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (!_aboutGameC.LessonT.HaveLesson() || _aboutGameC.LessonT >= LessonTypes.Build1Farms)
                    {
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                            _e.ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Subtract(ResourceTypes.Food, EconomyValues.FOOD_FOR_FEEDING_ONE_UNIT_AFTER_EVERY_UPDATE);
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

                    _e.ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Wood, extract);
                    TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        if (_buildingCs[cellIdxCurrent].BuildingT != BuildingTypes.Woodcutter)
                        {
                            if (_unitCs[cellIdxCurrent].HowManySecondUnitWasHereInThisCondition >= 10)
                            {
                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _unitCs[cellIdxCurrent].PlayerT, 1, cellIdxCurrent);

                                if (_aboutGameC.LessonT == LessonTypes.RelaxExtractPawn) SetNextLesson();

                                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.BuildingWoodcutterWithWarrior);
                            }
                        }
                    }
                    else
                    {
                        //_e.ClearBuildingOnCell(cellIdxCurrent);

                        if (_aboutGameC.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _aboutGameC.LessonT = LessonTypes.RelaxExtractPawn + 1;
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
                    if (!_e.MountainC(cellIdxCurrent).HaveAnyResources)
                    {
                        _e.WaterOnCellC(cellIdxCurrent).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                    }
                }
            }
        }
        void DryWaterOnCells()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources)
                {
                    _e.WaterOnCellC(cellIdxCurrent).Resources -= ValuesChessy.DRY_FERTILIZE_DURING_UPDATE_TAKING;
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

                    _e.ResourcesInInventoryC(_buildingCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Food, extract);
                    _e.WaterOnCellC(cellIdxCurrent).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
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
                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !_e.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !_e.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= ValuesChessy.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                _e.ExecuteTruce(this);
            }
        }
        void TryExecuteHungry()
        {
            if (!_aboutGameC.LessonT.HaveLesson() || _aboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (_e.ResourcesInInventory(playerT, res) < 0)
                    {
                        _e.SetResourcesInInventory(playerT, res, 0);

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _unitCs[cellIdxCurrent].PlayerT == playerT)
                            {
                                _hpUnitCs[cellIdxCurrent].Health -= HpUnitValues.MAX * 0.05f;
                                if (_hpUnitCs[cellIdxCurrent].Health <= 0)
                                {
                                    KillUnit(_unitCs[cellIdxCurrent].PlayerT.NextPlayer(), cellIdxCurrent);
                                    _e.UnitE(cellIdxCurrent).Dispose();
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

                    _e.HillC(cellIdxCurrent).Resources -= extract;
                    _e.ResourcesInInventoryC(_unitCs[cellIdxCurrent].PlayerT).Add(ResourceTypes.Ore, extract);

                    if (_aboutGameC.LessonT.Is(LessonTypes.ExtractHill))
                    {
                        SetNextLesson();

                        if (_e.IsSelectedCity)
                        {
                            SetNextLesson();
                        }
                    }
                }
            }
        }
        void GiveFoodAfterUpdate()
        {
            if (!_aboutGameC.LessonT.HaveLesson() || _aboutGameC.LessonT >= LessonTypes.Build1Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _e.ResourcesInInventoryC(player).Add(ResourceTypes.Food, EconomyValues.ADDING_FOOD_AFTER_UPDATE);
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= ValuesChessy.PERCENT_FOR_CHANGING_WIND) _windC.Speed = (byte)UnityEngine.Random.Range(1, 4);
        }
        void TryGiveHealthToBots()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_aboutGameC.GameModeT.Is(GameModeTypes.TrainingOffline))
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
                            if (_e.ResourcesInInventory(_unitCs[cellIdxCurrent].PlayerT, ResourceTypes.Food) > 0)
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
                if (_e.UnitT(cell_0).HaveUnit())
                {
                    if (_e.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!_aboutGameC.LessonT.HaveLesson())
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
                    if (_e.UnitT(cell_0).HaveUnit())
                    {
                        if (_e.UnitT(cell_0).Is(UnitTypes.Hell))
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

                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _buildingCs[cell_0].BuildingT = BuildingTypes.None;


                        _fireCs[cell_0].HaveFire = false;


                        foreach (var cell_1 in _e.IdxsCellsAround(cell_0))
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
                    if (_e.AdultForestC(cell_0).HaveAnyResources)
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
                if (_cloudCs[curCellIdx].IsCenter)
                {
                    _fireCs[curCellIdx].HaveFire = false;

                    foreach (var item in _e.IdxsCellsAround(curCellIdx))
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
            if (!_aboutGameC.LessonT.HaveLesson() || _aboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    var speed = 0.01f;
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_aboutGameC.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
                        {
                            if (_e.UnitT(cellIdxCurrent) == UnitTypes.Pawn)
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
            if (_aboutGameC.GameModeT == GameModeTypes.TrainingOffline)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
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
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
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
                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf))
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
                    if (!_e.UnitT(cell_0).HaveUnit() && !_e.MountainC(cell_0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in _e.IdxsCellsAround(cell_0))
                        {
                            if (_e.UnitT(cell_1).HaveUnit())
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
            if(UnityEngine.Random.Range(0f, 1f) >= 0.9f)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf) && _shiftingUnitCs[cellIdxCurrent].WhereNeedShiftIdxCell == 0)
                        {
                            var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                            var idx_1 = _e.GetIdxCellByDirectAround(cellIdxCurrent, (DirectTypes)randDir);

                            if (!_cellCs[idx_1].IsBorder && !_e.MountainC(idx_1).HaveAnyResources
                                && !_e.UnitT(idx_1).HaveUnit())
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