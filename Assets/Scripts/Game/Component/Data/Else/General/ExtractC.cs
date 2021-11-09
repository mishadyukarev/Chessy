using System;

namespace Chessy.Game
{
    public struct ExtractC
    {
        public static int GetExtractOneBuild(bool haveUpgrade)
        {
            var extaction = 1;
            if (haveUpgrade) extaction += 1;
            return extaction;
        }
        public static int GetAddFood(bool haveUpg, int amountFarm, int amountUnits)
        {
            return 3 + amountFarm * GetExtractOneBuild(haveUpg) - amountUnits;
        }

        public static int ExtractOnePawnWood(LevelUnitTypes levelUnit)
        {
            var amountExtract = 0;

            switch (levelUnit)
            {
                case LevelUnitTypes.None: throw new Exception();
                case LevelUnitTypes.First: amountExtract += 1; break;
                case LevelUnitTypes.Second: amountExtract += 2; break;
                default: throw new Exception();
            }

            return amountExtract;
        }
    }
}