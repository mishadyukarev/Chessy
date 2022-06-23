using Chessy.Model.Entity.View.UI.Down;
using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;

namespace Chessy.Model
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