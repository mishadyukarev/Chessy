namespace Game.Game
{
    public sealed class Events
    {
        public Events(in Entities ents, in EntitiesView entsView)
        {
            new CenterEventUIS(ents, entsView);
            new LeftCityEventUISys(ents, entsView);
            new LeftEnvEventUISys(ents, entsView);
            new DownEventUIS(ents, entsView);
            new RightUnitEventUIS(ents, entsView);
            new UpEventUIS();
        }
    }
}