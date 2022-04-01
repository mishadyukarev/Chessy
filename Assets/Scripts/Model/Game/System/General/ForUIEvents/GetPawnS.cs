using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed class GetPawnS : SystemModelGameAbs, IClickUI
    {
        internal GetPawnS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;

            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();

            var curPlayerI = eMG.CurPlayerITC.PlayerT;


            if (!eMG.LessonTC.Is(LessonTypes.BuyingHouse, LessonTypes.ClickOpenTown2))
            {
                if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
                {
                    if (eMG.PlayerInfoE(curPlayerI).PeopleInCity >= 1)
                    {
                        var pawnsInGame = eMG.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                            + eMG.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

                        if (pawnsInGame < eMG.PlayerInfoE(curPlayerI).MaxAvailablePawns)
                        {
                            eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.Pawn;
                            eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                            eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                        }
                        else
                        {
                            if (eMG.LessonTC.LessonT == LessonTypes.SettingPawn)
                            {
                                eMG.LessonTC.SetNextLesson();
                            }
                            else if (eMG.LessonTC.LessonT == LessonTypes.OpeningTown || eMG.LessonTC.LessonT == LessonTypes.BuyingHouse)
                            {

                            }

                            else
                            {

                                eMG.MistakeC.Set(MistakeTypes.NeedBuildingHouses, 0);
                                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
                                eMG.IsSelectedCity = true;
                            }

                        }
                    }
                    else
                    {
                        eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();

                        eMG.MistakeC.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                        //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }


                }
                else
                {
                    eMG.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                    eMG.MistakeC.Timer = 0;
                    eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
                }
            }

            eMG.NeedUpdateView = true;
        }
    }
}