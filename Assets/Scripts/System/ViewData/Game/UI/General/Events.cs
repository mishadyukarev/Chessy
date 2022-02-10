﻿namespace Game.Game
{
    public sealed class Events
    {
        public Events(in Entities ents, in EntitiesView entsView)
        {
            new CenterEventUIS(ents, entsView);
            new DownEventUIS(ents, entsView);
            new RightUnitEventUIS(ents, entsView);
            new UpEventUIS();

            new LeftCityEventUIS(ents, entsView);
            new LeftEnvEventUISys(ents, entsView.UIEs);
            new LeftMarketEventUIS(ents, entsView);
            new LeftSmelterEventUIS(ents, entsView.UIEs);
        }
    }
}