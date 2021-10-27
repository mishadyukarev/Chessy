using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class WindUISys : IEcsRunSystem
    {
        public void Run()
        {
            WindUIC.SetEulerRot(WindC.DirectWind);
        }
    }
}