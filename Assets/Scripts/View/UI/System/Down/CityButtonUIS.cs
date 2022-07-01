using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class CityButtonUIS : SystemUIAbstract
    {
        readonly CityButtonUIE _cityButtonUIE;

        internal CityButtonUIS(in CityButtonUIE cityButtonUIE, in EntitiesModel eMG) : base(eMG)
        {
            _cityButtonUIE = cityButtonUIE;
        }

        internal override void Sync()
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.OpeningTown)
            {
                _cityButtonUIE.ParentGOC.SetActive(true);
            }
            else
            {
                _cityButtonUIE.ParentGOC.SetActive(false);
            }
        }
    }
}