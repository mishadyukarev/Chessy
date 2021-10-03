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


            selectTypeViewUIComp.DisableParent();


            if (selectCom.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                selectTypeViewUIComp.Text = LanguageComCom.GetText(GameLanguageTypes.GiveOrTakeTool);
                selectTypeViewUIComp.EnableParent();
            }

            else if (selectCom.IsCellClickType(CellClickTypes.PickFire))
            {
                selectTypeViewUIComp.Text = LanguageComCom.GetText(GameLanguageTypes.PickAdultForest);
                selectTypeViewUIComp.EnableParent();
            }
        }
    }
}
