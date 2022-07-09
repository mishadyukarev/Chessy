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
                    _e.SunC.SecondsForChangingSideSun--;

                    if (_e.SunC.SecondsForChangingSideSun <= 0)
                    {
                        _e.SunC.ToggleNextSunSideT();
                        _e.SunC.SecondsForChangingSideSun = 20;
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
                        if (_e.HaveCloud(currentIdxCell))
                        {
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                _e.HealthTrail(currentIdxCell).Health(dirT) = 0;
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

                            //if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                            //{
                                
                            //}
                        }

                        _e.UnitMainC(cellIdxCurrent).CooldownForAttackAnyUnitInSeconds--;
                    }

                    PutOutFireWithClouds();
                    BurnAdultForest();
                    FireUpdate();
                    TryGiveWaterToUnitsDuringLessons();
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
                    //TryActiveGodsUniqueAbilityEveryUpdate();
                    TrySetDefendWithoutConditionUnits();
                    TryExecuteTruce();

                    

                    GetDataCellsS.GetDataCellsM();

                    _dateTimeLastUpdate = DateTime.Now;
                }

                TryExecuteShiftingUnit();
                TryShiftCloundsOrChangeDirection();
            }
        }

        void TryExecuteShiftingUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var cell_0 = cellIdxCurrent;
                var cell_1 = _e.ShiftingInfoForUnitC(cell_0).WhereNeedShiftIdxCell;

                if (cell_1 != 0)
                {
                    if (_e.ShiftingInfoForUnitC(cell_0).NeedReturnBack)
                    {
                        _e.ShiftingInfoForUnitC(cell_0).Distance -= Time.deltaTime;

                        if (_e.ShiftingInfoForUnitC(cell_0).Distance <= 0)
                        {
                            _e.ShiftingInfoForUnitC(cell_0).WhereNeedShiftIdxCell = 0;
                            _e.ShiftingInfoForUnitC(cell_0).NeedReturnBack = false;
                            _e.ShiftingInfoForUnitC(cell_0).Distance = 0;

                            GetDataCellsS.GetDataCellsM();
                        }

                        else
                        {
                            if (_e.ShiftingInfoForUnitC(cell_0).Distance / _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).HowMany(_e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell) >= 0.3)
                            {
                                if (!_e.UnitT(cell_1).HaveUnit())
                                {
                                    _e.ShiftingInfoForUnitC(cell_0).NeedReturnBack = false;
                                }
                            }
                        }
                    }

                    else
                    {
                        _e.ShiftingInfoForUnitC(cellIdxCurrent).Distance += Time.deltaTime;

                        if (_e.ShiftingInfoForUnitC(cellIdxCurrent).Distance >= _e.HowManyDistanceNeedForShiftingUnitC(cellIdxCurrent).HowMany(_e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell))
                        {
                            if (!_e.UnitT(cell_1).HaveUnit())
                            {
                                ShiftUnitOnOtherCellM(cellIdxCurrent, _e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell);

                                _e.ShiftingInfoForUnitC(cellIdxCurrent).WhereNeedShiftIdxCell = 0;
                                _e.ShiftingInfoForUnitC(cellIdxCurrent).Distance = 0;
                                _e.ShiftingInfoForUnitC(cellIdxCurrent).NeedReturnBack = false;

                                GetDataCellsS.GetDataCellsM();
                            }

                            else
                            {
                                _e.ShiftingInfoForUnitC(cellIdxCurrent).NeedReturnBack = true;
                            }
                        }
                    }


                    var pos_0 = _e.CellE(cell_0).PositionC.Position;
                    var pos_1 = _e.CellE(cell_1).PositionC.Position;

                    var t = _e.ShiftingInfoForUnitC(cell_0).Distance / _e.HowManyDistanceNeedForShiftingUnitC(cell_0).HowMany(cell_1);

                    _e.UnitPossitionOnCellC(_e.SkinInfoUnitC(cell_0).SkinIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);
                }
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
                        if (!_e.ShiftingInfoForUnitC(cellIdxCurrent).IsShiftingUnit)
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
        void TryActiveGodsUniqueAbilityEveryUpdate()
        {
            if (!_e.LessonT.HaveLesson())
            {
                if (_e.Motions % ValuesChessy.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (!_e.IsBorder(cell_0))
                        {
                            if (_e.UnitT(cell_0).HaveUnit())
                            {
                                if (_e.PlayerInfoE(_e.UnitPlayerT(cell_0)).GodInfoC.UnitT.Is(UnitTypes.Snowy))
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
                                    if (!_e.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (_e.PlayerInfoE(playerT).GodInfoC.UnitT.Is(UnitTypes.Elfemale))
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

                        AttackUnitOnCell(HpValues.MAX * 0.05, _e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                    }
                }
            }
        }
        void TryShiftCloundsOrChangeDirection()
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (_e.HaveCloud(curCellIdx))
                {
                    if (_e.IsCenterCloud(curCellIdx))
                    {
                        var directIdxCell = _e.GetIdxCellByDirect(curCellIdx, DistanceFromCellTypes.First, _e.DirectWindT);
                        var directXy = _e.XyCell(directIdxCell);


                        _e.CloudShiftingC(curCellIdx).WhereNeedShiftIdxCell = directIdxCell;

                        var isInSquareNextCell = directXy[0] >= 4 && directXy[0] <= 11 && directXy[1] >= 2 && directXy[1] <= 9;

                        if (isInSquareNextCell)
                        {
                            _e.CloudShiftingC(curCellIdx).Distance += Time.deltaTime * _e.WindC.Speed * 0.25f;

                            if (_e.CloudShiftingC(curCellIdx).Distance >= 1)
                            {
                                var list = new List<byte>();

                                list.Add(curCellIdx);

                                foreach (var cellIdxAround in _e.IdxsCellsAround(curCellIdx, DistanceFromCellTypes.First))
                                {
                                    list.Add(cellIdxAround);
                                }

                                foreach (var cellIdx in list)
                                {
                                    var cellIdxSkin = _e.CloudWhereSkinDataOnCell(cellIdx).SkinIdxCell;
                                    _e.CloudWhereSkinDataOnCell(cellIdxSkin).DataIdxCell = 0;

                                    _e.CloudOnCellE(cellIdx).Dispose();
                                }







                                list.ForEach((byte cellIdx) => _e.CloudOnCellE(cellIdx).Dispose());

                                SetClouds(directIdxCell);
                            }
                        }
                        else
                        {
                            _e.WindC.DirectT = (DirectTypes)UnityEngine.Random.Range(1, (byte)DirectTypes.End);
                        }
                    }

                                        var pos_0 = _e.CellE(curCellIdx).PositionC.Position;
                    var pos_1 = _e.CellE(_e.CloudShiftingC(curCellIdx).WhereNeedShiftIdxCell).PositionC.Position;

                    var t = _e.CloudShiftingC(curCellIdx).Distance;

                    _e.CloudPossitionC(_e.CloudWhereSkinDataOnCell(curCellIdx).SkinIdxCell).Position = Vector3.Lerp(pos_0, pos_1, t);

                }

                
            }
        }
        void TryPoorWaterToCellsWithClounds()
        {
            for (byte currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
            {
                if (_e.HaveCloud(currentIdxCell))
                {
                    if (!_e.MountainC(currentIdxCell).HaveAnyResources)
                    {
                        _e.WaterOnCellC(currentIdxCell).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                    }
                }
            }
        }
        void TryExtractWoodWithWoodcutter()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;
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
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
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
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
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
                                _e.HpUnitC(cellIdxCurrent).Health -= HpValues.MAX * 0.05f;
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
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _e.ResourcesInInventoryC(player).Add(ResourceTypes.Food, EconomyValues.ADDING_FOOD_AFTER_UPDATE);
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= ValuesChessy.PERCENT_FOR_CHANGING_WIND) _e.WindC.Speed = (byte)UnityEngine.Random.Range(1, 4);
        }
        void TryGiveHealthToBots()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.Second))
                        {
                            if (_e.HpUnitC(cellIdxCurrent).Health < HpValues.MAX)
                            {
                                _e.HpUnitC(cellIdxCurrent).Health += HpValues.MAX * 0.05;

                                if (_e.HpUnitC(cellIdxCurrent).Health > HpValues.MAX)
                                {
                                    _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
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
                    if (_e.HpUnitC(cellIdxCurrent).Health < HpValues.MAX)
                    {
                        if (_e.WaterUnit(cellIdxCurrent) > 0)
                        {
                            if (_e.ResourcesInInventory(_e.UnitPlayerT(cellIdxCurrent), ResourceTypes.Food) > 0)
                            {
                                _e.HpUnitC(cellIdxCurrent).Health += HpValues.MAX * 0.05f;

                                if (_e.HpUnitC(cellIdxCurrent).Health > HpValues.MAX)
                                {
                                    _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
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
                            _e.HpUnitC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (_e.UnitPlayerT(cell_0).Is(PlayerTypes.None))
                            {
                                AttackUnitOnCell(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                AttackUnitOnCell(HpValues.FIRE_DAMAGE, _e.UnitPlayerT(cell_0).NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _e.SetBuildingOnCellT(cell_0, BuildingTypes.None);


                        _e.HaveFire(cell_0) = false;


                        foreach (var cell_1 in _e.IdxsCellsAround(cell_0, DistanceFromCellTypes.First))
                        {
                            needForFireNext.Add(cell_1);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!_e.IsBorder(cell_0))
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
                if (_e.HaveCloud(curCellIdx))
                {
                    _e.HaveFire(curCellIdx) = false;
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
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    var speed = 0.01f;

                    _e.WaterUnitC(cellIdxCurrent).Water -= speed;// ValuesChessy.NeedWaterForThirsty(_e.UnitT(cellIdxCurrent));
                }
            }
        }
        void TryGiveWaterToUnitsDuringLessons()
        {
            if (_e.LessonT! >= LessonTypes.Install3WarriorsNextToTheRiver)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        _e.WaterUnitC(cellIdxCurrent).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }
        void TryGiveWaterToBotUnits()
        {
            if (_e.GameModeT == GameModeTypes.TrainingOffline)
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

                if (!_e.IsBorder(cell_0))
                {
                    if (!_e.UnitT(cell_0).HaveUnit() && !_e.MountainC(cell_0).HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cell_1 in _e.IdxsCellsAround(cell_0, DistanceFromCellTypes.First))
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

                            var idx_1 = _e.GetIdxCellByDirect(cellIdxCurrent, DistanceFromCellTypes.First, (DirectTypes)randDir);

                            if (!_e.IsBorder(idx_1) && !_e.MountainC(idx_1).HaveAnyResources
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