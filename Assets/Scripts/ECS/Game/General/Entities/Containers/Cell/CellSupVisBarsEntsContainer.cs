using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisBarsEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellHpSupStatEnts;
        internal ref SpriteRendererComponent CellHpSupStatEnt_SpriteRendererCom(int[] xy) => ref _cellHpSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellFertilizeSupStatEnts;
        internal ref SpriteRendererComponent CellFertilizeSupStatEnt_SprRendCom(int[] xy) => ref _cellFertilizeSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellForestSupStatEnts;
        internal ref SpriteRendererComponent CellWoodSupStatEnt_SprRendCom(int[] xy) => ref _cellForestSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellOreSupStatEnts;
        internal ref SpriteRendererComponent CellOreSupStatEnt_SprRendCom(int[] xy) => ref _cellOreSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisBarsEntsContainer((EcsEntity[,], EcsEntity[,], EcsEntity[,], EcsEntity[,]) ents) : base(ents.Item1)
        {
            _cellHpSupStatEnts = ents.Item1;
            _cellFertilizeSupStatEnts = ents.Item2;
            _cellForestSupStatEnts = ents.Item3;
            _cellOreSupStatEnts = ents.Item4;
        }
    }
}
