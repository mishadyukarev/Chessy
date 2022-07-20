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
                        if (_e.IsCenterCloud(currentIdxCell))
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                _e.HealthTrail(currentIdxCell).Health(dirT) = 0;
                            }

                            foreach (var item in _e.IdxsCellsAround(currentIdxCell))
                            {
                                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                {
                                    _e.HealthTrail(item).Health(dirT) = 0;
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
                                _e.UnitCooldownAbilitiesC(cellIdxCurrent).Take(abilityT, 1);
                            }

                            _e.UnitEffectsC(cellIdxCurrent).StunHowManyUpdatesNeedStay--;

                            if (_e.UnitT(cellIdxCurrent) == UnitTypes.Snowy)
                            {
                                if (!_e.UnitEffectsC(cellIdxCurrent).HaveFrozenArrawArcher)
                                {
                                    _e.UnitEffectsC(cellIdxCurrent).SecondForSnowyFrozenArraw--;

                                    if (_e.UnitEffectsC(cellIdxCurrent).SecondForSnowyFrozenArraw <= 0)
                                    {
                                        _e.UnitEffectsC(cellIdxCurrent).HaveFrozenArrawArcher = true;
                                        _e.UnitEffectsC(cellIdxCurrent).SecondForSnowyFrozenArraw = 5;
                                    }
                                }
                            }

                            _e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInThisCondition++;


                            if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                            {
                                if (_e.HpUnit(cellIdxCurrent) >= HpUnitValues.MAX)
                                {
                                    if (_e.UnitT(cellIdxCurrent) == UnitTypes.Pawn)
                                    {
                                        if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources && !_e.HillC(cellIdxCurrent).HaveAnyResources)
                                        {
                                            _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                                        }
                                    }
                                    else
                                    {
                                        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                                    }

                                }

                            }
                        }

                        _e.UnitMainC(cellIdxCurrent).CooldownForAttackAnyUnitInSeconds--;
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
                    if (_e.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.None)
                    {
                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent).IsShifting)
                        {
                            if (_e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInThisCondition >= 3)
                            {
                                _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                                _e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInThisCondition = 0;
                            }
                        }
                    }
                }

                //if (!_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractHill && !_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                //{
                //    if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                //        && _e.HpUnitC(cellIdxCurrent).Health >= HpValues.MAX)
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

            if (!_e.LessonT.HaveLesson())
            {
                if(_secondsForGodsAbilities >= 60)
                {
                    RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_cellCs[cell_0].IsBorder) continue;

                        if (_e.UnitT(cell_0).HaveUnit())
                        {
                            if (_e.PlayerInfoE(_e.UnitPlayerT(cell_0)).GodInfoC.UnitType.Is(UnitTypes.Snowy))
                            {
                                if (_e.UnitT(cell_0).Is(UnitTypes.Pawn))
                                {
                                    if (_e.MainToolWeaponT(cell_0).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                                    {
                                        _e.UnitEffectsC(cell_0).HaveFrozenArrawArcher = true;
                                    }
                                    else
                                    {
                                        _e.UnitEffectsC(cell_0).ProtectionRainyMagicShield = ValuesChessy.PROTECTION_MAGIC_SHIELD_AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                                else
                                {
                                    _e.UnitEffectsC(cell_0).ProtectionRainyMagicShield = ValuesChessy.PROTECTION_MAGIC_SHIELD_AFTER_5_MOTIONS_RAINY;
                                }
                            }
                        }
                        else
                        {
                            if (_e.AdultForestC(cell_0).HaveAnyResources)
                            {
                                if (!_e.BuildingOnCellT(cell_0).HaveBuilding())
                                {
                                    if (!_e.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (_e.PlayerInfoE(playerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

                                                _e.AdultForestC(cell_0).Resources = 0;
                                                _e.SetBuildingOnCellT(cell_0, BuildingTypes.None);

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
                                    if (_e.UnitT(curCell_0).HaveUnit() && _e.UnitPlayerT(curCell_0) != playerT)
                                    {
                                        _e.UnitEffectsC(curCell_0).StunHowManyUpdatesNeedStay = StunUnitValues.AFTER_ELFEMALE_BLOWOUT;
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
                    if (_e.HaveFire(cellIdxCurrent))
                    {
                        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.None);
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
                    if (_e.WaterUnitC(cellIdxCurrent).Water <= 0)
                    {
                        //var percent = Time.deltaTime;// HpValues.ThirstyPercent(_e.UnitT(cellIdxCurrent));

                        AttackUnitOnCell(HpUnitValues.MAX * 0.05, _e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                    }
                }
            }
        }

        void TryPoorWaterToCellsWithClounds()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (_e.IsCenterCloud(curCellIdx))
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
                if (_e.BuildingOnCellT(cellIdxCurrent) != BuildingTypes.Woodcutter) continue;

                GetDataCellsS.GetWoodcutterExtractCells(cellIdxCurrent);

                var extract = _e.WoodcutterExtract(cellIdxCurrent);

                _e.ResourcesInInventoryC(_e.BuildingPlayerT(cellIdxCurrent)).Add(ResourceTypes.Wood, extract);
                TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                if (!_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    _e.SetBuildingOnCellT(cellIdxCurrent, BuildingTypes.None);

                    if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                    {
                        if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                        {
                            _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
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
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build1Farms)
                    {
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                            _e.ResourcesInInventoryC(_e.UnitPlayerT(cellIdxCurrent)).Subtract(ResourceTypes.Food, EconomyValues.FOOD_FOR_FEEDING_ONE_UNIT_AFTER_EVERY_UPDATE);
                    }
                }
            }
        }
        void TryExtractForestWithPawn()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                {
                    var extract = _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractAdultForest;

                    _e.ResourcesInInventoryC(_e.UnitPlayerT(cellIdxCurrent)).Add(ResourceTypes.Wood, extract);
                    TryTakeAdultForestResourcesM(extract, cellIdxCurrent);

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        if (!_e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Woodcutter))
                        {
                            if (_e.UnitMainC(cellIdxCurrent).HowManySecondUnitWasHereInThisCondition >= 10)
                            {
                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _e.UnitPlayerT(cellIdxCurrent), 1, cellIdxCurrent);

                                if (_e.LessonT == LessonTypes.RelaxExtractPawn) SetNextLesson();

                                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.BuildingWoodcutterWithWarrior);
                            }
                        }
                    }
                    else
                    {
                        //_e.ClearBuildingOnCell(cellIdxCurrent);

                        if (_e.LessonT.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdxCurrent == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                _e.LessonT = LessonTypes.RelaxExtractPawn + 1;
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
                if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
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
                if (_e.BuildingExtractionC(cellIdxCurrent).CanFarmExtact)
                {
                    var extract = _e.FarmExtract(cellIdxCurrent);

                    _e.ResourcesInInventoryC(_e.BuildingPlayerT(cellIdxCurrent)).Add(ResourceTypes.Food, extract);
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
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build1Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    var res = ResourceTypes.Food;

                    if (_e.ResourcesInInventory(playerT, res) < 0)
                    {
                        _e.SetResourcesInInventory(playerT, res, 0);

                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.UnitPlayerT(cellIdxCurrent).Is(playerT))
                            {
                                _e.HpUnitC(cellIdxCurrent).Health -= HpUnitValues.MAX * 0.05f;
                                if (_e.HpUnitC(cellIdxCurrent).Health <= 0)
                                {
                                    KillUnit(_e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
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
                if (_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractHill && !_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                {
                    var extract = _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractHill;

                    _e.HillC(cellIdxCurrent).Resources -= extract;
                    _e.ResourcesInInventoryC(_e.UnitPlayerT(cellIdxCurrent)).Add(ResourceTypes.Ore, extract);

                    if (_e.LessonT.Is(LessonTypes.ExtractHill))
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
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build1Farms)
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
                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.Second))
                        {
                            if (_e.HpUnitC(cellIdxCurrent).Health < HpUnitValues.MAX)
                            {
                                _e.HpUnitC(cellIdxCurrent).Health += HpUnitValues.MAX * 0.05;

                                if (_e.HpUnitC(cellIdxCurrent).Health > HpUnitValues.MAX)
                                {
                                    _e.HpUnitC(cellIdxCurrent).Health = HpUnitValues.MAX;
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
                if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                {
                    if (_e.HpUnitC(cellIdxCurrent).Health < HpUnitValues.MAX)
                    {
                        if (_e.WaterUnit(cellIdxCurrent) > 0)
                        {
                            if (_e.ResourcesInInventory(_e.UnitPlayerT(cellIdxCurrent), ResourceTypes.Food) > 0)
                            {
                                _e.HpUnitC(cellIdxCurrent).Health += HpUnitValues.MAX * 0.05f;

                                if (_e.HpUnitC(cellIdxCurrent).Health > HpUnitValues.MAX)
                                {
                                    _e.HpUnitC(cellIdxCurrent).Health = HpUnitValues.MAX;
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
                        if (!_e.LessonT.HaveLesson())
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
                if (_e.HaveFire(cell_0))
                {
                    if (_e.UnitT(cell_0).HaveUnit())
                    {
                        if (_e.UnitT(cell_0).Is(UnitTypes.Hell))
                        {
                            _e.HpUnitC(cell_0).Health = HpUnitValues.MAX;
                        }
                        else
                        {
                            if (_e.UnitPlayerT(cell_0).Is(PlayerTypes.None))
                            {
                                AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                AttackUnitOnCell(HpUnitValues.FIRE_DAMAGE, _e.UnitPlayerT(cell_0).NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _e.SetBuildingOnCellT(cell_0, BuildingTypes.None);


                        _e.HaveFire(cell_0) = false;


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
                        _e.HaveFire(cell_0) = true;
                    }
                }
            }
        }
        void PutOutFireWithClouds()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (_e.IsCenterCloud(curCellIdx))
                {
                    _e.HaveFire(curCellIdx) = false;

                    foreach (var item in _e.IdxsCellsAround(curCellIdx))
                    {
                        _e.HaveFire(item) = false;
                    }
                }
            }
        }
        void BurnAdultForest()
        {
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (_e.HaveFire(cell_0))
                {
                    TryTakeAdultForestResourcesM(ValuesChessy.FIRE_ADULT_FOREST, cell_0);
                }
            }
        }


        #region WaterUnits

        void TakeWaterUnits()
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    var speed = 0.01f;
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.LessonT >= LessonTypes.Install1WarriorsNextToTheRiver)
                        {
                            if (_e.UnitT(cellIdxCurrent) == UnitTypes.Pawn)
                            {
                                _e.WaterUnitC(cellIdxCurrent).Water -= speed;
                            }
                        }
                        else
                        {
                            _e.WaterUnitC(cellIdxCurrent).Water -= speed;
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
                        if (_e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.Second)
                        {
                            _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
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
                    if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                    {
                        TryExecuteAddingUnitAnimationM(cellIdxCurrent);

                        _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
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
                            SetNewUnitOnCellS(UnitTypes.Wolf, PlayerTypes.None, cell_0);

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
                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Wolf) && _e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell == 0)
                        {
                            var randDir = UnityEngine.Random.Range((float)DirectTypes.None + 1, (float)DirectTypes.End);

                            var idx_1 = _e.GetIdxCellByDirectAround(cellIdxCurrent, (DirectTypes)randDir);

                            if (!_cellCs[idx_1].IsBorder && !_e.MountainC(idx_1).HaveAnyResources
                                && !_e.UnitT(idx_1).HaveUnit())
                            {
                                _e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell = idx_1;
                                _e.ShiftingInfoForUnitC(cellIdxCurrent).Distance = 0;


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