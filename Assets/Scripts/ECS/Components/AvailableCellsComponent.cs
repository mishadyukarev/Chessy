using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AvailableCellsComponent
    {
        internal List<int[]> AvailableCells { get; set; }


        internal AvailableCellsComponent(List<int[]> availableCells) => AvailableCells = availableCells;
    }
}
