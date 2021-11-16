using System;

namespace Chessy.Game
{
    public struct BuildC
    {
        private BuildTypes _build;

        public BuildTypes Build => _build;
        public bool Have => Build != default;
        public bool Is(params BuildTypes[] buildTypes)
        {
            foreach (var buildType in buildTypes) if (buildType == _build) return true;
            return false;
        }



        public void SetNew(BuildTypes build)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Is(build)) throw new Exception("It's got yet");
            if (Have) throw new Exception("It's got building");

            _build = build;
        }
        public void Remove()
        {
            if (!Have) throw new Exception();
            _build = BuildTypes.None;
        }
        public void Sync(BuildTypes buildType) => _build = buildType;
    }
}