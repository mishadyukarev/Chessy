using System;

namespace Chessy.Game
{
    public static class Building_Values
    {
        public const float HELTH_CITY = 1;

        public static int MaxHealth(in BuildingTypes build)
        {
            switch (build)
            {
                case BuildingTypes.Farm: return 1;
                case BuildingTypes.Woodcutter: return 1;
                case BuildingTypes.Camp: return 1;
                case BuildingTypes.IceWall: return 10;
                case BuildingTypes.Teleport: return 1;
                default: throw new Exception();
            }
        }
    }
}