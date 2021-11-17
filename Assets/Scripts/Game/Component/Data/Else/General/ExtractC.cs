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

        public static int ExtractOnePawnWood(LevelTypes levelUnit)
        {
            var amountExtract = 0;

            switch (levelUnit)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First: amountExtract += 1; break;
                case LevelTypes.Second: amountExtract += 2; break;
                default: throw new Exception();
            }

            return amountExtract;
        }
    }
}