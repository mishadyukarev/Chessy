using Chessy.Model.Component;
namespace Chessy.Model.Entity
{
    public struct PlayerInfoE
    {
        public PlayerInfoC PlayerInfoC;
        public GodInfoC GodInfoC;
        public PawnPeopleInfoC PawnInfoC;
        public readonly BuildingsInTownInfoC BuildingsInTownInfoC;
        public readonly ResourcesInInventoryC ResourcesInInventoryC;
        public readonly HowManyToolWeaponsInInventoryC HowManyToolWeaponsInInventoryC;

        internal PlayerInfoE(in bool def)
        {
            PlayerInfoC = default;
            GodInfoC = default;
            PawnInfoC = default;
            BuildingsInTownInfoC = new BuildingsInTownInfoC(new bool[(byte)BuildingTypes.End]);
            ResourcesInInventoryC = new ResourcesInInventoryC(new float[(byte)ResourceTypes.End]);
            HowManyToolWeaponsInInventoryC = new HowManyToolWeaponsInInventoryC(def);
        }
    }
}