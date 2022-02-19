namespace Game.Game
{
    public struct CellUnitNeedKillE
    {
        public bool NeedKill;
        public PlayerTC WhoKiller;

        public void Set(in bool needKill, in PlayerTypes whoKiller)
        {
            NeedKill = needKill;
            WhoKiller.Player = whoKiller;
        }
    }
}