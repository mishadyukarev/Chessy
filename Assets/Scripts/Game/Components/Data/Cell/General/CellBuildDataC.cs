using System;

namespace Scripts.Game
{
    public struct CellBuildDataC
    {
        private BuildTypes _buildType;

        public BuildTypes BuildType => _buildType;
        public bool HaveBuild => BuildType != BuildTypes.None;

        public bool Is(BuildTypes buildType) => _buildType == buildType;
        public bool Is(BuildTypes[] buildTypes)
        {
            foreach (var buildType in buildTypes) if (buildType == _buildType) return true;
            return false;
        }
        public void SetBuild(BuildTypes buildType)
        {
            if(buildType == BuildTypes.None) throw new Exception("BuildType is None");
            if (Is(buildType)) throw new Exception("It's got yet");
            if (HaveBuild) throw new Exception("It's got building");
            _buildType = buildType;
        }
        public void Sync(BuildTypes buildType) => _buildType = buildType;
        public void Reset()
        {
            if (!HaveBuild) throw new Exception();
            _buildType = BuildTypes.None;
        }
    }
}