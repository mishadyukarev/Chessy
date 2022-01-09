namespace Game.Game
{
    public sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            EntityUIPool.DirectWindUp<DirWindUIC>().SetEulerRot(WhoseMoveC.CurPlayerI, WindC.CurDirWind);
        }
    }
}