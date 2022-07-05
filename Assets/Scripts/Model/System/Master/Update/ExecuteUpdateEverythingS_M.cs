using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System.Linq;
namespace Chessy.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModelAbstract
    {
        internal ExecuteUpdateEverythingMS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {

        }



        internal void Execute()
        {
            _e.Motions++;
            _e.SunC.ToggleNextSunSideT();

            TryPutOutFireWithClouds();
            BurnAdultForest();
            FireUpdate();
            TryGivePeople();

            TryChangeDirectionOfWindRandomly();
            //TryShiftWolf();
            FeedUnits();
            TryGiveHealthToBots();
            TryGiveWaterAroundRiverToCells();
            DryWaterOnCells();
            TryExtractFoodWithFarm();
            //TryExtractWoodWithWoodcutter();
            GiveHealthToUnitsWithRelaxCondition();

            TryGiveWaterToUnitsAroundRainy();

            TryFireAroundHellGod();
            ToggleConditionUnitsIfTheresFire();
            TrySetDefendConditionUnits();
            //TryExtractForestWithPawn();
            TryExtractHillsWithPawns();
            TryChangeRelaxConditionPawns();
            GiveFoodAfterUpdate();
            TryExecuteHungry();
            //TrySpawnWolf();
            TryActiveGodsUniqueAbilityEveryUpdate();
            TrySkipLessonWithRiver();
            TryExecuteAI();
            RefreshStepsAll();

            TryExecuteTruce();
        }

        void TryActiveGodsUniqueAbilityEveryUpdate()
        {
            if (!_e.LessonT.HaveLesson())
            {
                if (_e.Motions % ValuesChessy.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    _s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);

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
                                            _e.UnitEffectsC(cell_0).ShootsFrozenArrawArcher++;
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
                                                _s.SetNewUnitOnCellS(UnitTypes.Tree, playerT, cell_0);

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

        void TryGivePeople()
        {
            if (_e.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
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
                _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Truce);

                _e.ExecuteTruce();
            }
        }
        void TrySkipLessonWithRiver()
        {

            if (_e.LessonT == LessonTypes.Install3WarriorsNextToTheRiver)
            {
                var amountUnitsNearRiverForLesson = 0;

                for (byte cellIdx0 = 0; cellIdx0 < IndexCellsValues.CELLS; cellIdx0++)
                {
                    if (_e.UnitT(cellIdx0) == UnitTypes.Pawn && _e.UnitPlayerT(cellIdx0) == PlayerTypes.First && _e.RiverT(cellIdx0).HaveRiverNear())
                    {
                        amountUnitsNearRiverForLesson++;
                    }
                }

                if (amountUnitsNearRiverForLesson >= 3)
                {
                     _s.SetNextLesson();
                }
            }
        }
        void TryChangeDirectionOfWindRandomly()
        {
            if (UnityEngine.Random.Range(0f, 1f) > ValuesChessy.PERCENT_FOR_CHANGING_WIND) _e.WindC.Speed = (byte)UnityEngine.Random.Range(1, 4);
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
                            _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                        }
                    }
                }
            }
        }
        void RefreshStepsAll()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _e.EnergyUnitC(cellIdxCurrent).Energy = StepValues.MAX;
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

        void GiveHealthToUnitsWithRelaxCondition()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                {
                    _e.HpUnitC(cellIdxCurrent).Health = HpValues.MAX;
                }
            }
        }



        void TryFireAroundHellGod()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_e.AdultForestC(cellE).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                _e.HaveFire(cellE) = true;
                            }
                        }
                    }

                    if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (_e.AroundCellsE(_e.CenterCloudCellIdx).CellsAround.Any(cell => cell == cellIdxCurrent))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
                    {
                        if (_e.BuildingOnCellT(cellE).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
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
        void TrySetDefendConditionUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (!_e.UnitConditionT(cellIdxCurrent).HaveCondition())
                    {
                        if (_e.EnergyUnit(cellIdxCurrent) >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                        {
                            _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
                            _e.EnergyUnitC(cellIdxCurrent).Energy -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
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
                         _s.SetNextLesson();

                        if (_e.IsSelectedCity)
                        {
                            _s.SetNextLesson();
                        }
                    }
                }
            }
        }
        void TryChangeRelaxConditionPawns()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (!_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractHill && !_e.ExtactionResourcesWithWarriorC(cellIdxCurrent).CanExtractAdultForest)
                {
                    if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed)
                        && _e.HpUnitC(cellIdxCurrent).Health >= HpValues.MAX)
                    {
                        _e.SetUnitConditionT(cellIdxCurrent, ConditionUnitTypes.Protected);
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
                                _s.KillUnit(_e.UnitPlayerT(cellIdxCurrent).NextPlayer(), cellIdxCurrent);
                                _e.UnitE(cellIdxCurrent).ClearEverything();

                                break;
                            }
                        }
                    }
                }
            }
        }
        void TryExecuteAI()
        {
            //if (!_eMG.LessonTC.HaveLesson)
            //{
            //    _aIBotS.Execute();
            //}
        }
    }
}