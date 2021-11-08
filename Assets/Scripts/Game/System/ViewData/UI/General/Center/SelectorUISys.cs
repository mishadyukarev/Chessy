using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            SelectorUIC.DisableAll();

            if (!SelectorC.Is(CellClickTypes.None))
            {
                SelectorUIC.SetActive(SelectorC.CellClick, true);
            }
            else
            {
                
            }
        }
    }
}
