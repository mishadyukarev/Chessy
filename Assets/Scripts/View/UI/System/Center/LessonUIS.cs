using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class LessonUIS : SystemUIAbstract
    {
        readonly CenterGameUIEs _centerUIEs;

        internal LessonUIS(in CenterGameUIEs centerUIEs, in EntitiesModel eMGame) : base(eMGame)
        {
            _centerUIEs = centerUIEs;
        }

        internal override void Sync()
        {
            for (var lessonT = (LessonTypes)1; lessonT < LessonTypes.End; lessonT++)
            {
                _centerUIEs.LessonGOC(lessonT).TrySetActive(lessonT == aboutGameC.LessonType);
            }
        }
    }
}