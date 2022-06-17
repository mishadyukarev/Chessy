using Chessy.Common.Enum;
using Chessy.Game.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetPawn()
        {
            _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

            var curPlayerI = _eMG.CurPlayerITC.PlayerT;


            if (!_eMG.LessonTC.Is(LessonTypes.TryBuyingHouse, LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            {
                if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerTC.PlayerT))
                {
                    if (_eMG.PlayerInfoE(curPlayerI).PawnInfoC.HaveAnyPeopleInCity)
                    {
                        if (_eMG.PlayerInfoE(curPlayerI).PawnInfoC.AmountInGame < _eMG.PlayerInfoE(curPlayerI).PawnInfoC.MaxAvailable)
                        {
                            _eMG.SelectedCell = 0;

                            _eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.Pawn;
                            _eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                            _eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                        }
                        else
                        {
                            if (_eMG.LessonTC.Is(LessonTypes.SettingPawn))
                            {
                                _eMG.LessonTC.SetNextLesson();
                            }
                            else if (_eMG.LessonTC.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                            {

                            }

                            else
                            {

                                _sMG.MistakeSs.SetMistakeS.Set(MistakeTypes.NeedBuildingHouses, 0);
                                _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                                _eMG.IsSelectedCity = true;
                            }

                        }
                    }
                    else
                    {
                        _eMG.SoundAction(ClipTypes.WritePensil).Invoke();

                        _sMG.MistakeSs.SetMistakeS.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                        //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }


                }
                else
                {
                    _eMG.MistakeTC.MistakeT = MistakeTypes.NeedWaitQueue;
                    _eMG.MistakeTimerC.Timer = 0;
                    _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }

            _eMG.NeedUpdateView = true;
        }
    }
}