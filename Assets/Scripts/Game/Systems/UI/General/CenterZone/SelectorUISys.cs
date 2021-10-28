using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!SelectorC.Is(CellClickTypes.None))
            {
                SelectorUIC.SetActive(SelectorC.CellClickType, true);
            }
            else
            {
                SelectorUIC.DisableAll();
            }
        }
    }
}
