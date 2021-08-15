using Assets.Scripts.Abstractions.Enums.Cell;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellBishopDataComp
    {
        internal BishopMainWeaponTypes MainWeaponType { get; set; }
        internal bool IsMainWeapon(BishopMainWeaponTypes mainWeaponType) => MainWeaponType == mainWeaponType;
        internal void ReplaceMainWeapon(BishopMainWeaponTypes newMainWeaponType) => MainWeaponType = newMainWeaponType;
    }
}
