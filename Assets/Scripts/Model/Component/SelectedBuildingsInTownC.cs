using System.Collections.Generic;
namespace Chessy.Model
{
    public sealed class SelectedBuildingsInTownC
    {
        readonly Dictionary<BuildingTypes, bool> _selectedBuildings;
        public bool Is(in BuildingTypes buildingT) => _selectedBuildings[buildingT];

        internal SelectedBuildingsInTownC()
        {
            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);

            _selectedBuildings = selectedBuildings;
        }
        public void Set(in BuildingTypes buildingT, in bool isSelected) => _selectedBuildings[buildingT] = isSelected;
    }
}