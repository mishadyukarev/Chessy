namespace Chessy.Model
{
    public struct BuildingC
    {
        public BuildingTypes BuildingT { get; internal set; }
        public PlayerTypes PlayerT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }
    }
}