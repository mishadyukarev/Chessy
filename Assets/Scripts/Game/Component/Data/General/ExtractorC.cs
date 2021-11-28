using System;

namespace Game.Game
{
    public struct ExtractorC
    {
        public static int GetExtractOneBuild(float percUpg)
        {
            var extaction = 10;
            extaction += (int)(extaction * percUpg);
            return extaction;
        }
        public static int GetAddFood(float percUpg, int amountFarm, int amountUnits)
        {
            return 40 + amountFarm * GetExtractOneBuild(percUpg) - amountUnits;
        }

        public static int ExtractOnePawnWood(LevelTypes levelUnit)
        {
            var amountExtract = 0;

            switch (levelUnit)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First: amountExtract += 10; break;
                case LevelTypes.Second: amountExtract += 20; break;
                default: throw new Exception();
            }

            return amountExtract;
        }
    }
}