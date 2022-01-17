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


            if (Build<BuildingTC>(_idx).Is(BuildingTypes.Farm) && Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, _idx).Have)
            {
                env = EnvTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Build<BuildingTC>(_idx).Is(BuildingTypes.Woodcutter) && Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Build<BuildingTC>(_idx).Is(BuildingTypes.Mine) && Environment<HaveEnvironmentC>(EnvTypes.Hill, _idx).Have)
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


            if (extract > Environment<AmountC>(env, _idx).Amount) extract = Environment<AmountC>(env, _idx).Amount;

            return true;
        }
        public bool CanBuild(in BuildingTypes build, in PlayerTypes who, out MistakeTypes mistake)
        {
            mistake = default;

            var buildC = Build<BuildingTC>(_idx);


            if (CellUnitStepEs.Have(_idx, build))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!Environment<HaveEnvironmentC>(EnvTypes.AdultForest, _idx).Have)
                    {
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
    }
}