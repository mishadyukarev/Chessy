using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;

namespace Assets.Scripts.ECS.Systems.Game.Master
{
    internal sealed class InitGameMasterSystem : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld = default;

        private EcsFilter<DonerDataUIComponent> _donerFilter = default;
        private EcsFilter<InventorResourcesComponent> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilter = default;

        public void Init()
        {
            
        }
    }
}
