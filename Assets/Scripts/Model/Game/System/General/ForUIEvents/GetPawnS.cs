using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class GetPawnS : SystemModel, IClickUI
    {
        internal GetPawnS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

            var curPlayerI = eMG.CurPlayerITC.PlayerT;


            if (!eMG.LessonTC.Is(LessonTypes.TryBuyingHouse, LessonTypes.ClickOpenTown2))
            {
                if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
                {
                    if (eMG.PlayerInfoE(curPlayerI).PawnInfoC.HaveAnyPeopleInCity)
                    {
                        if (eMG.PlayerInfoE(curPlayerI).PawnInfoC.AmountInGame < eMG.PlayerInfoE(curPlayerI).PawnInfoC.MaxAvailable)
                        {
                            eMG.SelectedCell = 0;

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
                            else if (eMG.LessonTC.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                            {

                            }

                            else
                            {

                                sMG.MistakeSs.SetMistakeS.Set(MistakeTypes.NeedBuildingHouses, 0);
                                eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                                eMG.IsSelectedCity = true;
                            }

                        }
                    }
                    else
                    {
                        eMG.SoundAction(ClipTypes.WritePensil).Invoke();

                        sMG.MistakeSs.SetMistakeS.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                        //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }


                }
                else
                {
                    eMG.MistakeTC.MistakeT = MistakeTypes.NeedWaitQueue;
                    eMG.MistakeTimerC.Timer = 0;
                    eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }

            eMG.NeedUpdateView = true;
        }
    }
}