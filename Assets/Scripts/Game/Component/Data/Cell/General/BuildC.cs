using System;

namespace Chessy.Game
{
    public struct BuildC
    {
        private BuildTypes _build;

        public BuildTypes Build
        {
            get => _build;
            set
            {
                if (value == default) throw new Exception("BuildType is None");
                if (Is(value)) throw new Exception("It's got yet");
                if (HaveBuild) throw new Exception("It's got building");
                _build = value;
            }
        }
        public bool HaveBuild => Build != default;



        public bool Is(BuildTypes buildType) => _build == buildType;
        public bool Is(BuildTypes[] buildTypes)
        {
            foreach (var buildType in buildTypes) if (buildType == _build) return true;
            return false;
        }
        public void Reset()
        {
            if (!HaveBuild) throw new Exception();
            _build = BuildTypes.None;
        }
        public void Sync(BuildTypes buildType) => _build = buildType;
    }
}