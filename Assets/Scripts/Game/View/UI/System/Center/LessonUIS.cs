using Chessy.Common.Entity;
using Chessy.Game.Entity.Model;
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
            _centerUIEs.BookGuidSelectionE.ParentGOVC.SetActive(_eMCommon.BookE.IsOpenedBook);
            _centerUIEs.SettingUnitLessonE.ParengGOVC.SetActive(_eMGame.LessonTC.LessonT == LessonTypes.SettingKing);
        }
    }
}