using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public sealed class PlayerInfoE
    {
        public readonly PlayerInfoC PlayerInfoC = new();
        public readonly GodInfoC GodInfoC = new();
        public readonly PawnPeopleInfoC PawnInfoC = new();
        public readonly BuildingsInTownInfoC BuildingsInTownInfoC = new();
        public readonly ResourcesInInventoryC ResourcesInInventoryC = new();
        public readonly HowManyToolWeaponsInInventoryC HowManyToolWeaponsInInventoryC = new();

        internal void Dispose()
        {
            PlayerInfoC.Dispose();
            GodInfoC.Dispose();
            PawnInfoC.Dispose();
            BuildingsInTownInfoC.Dispose();
            ResourcesInInventoryC.Dispose();
            HowManyToolWeaponsInInventoryC.Dispose();
        }
    }
}