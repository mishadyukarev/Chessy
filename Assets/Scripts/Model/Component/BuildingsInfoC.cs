namespace Chessy.Model.Model.Component
{
    public struct BuildingsInfoC
    {
        readonly bool[] _haveBuilding;

        internal bool[] HaveBuildingsClone => (bool[])_haveBuilding.Clone();

        public bool HaveBuilding(in BuildingTypes buildingT) => _haveBuilding[(byte)buildingT];

        internal BuildingsInfoC(in bool[] haves) => _haveBuilding = haves;

        internal void Set(in BuildingTypes buildingT, in bool have) => _haveBuilding[(byte)buildingT] = have;
        internal void Destroy(in BuildingTypes buildingT) => _haveBuilding[(byte)buildingT] = false;
        internal bool Build(in BuildingTypes buildingT) => _haveBuilding[(byte)buildingT] = true;
        internal void Clear()
        {
            for (var i = 0; i < _haveBuilding.Length; i++)
            {
                _haveBuilding[i] = false;
            }
        }

        internal void Sync(in bool[] haveBuildings)
        {
            for (int i = 0; i < haveBuildings.Length; i++) _haveBuilding[i] = haveBuildings[i];
        }
    }
}