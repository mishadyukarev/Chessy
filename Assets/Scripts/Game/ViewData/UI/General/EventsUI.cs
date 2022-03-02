using System;

namespace Chessy.Game
{
    public sealed class EventsUI
    {
        public readonly LeftCityEventUI LeftCityEventUI;

        public EventsUI(in Action updateView, in Action updateUI,  in EntitiesViewUI entsUI, in EntitiesModel ents)
        {
            new CenterEventUIS(entsUI, ents);
            new DownEventUIS(updateUI, entsUI, ents);
            new RightUnitEventUIS(entsUI, ents);
            new UpEventUIS(entsUI, ents);

            LeftCityEventUI = new LeftCityEventUI(entsUI, ents);
            new LeftEnvironmentEventUIS(updateView, entsUI, ents);
            new LeftMarketEventUIS(entsUI, ents);
        }
    }
}