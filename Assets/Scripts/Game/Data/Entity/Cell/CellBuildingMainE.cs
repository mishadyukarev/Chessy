namespace Chessy.Game
{
    public struct CellBuildingMainE
    {
        public BuildingTC BuildingC;
        public PlayerTC PlayerC;
        public LevelTC LevelTC;
        public HealthC HealthC;
        public bool IsActiveSmelter;

        public PlayerTC KillerC;
        public DamageC AttackBuildingC;

        public void Set(in BuildingTypes buildT, in LevelTypes levT, in float hp, PlayerTypes playerT)
        {
            BuildingC.Building = buildT;
            HealthC.Health = hp;
            PlayerC.Player = playerT;
            LevelTC.Level = levT;
        }
    }
}