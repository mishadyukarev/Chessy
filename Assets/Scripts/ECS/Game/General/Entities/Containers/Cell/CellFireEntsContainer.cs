using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellFireEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFireEnts;
        internal ref SpriteRendererComponent CellFireEnt_SprRendCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaverEffectComponent CellFireEnt_HaverEffectCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<HaverEffectComponent>();
        internal ref TimeStepsComponent CellFireEnt_TimeStepsCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();


        internal CellFireEntsContainer(EcsEntity[,] cellFireEnts) : base(cellFireEnts)
        {
            _cellFireEnts = cellFireEnts;
        }
    }
}
