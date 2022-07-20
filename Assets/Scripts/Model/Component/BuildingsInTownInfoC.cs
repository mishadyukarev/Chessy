namespace Chessy.Model.Component
{
    public sealed class BuildingsInTownInfoC
    {
        readonly bool[] _haveBuilding;

        internal bool[] HaveBuildingsClone => (bool[])_haveBuilding.Clone();

        public ref bool HaveBuilding(in BuildingTypes buildingT) => ref _haveBuilding[(byte)buildingT];

        internal BuildingsInTownInfoC()
        {
            _haveBuilding = new bool[(byte)BuildingTypes.End];
        }

        internal void Set(in BuildingTypes buildingT, in bool have) => _haveBuilding[(byte)buildingT] = have;
        internal void Destroy(in BuildingTypes buildingT) => _haveBuilding[(byte)buildingT] = false;
        internal bool Build(in BuildingTypes buildingT) => _haveBuilding[(byte)buildingT] = true;
        internal void Dispose()
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