using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class CityButtonUIS : SystemUIAbstract
    {
        bool _needActive;
        readonly CityButtonUIE _cityButtonUIE;

        internal CityButtonUIS(in CityButtonUIE cityButtonUIE, in EntitiesModel eMG) : base(eMG)
        {
            _cityButtonUIE = cityButtonUIE;
        }

        internal override void Sync()
        {
            _needActive = !aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= LessonTypes.OpeningTown;

            _cityButtonUIE.ParentGOC.TrySetActive(_needActive);
        }
    }
}