namespace Chessy.Game
{
    public struct BuildingTC
    {
        public BuildingTypes Building;

        public bool HaveBuilding => !Is(BuildingTypes.None, BuildingTypes.End);
        public bool Is(params BuildingTypes[] builds)
        {
            foreach (var build in builds) if (build == Building) return true;
            return false;
        }

        public BuildingTC(in BuildingTypes build) => Building = build;
    }
}