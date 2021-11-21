using System;

namespace Game.Game
{
    public struct Extractor
    {
        public static int GetExtractOneBuild(float percUpg)
        {
            var extaction = 1;
            extaction += (int)(extaction * percUpg);
            return extaction;
        }
        public static int GetAddFood(float percUpg, int amountFarm, int amountUnits)
        {
            return 3 + amountFarm * GetExtractOneBuild(percUpg) - amountUnits;
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