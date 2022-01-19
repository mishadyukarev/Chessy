namespace Game.Game
{
    public struct EventUIManager
    {
        public EventUIManager(in bool def)
        {
            new CenterEventUIS();
            new LeftCityEventUISys();
            new LeftEnvEventUISys();
            new DownEventUIS();
            new RightUnitEventUIS();
            new UpEventUIS();
        }
    }
}