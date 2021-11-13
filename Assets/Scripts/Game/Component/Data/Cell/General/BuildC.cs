using System;

namespace Chessy.Game
{
    public struct BuildC
    {
        private BuildTypes _type;

        public BuildTypes Type => _type;
        public bool Have => Type != default;
        public bool Is(params BuildTypes[] buildTypes)
        {
            foreach (var buildType in buildTypes) if (buildType == _type) return true;
            return false;
        }



        public void SetNew(BuildTypes build)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Is(build)) throw new Exception("It's got yet");
            if (Have) throw new Exception("It's got building");

            _type = build;
        }
        public void Remove()
        {
            if (!Have) throw new Exception();
            _type = BuildTypes.None;
        }
        public void Sync(BuildTypes buildType) => _type = buildType;
    }
}