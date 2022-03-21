using Chessy.Game;
using System.Collections.Generic;

namespace Chessy.Common
{
    public sealed class SelectedE
    {
        public AbilityTC AbilityTC;

        public SelectedBuildingsC BuildingsC;
        public SelectedUnitC UnitC;
        public SelectedToolWeaponC ToolWeaponC;

        public SelectedE(in SelectedToolWeaponC selTWC)
        {
            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);
            BuildingsC = new SelectedBuildingsC(selectedBuildings);

            ToolWeaponC = selTWC;
        }
    }
}