using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    public sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            WindUIC.SetEulerRot(WhoseMoveC.CurPlayerI, WindC.DirectWind);
        }
    }
}