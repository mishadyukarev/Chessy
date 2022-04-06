using Chessy.Game.Model.Entity;
using Chessy.Game.View.UI.Entity;

namespace Chessy.Game
{
    sealed class SkipLessonUIS : SystemUIAbstract
    {
        readonly SkipLessonUIE _skipLessonUIE;

        internal SkipLessonUIS(in SkipLessonUIE skipLessonUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _skipLessonUIE = skipLessonUIE;
        }

        internal override void Sync()
        {
            _skipLessonUIE.ButtonUIC.SetActiveParent(e.LessonTC.HaveLesson);
        }
    }
}