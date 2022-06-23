﻿using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class LeftZonesUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI _eUI;

        internal LeftZonesUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            _eUI = entsUI;
        }

        internal override void Sync()
        {
            var needActiveCity = false;
            var needActiveEnvironment = false;

            if (_e.IsSelectedCity)
            {
                needActiveCity = true;
            }
            else
            {
                if (_e.CellsC.IsSelectedCell)
                {
                    if (!_e.LessonT.HaveLesson())
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