namespace Game.Game
{
    public sealed class Events
    {
        public Events(in Entities ents, in EntitiesUI entsUI)
        {
            new CenterEventUIS(ents, entsUI);
            new DownEventUIS(ents, entsUI);
            new RightUnitEventUIS(ents, entsUI);
            new UpEventUIS();

            new LeftCityEventUIS(ents, entsUI);
            new LeftEnvEventUISys(ents, entsUI);
            new LeftMarketEventUIS(ents, entsUI);
            new LeftSmelterEventUIS(ents, entsUI);
        }
    }
}