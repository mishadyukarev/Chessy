using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone
{
    internal sealed class SyncToolsUpUISystem : IEcsRunSystem
    {
        private EcsFilter<ToolsViewUIComp> _toolUpUIFilter = default;
        private EcsFilter<InventorToolsComponent> _inventToolsFilter = default;

        public void Run()
        {
            ref var toolsViewUICom = ref _toolUpUIFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);

            toolsViewUICom.SetTextPawnExtraTool(PawnExtraToolTypes.Pick, inventToolsCom.GetAmountTools(PawnExtraToolTypes.Pick).ToString());
            toolsViewUICom.SetTextPawnExtraTool(PawnExtraToolTypes.Sword, inventToolsCom.GetAmountTools(PawnExtraToolTypes.Sword).ToString());
        }
    }
}
