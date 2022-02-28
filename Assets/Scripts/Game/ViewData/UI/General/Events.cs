using System;

namespace Chessy.Game
{
    public struct Events
    {
        public Events(in Action updateView, in Action updateUI, in EntitiesModel ents, in EntitiesViewUI entsUI)
        {
            new CenterEventUIS(ents, entsUI);
            new DownEventUIS(updateUI, ents, entsUI);
            new RightUnitEventUIS(ents, entsUI);
            new UpEventUIS(ents, entsUI);

            new LeftCityEventUIS(ents, entsUI);
            new LeftEnvironmentEventUIS(updateView, ents, entsUI);
            new LeftMarketEventUIS(ents, entsUI);
            new LeftSmelterEventUIS(ents, entsUI);
        }
    }
}