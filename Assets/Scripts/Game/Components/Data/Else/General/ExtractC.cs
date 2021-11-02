using UnityEditor;
using UnityEngine;

namespace Scripts.Game
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
    }
}