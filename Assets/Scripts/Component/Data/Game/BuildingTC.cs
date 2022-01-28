namespace Game.Game
{
    public struct BuildingTC
    {
        public BuildingTypes Build;

        public bool Have => Build != default;
        public bool Is(params BuildingTypes[] builds)
        {
            foreach (var build in builds) if (build == Build) return true;
            return false;
        }

        public BuildingTC(in BuildingTypes build) => Build = build;

        public void Reset() => Build = default;
    }
}