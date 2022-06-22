using Chessy.Game.Model.Component;

namespace Chessy.Game
{
    public struct BuildingE
    {
        public BuildingTypes BuildingT { get; internal set; }
        public PlayerTypes PlayerT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }

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