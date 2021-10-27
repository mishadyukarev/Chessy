using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (SelectorC.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                SelectorUIC.SetActiveBack(true);
                SelectorUIC.SetActiveGiveTake(true);
            }

            else if (SelectorC.IsCellClickType(CellClickTypes.PickFire))
            {
                SelectorUIC.SetActiveBack(true);
                SelectorUIC.SetActivePickAdultForest(true);
            }

            else
            {
                SelectorUIC.SetActiveBack(false);

                SelectorUIC.SetActivePickAdultForest(false);
                SelectorUIC.SetActiveGiveTake(false);
            }
        }
    }
}
