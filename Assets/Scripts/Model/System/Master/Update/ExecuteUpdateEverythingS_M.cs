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

            TryGivePeople();

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



        void TryFireAroundHellGod()
        {
            //for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            //{
            //    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Hell))
            //    {
            //        foreach (var cellE in _e.AroundCellsE(cellIdxCurrent).CellsAround)
            //        {
            //            if (_e.AdultForestC(cellE).HaveAnyResources)
            //            {
            //                if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
            //                {
            //                    _e.HaveFire(cellE) = true;
            //                }
            //            }
            //        }

            //        if (_e.RiverT(cellIdxCurrent).HaveRiverNear())
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
            //            if (_e.BuildingOnCellT(cellE).Is(BuildingTypes.IceWall))
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