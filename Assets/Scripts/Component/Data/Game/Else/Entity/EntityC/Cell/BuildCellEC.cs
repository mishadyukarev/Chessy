using System;
using System.Collections.Generic;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    public struct BuildCellEC : IBuildCell
    {
        readonly byte _idx;

        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            var ownC = Build<PlayerTC>(_idx);


            if (Build<BuildingC>(_idx).Is(BuildTypes.Farm) && Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, _idx).Have)
            {
                env = EnvTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Build<BuildingC>(_idx).Is(BuildTypes.Woodcutter) && Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Build<BuildingC>(_idx).Is(BuildTypes.Mine) && Environment<HaveEnvironmentC>(EnvTypes.Hill, _idx).Have)
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
            //extract += (int)(extract * BuildsUpgC.PercUpg(Build<BuildingC>(_idx).Build, ownC.Player));


            if (extract > Environment<AmountResourcesC>(env, _idx).Resources) extract = Environment<AmountResourcesC>(env, _idx).Resources;

            return true;
        }
        public bool CanBuild(in BuildTypes build, in PlayerTypes who, out MistakeTypes mistake, out Dictionary<ResTypes, int> needRes)
        {
            mistake = default;
            needRes = default;

            var buildC = Build<BuildingC>(_idx);


            if (Unit<UnitCellEC>(_idx).Have(build))
            {
                if (!buildC.Have || buildC.Is(BuildTypes.Camp))
                {
                    if (!Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
                    {
                        //if (InvResC.CanCreateBuild(who, build, out needRes))
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    mistake = MistakeTypes.Economy;
                        //    return false;
                        //}

                        return true;
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
            if (Build<BuildingC>(_idx).Is(build)) throw new Exception("It's got yet");
            if (Build<BuildingC>(_idx).Have) Remove();

            Build<BuildingC>(_idx).Build = build;
            Build<PlayerTC>(_idx).Player = owner;
            WhereBuildsE.HaveBuild<HaveBuildingC>(build, owner, _idx).Have = true;
        }
        public void Remove()
        {
            if (Build<BuildingC>(_idx).Have)
            {
                WhereBuildsE.HaveBuild<HaveBuildingC>(Build<BuildingC>(_idx).Build, Build<PlayerTC>(_idx).Player, _idx).Have = false;

                Build<BuildingC>(_idx).Reset();
                Build<PlayerTC>(_idx).Reset();
            }
        }
        public void Sync(in BuildTypes build, in PlayerTypes owner)
        {
            Build<BuildingC>(_idx).Build = build;
            Build<PlayerTC>(_idx).Player = owner;
        }
    }
}