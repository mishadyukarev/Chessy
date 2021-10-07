using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class SelectorTypeUISystem : IEcsRunSystem
    {
        private EcsFilter<SelectorTypeViewUIComp> _selectorTypeUIFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        public void Run()
        {
            ref var selectTypeViewUIComp = ref _selectorTypeUIFilter.Get1(0);
            ref var selectCom = ref _selectorFilter.Get1(0);


            if (selectCom.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                selectTypeViewUIComp.SetActiveBack(true);
                selectTypeViewUIComp.SetActiveGiveTake(true);
            }

            else if (selectCom.IsCellClickType(CellClickTypes.PickFire))
            {
                selectTypeViewUIComp.SetActiveBack(true);
                selectTypeViewUIComp.SetActivePickAdultForest(true);
            }

            else
            {
                selectTypeViewUIComp.SetActiveBack(false);

                selectTypeViewUIComp.SetActivePickAdultForest(false);
                selectTypeViewUIComp.SetActiveGiveTake(false);
            }
        }
    }
}
