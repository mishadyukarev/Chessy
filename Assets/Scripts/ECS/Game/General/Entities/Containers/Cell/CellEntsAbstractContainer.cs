using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal abstract class CellEntsAbstractContainer
    {
        protected const int X = 0;
        protected const int Y = 1;

        internal int Xamount { get; private set; }
        internal int Yamount { get; private set; }

        protected CellEntsAbstractContainer(EcsEntity[,] anyCellEnts)
        {
            Xamount = anyCellEnts.GetUpperBound(X) + 1;
            Yamount = anyCellEnts.GetUpperBound(Y) + 1;
        }
    }
}
