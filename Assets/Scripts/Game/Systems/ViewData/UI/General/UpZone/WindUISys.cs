using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            WindUIC.SetEulerRot(WhoseMoveC.CurPlayer, WindC.DirectWind);
        }
    }
}