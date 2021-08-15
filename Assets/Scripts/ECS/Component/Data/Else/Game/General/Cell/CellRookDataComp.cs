using Assets.Scripts.Abstractions.Enums.Unit;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellRookDataComp
    {
        internal RookMainWeaponTypes MainWeaponType { get; set; }
        internal bool IsMainWeapon(RookMainWeaponTypes mainWeaponType) => MainWeaponType == mainWeaponType;
        internal void ReplaceMainWeapon(RookMainWeaponTypes newMainWeaponType) => MainWeaponType = newMainWeaponType;
    }
}
