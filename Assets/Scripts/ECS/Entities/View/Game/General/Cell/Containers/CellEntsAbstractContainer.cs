using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal abstract class CellEntsAbstractContainer
    {
        protected const int X = 0;
        protected const int Y = 1;

        internal int Xamount { get; private set; }
        internal int Yamount { get; private set; }

        protected CellEntsAbstractContainer(GameObject[,] anyCellGOs)
        {
            Xamount = anyCellGOs.GetUpperBound(X) + 1;
            Yamount = anyCellGOs.GetUpperBound(Y) + 1;
        }
    }
}
