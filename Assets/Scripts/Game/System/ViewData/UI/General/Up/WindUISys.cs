using Leopotam.Ecs;
using Photon.Pun;

namespace Game.Game
{
    public sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            DirWindUIC.SetEulerRot(WhoseMoveC.CurPlayerI, WindC.CurDirWind);
        }
    }
}