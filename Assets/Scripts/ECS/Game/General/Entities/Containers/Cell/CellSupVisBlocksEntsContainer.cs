using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisBlocksEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellProtectRelaxEnts;
        internal ref SpriteRendererComponent CellProtectRelaxEnt_SpriteRendererCom(int[] xy) => ref _cellProtectRelaxEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellMaxStepsEnts;
        internal ref SpriteRendererComponent CellMaxStepsEnt_SpriteRendererCom(int[] xy) => ref _cellMaxStepsEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisBlocksEntsContainer((EcsEntity[,], EcsEntity[,]) ents) : base(ents.Item1)
        {
            _cellProtectRelaxEnts = ents.Item1;
            _cellMaxStepsEnts = ents.Item2;
        }
    }
}
