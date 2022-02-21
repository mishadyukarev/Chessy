using System;

namespace Game.Game
{
    public struct Events
    {
        public Events(in Action updateView, in Action updateUI, in EntitiesModel ents, in EntitiesViewUI entsUI)
        {
            new CenterEventUIS(ents, entsUI);
            new DownEventUIS(updateUI, ents, entsUI);
            new RightUnitEventUIS(ents, entsUI);
            new UpEventUIS();

            new LeftCityEventUIS(ents, entsUI);
            new LeftEnvEventUISys(updateView, ents, entsUI);
            new LeftMarketEventUIS(ents, entsUI);
            new LeftSmelterEventUIS(ents, entsUI);
        }
    }
}