using ECS;

namespace Game.Game
{
    public struct CellUnitMainToolWeaponE
    {
        public ToolWeaponTC ToolWeaponTC;
        public LevelTC LevelTC;

        public void Set(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponTC.ToolWeapon = twT;
            LevelTC.Level = levT;
        }
    }
}