using Assets.Scripts.Abstractions.Enums.Cell.Pawn;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGiveExtraPawnWeaponComp
    {
        internal byte IdxForGiveExtraPawnWeapon { get; set; }
        internal PawnExtraWeaponTypes PawnExtraWeaponType { get; set; }
    }
}
