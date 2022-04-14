using Chessy.Game.Model.Component;

namespace Chessy.Game
{
    public struct BuildingE
    {
        public BuildingTC BuildingTC;
        public PlayerTC PlayerTC;
        public LevelTC LevelTC;
        public HealthC HealthC;
        public readonly VisibleC VisibleC;

        public ResourcesC WoodcutterExtractC;
        public ResourcesC FarmExtractC;

        internal BuildingE(in bool b) : this()
        {
            VisibleC = new VisibleC(default);
        }
    }
}