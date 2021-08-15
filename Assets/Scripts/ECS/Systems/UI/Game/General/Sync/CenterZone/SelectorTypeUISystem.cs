using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
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

            if (selectCom.IsCellClickType(CellClickTypes.TakeExtraThing))
            {
                selectTypeViewUIComp.Text = "Take Tool From Pawn";
                selectTypeViewUIComp.EnableZone();
            }

            else if (selectCom.IsCellClickType(CellClickTypes.GiveExtraThing))
            {
                selectTypeViewUIComp.Text = "Give Tool To Pawn";
                selectTypeViewUIComp.EnableZone();
            }

            else
            {
                selectTypeViewUIComp.DisableZone();
            }
        }
    }
}
