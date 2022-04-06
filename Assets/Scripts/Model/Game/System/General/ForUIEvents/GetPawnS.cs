using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;

namespace Chessy.Game.Model.System
{
    public sealed class GetPawnS : SystemModel, IClickUI
    {
        internal GetPawnS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;

            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();

            var curPlayerI = eMG.CurPlayerITC.PlayerT;


            if (!eMG.LessonTC.Is(LessonTypes.BuyingHouse, LessonTypes.ClickOpenTown2))
            {
                if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
                {
                    if (eMG.PlayerInfoE(curPlayerI).PawnInfoE.PeopleInCityC.HaveAny)
                    {
                        if (eMG.PlayerInfoE(curPlayerI).PawnInfoE.PawnsInGame < eMG.PlayerInfoE(curPlayerI).PawnInfoE.MaxAvailable)
                        {
                            eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.Pawn;
                            eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                            eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                        }
                        else
                        {
                            if (eMG.LessonTC.Is(LessonTypes.SettingPawn))
                            {
                                eMG.LessonTC.SetNextLesson();
                            }
                            else if (eMG.LessonTC.Is(LessonTypes.OpeningTown, LessonTypes.BuyingHouse))
                            {

                            }

                            else
                            {

                                sMG.SetMistakeS.Set(MistakeTypes.NeedBuildingHouses, 0);
                                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
                                eMG.IsSelectedCity = true;
                            }

                        }
                    }
                    else
                    {
                        eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();

                        sMG.SetMistakeS.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                        //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }


                }
                else
                {
                    eMG.MistakeTC.MistakeT = MistakeTypes.NeedWaitQueue;
                    eMG.MistakeTimerC.Timer = 0;
                    eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
                }
            }

            eMG.NeedUpdateView = true;
        }
    }
}