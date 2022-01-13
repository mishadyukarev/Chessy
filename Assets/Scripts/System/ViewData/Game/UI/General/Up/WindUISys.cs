using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    struct WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            //DirectWind<DirWindUIC>().SetEulerRot(EntWhoseMove.CurPlayerI, WindC.CurDirWind);
        }
    }
}