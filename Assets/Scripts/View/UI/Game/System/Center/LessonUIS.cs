using Chessy.Common.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;

namespace Chessy.Game.View.UI.System
{
    public struct LessonUIS : IEcsRunSystem
    {
        readonly CenterUIEs _centerUIEs;
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesModelGame _eMGame;

        public LessonUIS(in CenterUIEs centerUIEs, in EntitiesModelCommon eMCommon, in EntitiesModelGame eMGame)
        {
            _centerUIEs = centerUIEs;
            _eMCommon = eMCommon;
            _eMGame = eMGame;
        }

        public void Run()
        {
            var whoseMove = _eMGame.WhoseMove.PlayerT;


            for (var lessonT = (LessonTypes)1; lessonT < LessonTypes.End; lessonT++)
            {
                _centerUIEs.LessonGOC(lessonT).SetActive(lessonT == _eMGame.LessonTC.LessonT);
            }

            _centerUIEs.BookGuidSelectionE.ParentGOVC.SetActive(_eMCommon.BookE.IsOpenedBook);
            //_centerUIEs.SettingUnitLessonE.ParengGOVC.SetActive(_eMGame.LessonTC.LessonT == LessonTypes.SettingKing);
            //_centerUIEs.GettingUnitLessonE.ParentGOC.SetActive(_eMGame.LessonTC.LessonT == LessonTypes.PickGod);
            //_centerUIEs.SettingPawnE.ParentGOC.SetActive(_eMGame.LessonTC.LessonT == LessonTypes.SettingPawn);
            //_centerUIEs.SettingGodLessonE.ParentGOC.SetActive(_eMGame.LessonTC.LessonT == LessonTypes.SettingGod);
        }
    }
}