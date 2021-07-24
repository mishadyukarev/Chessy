using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellEnts;
        internal ref CellGOComponent CellEnt_CellGOCom(int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellGOComponent>();

        internal CellEntsContainer(EcsEntity[,] cellEnts) : base(cellEnts)
        {
            _cellEnts = cellEnts;
        }
    }
}
