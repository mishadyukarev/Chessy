using Chessy.Model.Entity;
namespace Chessy.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModelAbstract
    {
        internal ExecuteUpdateEverythingMS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {

        }



        internal void Execute()
        {
            //_e.Motions++;

            //TryGivePeople();

            //TryChangeDirectionOfWindRandomly();
            //TryShiftWolf();
            //FeedUnits();
            //TryGiveHealthToBots();
            //TryGiveWaterAroundRiverToCells();
            //DryWaterOnCells();
            //TryExtractFoodWithFarm();
            //TryExtractWoodWithWoodcutter();
            //GiveHealthToUnitsWithRelaxCondition();

            //TryGiveWaterToUnitsAroundRainy();

            TryFireAroundHellGod();
            //ToggleConditionUnitsIfTheresFire();
            //TrySetDefendConditionUnits();
            //TryExtractForestWithPawn();
            //TryExtractHillsWithPawns();
            //TryChangeRelaxConditionPawns();
            //GiveFoodAfterUpdate();
            //TryExecuteHungry();
            //TrySpawnWolf();
            //TryActiveGodsUniqueAbilityEveryUpdate();
            //TrySkipLessonWithRiver();
            //TryExecuteAI();
            //RefreshStepsAll();

            //TryExecuteTruce();
        }



        //void TryGivePeople()
        //{
        //    if (_e.Motions % 5 == 0)
        //    {
        //        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
        //        {
        //            PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
        //        }
        //    }
        //}
        //void TrySkipLessonWithRiver()
        //{

        //    if (_aboutGameC.LessonT == LessonTypes.Install1WarriorsNextToTheRiver)
        //    {
        //        var amountUnitsNearRiverForLesson = 0;

        //        for (byte cellIdx0 = 0; cellIdx0 < IndexCellsValues.CELLS; cellIdx0++)
        //        {
        //            if (_unitCs[cellIdx0) == UnitTypes.Pawn && _unitCs[cellIdx0) == PlayerTypes.First && _riverCs[cellIdx0).HaveRiverNear())
        //            {
        //                amountUnitsNearRiverForLesson++;
        //            }
        //        }

        //        if (amountUnitsNearRiverForLesson >= 3)
        //        {
        //             _s.SetNextLesson();
        //        }
        //    }
        //}



        void TryFireAroundHellGod()
        {
            //for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            //{
            //    if (_unitCs[cellIdxCurrent).Is(UnitTypes.Hell))
            //    {
            //        foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
            //        {
            //            if (_environmentCs[cellE].HaveEnvironment(EnvironmentTypes.AdultForest))
            //            {
            //                if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
            //                {
            //                    _fireCs[cellE) = true;
            //                }
            //            }
            //        }

            //        if (_riverCs[cellIdxCurrent).HaveRiverNear())
            //        {
            //            //Es.UnitE(cell_0).Take(Es, 0.15f);
            //        }

            //        if (_e.AroundCellsE(_e.CenterCloudCellIdx).CellsAround.Any(cell => cell == cellIdxCurrent))
            //        {
            //            //Es.UnitE(cell_0).Take(Es, 0.15f);
            //            break;
            //        }

            //        foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
            //        {
            //            if (_buildingCs[cellE).Is(BuildingTypes.IceWall))
            //            {
            //                //Es.UnitE(cell_0).Take(Es, 0.15f);
            //                break;
            //            }
            //        }
            //    }
            //}
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