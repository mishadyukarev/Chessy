namespace Game.Game
{
    public struct BuildC : IBuildCell
    {
        public BuildTypes Build { get; internal set; }

        public bool Have => Build != default;
        public bool Is(params BuildTypes[] builds)
        {
            foreach (var build in builds) if (build == Build) return true;
            return false;
        }

        internal BuildC(in BuildTypes build) => Build = build;

        internal void Reset() => Build = default;
    }
}