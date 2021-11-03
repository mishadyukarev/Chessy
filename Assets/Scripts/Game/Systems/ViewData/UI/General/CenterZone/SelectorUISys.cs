using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            SelectorUIC.DisableAll();

            if (!SelectorC.Is(CellClickTypes.None))
            {
                SelectorUIC.SetActive(SelectorC.CellClickType, true);
            }
            else
            {
                
            }
        }
    }
}
