using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            WindUIC.SetEulerRot(WhoseMoveC.CurPlayerI, WindC.Direct);
        }
    }
}