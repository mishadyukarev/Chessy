namespace Game.Game
{
    public struct BuildingC : IBuildCell
    {
        public BuildTypes Build;

        public bool Have => Build != default;
        public bool Is(params BuildTypes[] builds)
        {
            foreach (var build in builds) if (build == Build) return true;
            return false;
        }

        public BuildingC(in BuildTypes build) => Build = build;

        public void Reset() => Build = default;
    }
}