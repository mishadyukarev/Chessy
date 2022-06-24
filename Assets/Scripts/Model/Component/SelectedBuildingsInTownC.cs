using System.Collections.Generic;

namespace Chessy.Model
{
    public struct SelectedBuildingsInTownC
    {
        readonly Dictionary<BuildingTypes, bool> _selectedBuildings;
        public bool Is(in BuildingTypes buildingT) => _selectedBuildings[buildingT];

        internal SelectedBuildingsInTownC(in Dictionary<BuildingTypes, bool> selBuildings) => _selectedBuildings = selBuildings;

        public void Set(in BuildingTypes buildingT, in bool isSelected) => _selectedBuildings[buildingT] = isSelected;
    }
}