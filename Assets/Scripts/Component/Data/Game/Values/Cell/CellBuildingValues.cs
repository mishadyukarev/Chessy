using System;

namespace Game.Game
{
    public static class CellBuildingValues
    {
        public static int MaxAmountHealth(in BuildingTypes build)
        {
            switch (build)
            {
                case BuildingTypes.City: return 1;
                case BuildingTypes.Farm: return 1;
                case BuildingTypes.Woodcutter: return 1;
                case BuildingTypes.Mine: return 1;
                case BuildingTypes.Camp: return 1;
                case BuildingTypes.IceWall: return 10;
                case BuildingTypes.Teleport: return 1;
                default: throw new Exception();
            }
        }

        public static float ProtectionPercent(in BuildingTypes build)
        {
            switch (build)
            {
                case BuildingTypes.None: return 0;
                case BuildingTypes.City: return 0.2f;
                case BuildingTypes.Farm: return -0.1f;
                case BuildingTypes.Woodcutter: return -0.1f;
                case BuildingTypes.Mine: return -0.1f;
                case BuildingTypes.Camp: return 0;
                case BuildingTypes.IceWall: return 0;
                case BuildingTypes.Teleport: return 0;
                default: throw new Exception();
            }
        }
    }
}