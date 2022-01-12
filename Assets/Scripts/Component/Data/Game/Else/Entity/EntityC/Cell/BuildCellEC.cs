using System;
using System.Collections.Generic;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    public struct BuildCellEC : IBuildCell
    {
        readonly byte _idx;

        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            var ownC = Build<PlayerC>(_idx);


            if (Build<BuildC>(_idx).Is(BuildTypes.Farm) && Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, _idx).Have)
            {
                env = EnvTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Build<BuildC>(_idx).Is(BuildTypes.Woodcutter) && Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Build<BuildC>(_idx).Is(BuildTypes.Mine) && Environment<HaveEnvironmentC>(EnvTypes.Hill, _idx).Have)
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
            extract += (int)(extract * BuildsUpgC.PercUpg(Build<BuildC>(_idx).Build, ownC.Player));


            if (extract > Environment<ResourcesC>(env, _idx).Resources) extract = Environment<ResourcesC>(env, _idx).Resources;

            return true;
        }
        public bool CanBuild(in BuildTypes build, in PlayerTypes who, out MistakeTypes mistake, out Dictionary<ResTypes, int> needRes)
        {
            mistake = default;
            needRes = default;

            var buildC = Build<BuildC>(_idx);


            if (Unit<UnitCellEC>(_idx).Have(build))
            {
                if (!buildC.Have || buildC.Is(BuildTypes.Camp))
                {
                    if (!Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
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
            Build<PlayerC>(_idx).Player = owner;
            WhereBuildsC.Set(build, owner, _idx, true);
        }
        public void Remove()
        {
            if (Build<BuildC>(_idx).Have)
            {
                WhereBuildsC.Set(Build<BuildC>(_idx).Build, Build<PlayerC>(_idx).Player, _idx, false);

                Build<BuildC>(_idx).Reset();
                Build<PlayerC>(_idx).Reset();
            }
        }
        public void Sync(in BuildTypes build, in PlayerTypes owner)
        {
            Build<BuildC>(_idx).Build = build;
            Build<PlayerC>(_idx).Player = owner;
        }
    }
}