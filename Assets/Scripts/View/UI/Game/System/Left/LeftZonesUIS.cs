using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame _eUI;

        internal LeftZonesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = entsUI;
        }

        internal override void Sync()
        {
            var needActiveCity = false;
            var needActiveEnvironment = false;

            if (e.IsSelectedCity)
            {
                needActiveCity = true;
            }
            else
            {
                if (e.CellsC.IsSelectedCell)
                {
                    if (!e.LessonTC.HaveLesson)
                    {
                        needActiveEnvironment = true;
                    }
                }
            }


            _eUI.LeftEs.CityE(BuildingTypes.House).ParentGOC.SetActive(needActiveCity);
            _eUI.LeftEnvEs.Zone.SetActive(needActiveEnvironment);
        }
    }
}