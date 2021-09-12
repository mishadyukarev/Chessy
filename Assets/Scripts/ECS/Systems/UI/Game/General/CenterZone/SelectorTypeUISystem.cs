using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Center;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone
{
    internal sealed class SelectorTypeUISystem : IEcsRunSystem
    {
        private EcsFilter<SelectorTypeViewUIComp> _selectorTypeUIFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var selectTypeViewUIComp = ref _selectorTypeUIFilter.Get1(0);
            ref var selectCom = ref _selectorFilter.Get1(0);


            selectTypeViewUIComp.DisableParent();


            if (selectCom.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                selectTypeViewUIComp.Text = "Give or Take Tool";
                selectTypeViewUIComp.EnableParent();
            }

            else if (selectCom.IsCellClickType(CellClickTypes.PickFire))
            {
                selectTypeViewUIComp.Text = "Pick Adult Forest";
                selectTypeViewUIComp.EnableParent();
            }
        }
    }
}
