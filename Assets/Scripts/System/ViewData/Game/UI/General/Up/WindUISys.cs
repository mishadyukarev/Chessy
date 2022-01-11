using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    struct WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            DirectWind<DirWindUIC>().SetEulerRot(WhoseMoveC.CurPlayerI, WindC.CurDirWind);
        }
    }
}