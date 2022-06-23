using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;

namespace Chessy.Model.View.UI.System
{
    sealed class LessonUIS : SystemUIAbstract
    {
        readonly CenterUIEs _centerUIEs;

        internal LessonUIS(in CenterUIEs centerUIEs, in EntitiesModel eMGame) : base(eMGame)
        {
            _centerUIEs = centerUIEs;
        }

        internal override void Sync()
        {
            for (var lessonT = (LessonTypes)1; lessonT < LessonTypes.End; lessonT++)
            {
                _centerUIEs.LessonGOC(lessonT).SetActive(lessonT == _e.LessonT);
            }
        }
    }
}