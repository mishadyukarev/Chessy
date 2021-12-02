using System;
using System.Collections.Generic;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct BuildC : IBuildCell
    {
        private BuildTypes _build;
        private readonly byte _idx;

        public BuildTypes Build => _build;
        public bool Have => Build != default;
        public bool Is(params BuildTypes[] builds)
        {
            foreach (var build in builds) if (build == _build) return true;
            return false;
        }
        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            var ownC = Build<OwnerC>(_idx);
            var envC = Environment<EnvC>(_idx);
            var envResC = Environment<EnvResC>(_idx);


            if (Is(BuildTypes.Farm) && envC.Have(EnvTypes.Fertilizer))
            {
                env = EnvTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Is(BuildTypes.Woodcutter) && envC.Have(EnvTypes.AdultForest))
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Is(BuildTypes.Mine) && envC.Have(EnvTypes.Hill))
            {
                env = EnvTypes.Hill;
                res = ResTypes.Ore;
            }
            else
            {
                extract = default;
                env = default;
                res = default;

                return false;
            }



            extract = 10;
            extract += (int)(extract * BuildsUpgC.PercUpg(_build, ownC.Owner));


            if (extract > envResC.Amount(env)) extract = envResC.Amount(env);

            return true;
        }
        public bool CanBuild(BuildTypes build, PlayerTypes who, out MistakeTypes mistake, out Dictionary<ResTypes, int> needRes)
        {
            mistake = default;
            needRes = default;

            var buildC = Build<BuildC>(_idx);
            var envC = Environment<EnvC>(_idx);


            if (Unit<StepC>(_idx).HaveMin)
            {
                if (!buildC.Have || buildC.Is(BuildTypes.Camp))
                {
                    if (!envC.Have(EnvTypes.AdultForest))
                    {
                        if (InvResC.CanCreateBuild(who, build, out needRes))
                        {
                            return true;
                        }
                        else
                        {
                            mistake = MistakeTypes.Economy;
                            return false;
                        }
                    }
                    else
                    {
                        mistake = MistakeTypes.NeedOtherPlace;
                        return false;
                    }
                }
                else
                {
                    mistake = MistakeTypes.NeedOtherPlace;
                    return false;
                }
            }
            else
            {
                mistake = MistakeTypes.NeedMoreSteps;
                return false;
            }
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
            if (Have) Remove();
   
            _build = build;
            Build<OwnerC>(_idx).SetOwner(owner);
            WhereBuildsC.Set(build, owner, _idx, true);
        }
        public void Remove()
        {
            var owner = Build<OwnerC>(_idx).Owner;

            if (Have)
            {
                WhereBuildsC.Set(_build,  owner, _idx, false);
                _build = BuildTypes.None;
            } 
        }
        public void Sync(BuildTypes build)
        {
            _build = build;
        }
    }
}