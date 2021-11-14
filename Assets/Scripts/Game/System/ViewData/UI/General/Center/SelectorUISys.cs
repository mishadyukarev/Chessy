using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            SelectorUIC.DisableAll();

            if (!CellClickC.Is(CellClickTypes.None))
            {
                SelectorUIC.SetActive(CellClickC.Click, true);
            }
            else
            {
                
            }
        }
    }
}
