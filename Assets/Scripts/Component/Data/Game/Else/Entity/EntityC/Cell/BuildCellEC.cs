using System;
using System.Collections.Generic;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct BuildCellEC : IBuildCell
    {
        readonly byte _idx;

        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            var ownC = Build<OwnerC>(_idx);
            var envC = Environment<EnvironmentC>(_idx);
            var envResC = Environment<EnvResC>(_idx);


            if (Build<BuildC>(_idx).Is(BuildTypes.Farm) && envC.Have(EnvTypes.Fertilizer))
            {
                env = EnvTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Build<BuildC>(_idx).Is(BuildTypes.Woodcutter) && envC.Have(EnvTypes.AdultForest))
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Build<BuildC>(_idx).Is(BuildTypes.Mine) && envC.Have(EnvTypes.Hill))
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
            extract += (int)(extract * BuildsUpgC.PercUpg(Build<BuildC>(_idx).Build, ownC.Owner));


            if (extract > envResC.Amount(env)) extract = envResC.Amount(env);

            return true;
        }
        public bool CanBuild(in BuildTypes build, in PlayerTypes who, out MistakeTypes mistake, out Dictionary<ResTypes, int> needRes)
        {
            mistake = default;
            needRes = default;

            var buildC = Build<BuildC>(_idx);
            var envC = Environment<EnvironmentC>(_idx);


            if (Unit<UnitCellEC>(_idx).Have(build))
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


        internal BuildCellEC(in byte idx) => _idx = idx;


        public void SetNew(in BuildTypes build, in PlayerTypes owner)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Build<BuildC>(_idx).Is(build)) throw new Exception("It's got yet");
            if (Build<BuildC>(_idx).Have) Remove();

            Build<BuildC>(_idx).Build = build;
            Build<OwnerC>(_idx).Owner = owner;
            WhereBuildsC.Set(build, owner, _idx, true);
        }
        public void Remove()
        {
            if (Build<BuildC>(_idx).Have)
            {
                WhereBuildsC.Set(Build<BuildC>(_idx).Build, Build<OwnerC>(_idx).Owner, _idx, false);

                Build<BuildC>(_idx).Reset();
                Build<OwnerC>(_idx).Reset();
            }
        }
        public void Sync(in BuildTypes build, in PlayerTypes owner)
        {
            Build<BuildC>(_idx).Build = build;
            Build<OwnerC>(_idx).Owner = owner;
        }
    }
}