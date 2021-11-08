using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public struct SelUnitC
    {
        public static UnitTypes SelUnitType;
        public static LevelUnitTypes LevelSelUnitType;


        public static bool IsSelUnit => SelUnitType != default;



        public static void ResetSelUnit() => SelUnitType = default;
    }
}