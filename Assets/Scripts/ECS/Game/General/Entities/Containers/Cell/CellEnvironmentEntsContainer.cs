using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellEnvironmentEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFertilizerEnts;
        internal ref SpriteRendererComponent CellFertilizerEnt_SprRendCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellFertilizerEnt_AmountResourcesCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellFertilizerEnt_HaveEnvCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellYoungForestEnts;
        internal ref SpriteRendererComponent CellYoungForestEnt_SprRendCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaveEnvironmentComponent CellYoungForestEnt_HaveEnvCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellAdultForestEnts;
        internal ref SpriteRendererComponent CellAdultForestEnt_SprRendCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellAdultForestEnt_AmountResourcesCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellAdultForestEnt_HaveEnvCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellHillEnts;
        internal ref SpriteRendererComponent CellHillEnt_SprRendCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellHillEnt_AmountResourcesCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellHillEnt_HaveEnvCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellMountainEnts;
        internal ref SpriteRendererComponent CellMountainEnt_SprRendCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaveEnvironmentComponent CellMountainEnt_HaveEnvCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        internal CellEnvironmentEntsContainer((EcsEntity[,], EcsEntity[,], EcsEntity[,], EcsEntity[,], EcsEntity[,]) ents) : base(ents.Item1)
        {
            _cellFertilizerEnts = ents.Item1;
            _cellYoungForestEnts = ents.Item2;
            _cellAdultForestEnts = ents.Item3;
            _cellHillEnts = ents.Item4;
            _cellMountainEnts = ents.Item5;
        }
    }
}
