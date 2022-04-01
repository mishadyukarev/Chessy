using System.Collections.Generic;

namespace Chessy.Game
{
    public struct SelectedBuildingsC
    {
        readonly Dictionary<BuildingTypes, bool> _selectedBuildings;
        public bool Is(in BuildingTypes buildingT) => _selectedBuildings[buildingT];

        internal SelectedBuildingsC(in Dictionary<BuildingTypes, bool> selBuildings) => _selectedBuildings = selBuildings;

        public void Set(in BuildingTypes buildingT, in bool isSelected) => _selectedBuildings[buildingT] = isSelected;
    }
}