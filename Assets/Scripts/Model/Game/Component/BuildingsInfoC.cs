using System.Collections.Generic;

namespace Chessy.Game.Model.Component
{
    public struct BuildingsInfoC
    {
        readonly Dictionary<BuildingTypes, bool> _haveBuilding;

        public bool HaveBuilding(in BuildingTypes buildingT) => _haveBuilding[buildingT];

        internal BuildingsInfoC(in Dictionary<BuildingTypes, bool> haves) => _haveBuilding = haves;

        internal void Set(in BuildingTypes buildingT, in bool have) => _haveBuilding[buildingT] = have;
        internal void Destroy(in BuildingTypes buildingT) => _haveBuilding[buildingT] = false;
        internal bool Build(in BuildingTypes buildingT) => _haveBuilding[buildingT] = true;
    }
}