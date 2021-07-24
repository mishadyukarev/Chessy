using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellSupportVisionEnts;
        internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisEntsContainer(EcsEntity[,] cellSupportVisionEnts) : base(cellSupportVisionEnts)
        {
            _cellSupportVisionEnts = cellSupportVisionEnts;
        }
    }
}
