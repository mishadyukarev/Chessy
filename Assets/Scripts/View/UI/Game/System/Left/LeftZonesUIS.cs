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
            _eUI.LeftEs.CityE(BuildingTypes.House).ParentGOC.SetActive(false);
            _eUI.LeftEnvEs.Zone.SetActive(false);

            if (e.IsSelectedCity)
            {
                _eUI.LeftEs.CityE(BuildingTypes.House).ParentGOC.SetActive(true);
            }
            else
            {
                var idx_sel = e.CellsC.Selected;

                if (e.CellsC.IsSelectedCell)
                {
                    if (!e.LessonTC.HaveLesson)
                    {
                        _eUI.LeftEnvEs.Zone.SetActive(true);
                    }
                }
            }
        }
    }
}