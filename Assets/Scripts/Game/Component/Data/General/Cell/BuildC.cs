using System;

namespace Game.Game
{
    public struct BuildC
    {
        private BuildTypes _build;
        private readonly byte _idx;

        public BuildTypes Build => _build;
        public bool Have => Build != default;
        public bool Is(params BuildTypes[] builds)
        {
            foreach (var buildType in builds) if (buildType == _build) return true;
            return false;
        }



        public BuildC(BuildTypes build, byte idx)
        {
            _build = build;
            _idx = idx;
        }



        public void SetNew(BuildTypes build, PlayerTypes owner)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Is(build)) throw new Exception("It's got yet");
            if (Have) throw new Exception("It's got building");
   
            _build = build;
            WhereBuildsC.Set(build, owner, _idx, true);
        }
        public void Remove(PlayerTypes owner)
        {
            if (!Have) throw new Exception();

            WhereBuildsC.Set(_build, owner, _idx, false);
            _build = BuildTypes.None;
        }
        public void Sync(BuildTypes build)
        {
            _build = build;
        }
    }
}