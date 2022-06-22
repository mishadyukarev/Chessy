using Chessy.Game;

namespace Chessy.Common
{
    public struct SelectedE
    {
        public AbilityTypes AbilityT { get; internal set; }
        public SelectedBuildingsC BuildingsC;
        public SelectedToolWeaponC ToolWeaponC;
    }
}