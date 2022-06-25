using Chessy.Model;
using Chessy.Model.View.UI.Entity;

namespace Chessy.Model
{
    sealed class SkipLessonUIS : SystemUIAbstract
    {
        readonly SkipLessonUIE _skipLessonUIE;

        internal SkipLessonUIS(in SkipLessonUIE skipLessonUIE, in EntitiesModel eMG) : base(eMG)
        {
            _skipLessonUIE = skipLessonUIE;
        }

        internal override void Sync()
        {
            _skipLessonUIE.ButtonUIC.SetActiveParent(_e.LessonT.HaveLesson());
        }
    }
}