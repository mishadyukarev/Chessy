using Chessy.Game.EventsUI.Center;
using Chessy.Game.EventsUI.Left;
using System;

namespace Chessy.Game.EventsUI
{
    public sealed class EventsUIManager
    {
        public readonly CityEventsUI LeftCityEventUI;
        public readonly BuildingEventsUI CenterBuildingEnventsUI;

        public EventsUIManager(in Action updateView, in Action updateUI,  in EntitiesViewUI entsUI, in EntitiesModel ents)
        {  
            new DownEventUIS(updateUI, entsUI, ents);
            new RightUnitEventUIS(entsUI, ents);
            new UpEventUIS(entsUI, ents);

            new CenterEventUIS(entsUI.CenterEs, ents);
            CenterBuildingEnventsUI = new BuildingEventsUI(entsUI.CenterEs, ents);


            LeftCityEventUI = new CityEventsUI(entsUI.LeftEs, ents);
            new LeftEnvironmentEventUIS(updateView, entsUI, ents);
        }
    }
}