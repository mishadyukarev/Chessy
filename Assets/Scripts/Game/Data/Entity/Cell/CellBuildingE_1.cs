using Photon.Realtime;

namespace Game.Game
{
    public struct CellBuildingPoolEs
    {
        readonly bool[] _owners;
        public bool IsActiveSmelter;

        public BuildingTC BuildingC;
        public HealthC HealthC;
        public PlayerTC PlayerC;
        public LevelTC LevelTC;

        public ExtractE WoodcutterExtractE;
        public ExtractE FarmExtractE;

        public ref bool IsVisible(in PlayerTypes player) => ref _owners[(byte)player - 1];


        public CellBuildingPoolEs(in byte types) : this()
        {
            _owners = new bool[types];
        }

        public void Set(in BuildingTypes buildT, in LevelTypes levT, in float hp, PlayerTypes playerT)
        {
            BuildingC.Building = buildT;
            HealthC.Health = hp;
            PlayerC.Player = playerT;
            LevelTC.Level = levT;
        }
    }
}