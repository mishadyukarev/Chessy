using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities
{
    internal sealed class CellBuildingEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellBuildingEnts;
        internal ref CellBuildingComponent CellBuildEnt_CellBuilCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
        internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
        internal ref OwnerComponent CellBuildEnt_OwnerCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
        internal ref OwnerBotComponent CellBuildEnt_OwnerBotCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
        internal ref SpriteRendererComponent CellBuildEnt_SpriteRendererCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();

        internal CellBuildingEntsContainer(EcsEntity[,] cellBuildingEnts) : base(cellBuildingEnts)
        {
            _cellBuildingEnts = cellBuildingEnts;
        }
    }
}
