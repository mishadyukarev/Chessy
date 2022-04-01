namespace Chessy.Game
{
    public struct BuildingTC
    {
        public BuildingTypes BuildingT { get; internal set; }

        public bool HaveBuilding => !Is(BuildingTypes.None, BuildingTypes.End);
        public bool Is(params BuildingTypes[] builds)
        {
            foreach (var build in builds) if (build == BuildingT) return true;
            return false;
        }
    }
}