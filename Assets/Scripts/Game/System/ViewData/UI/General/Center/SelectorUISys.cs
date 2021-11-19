using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            SelectorUIC.SyncView(CellClickC.Click);
        }
    }
}
