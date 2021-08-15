using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.Cell.Pawn;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellPawnDataComp
    {
        internal PawnMainToolTypes MainToolType { get; set; }
        internal bool HaveMainTool => MainToolType != PawnMainToolTypes.None;
        internal bool IsMainTool(PawnMainToolTypes pawnMainToolTypes) => MainToolType == pawnMainToolTypes;
        internal void ResetMainTool() => MainToolType = default;


        internal PawnMainWeaponTypes MainWeaponType { get; set; }
        internal bool HaveMainWeapon => MainWeaponType != PawnMainWeaponTypes.None;
        internal bool IsWeapon(PawnMainWeaponTypes pawnMainWeaponType) => MainWeaponType == pawnMainWeaponType;
        internal void ResetMainWeapon() => MainWeaponType = default;




        internal PawnExtraToolTypes ExtraToolType;
        internal bool HaveExtraTool => ExtraToolType != PawnExtraToolTypes.None;
        internal bool IsExtraTool(PawnExtraToolTypes pawnSecondToolType) => ExtraToolType == pawnSecondToolType;
        internal void ResetExtraTool() => ExtraToolType = default;


        internal PawnExtraWeaponTypes ExtraWeaponType;
        internal bool HaveExtraWeapon => ExtraWeaponType != PawnExtraWeaponTypes.None;
        internal bool IsExtraWeapon(PawnExtraWeaponTypes pawnExtraWeaponType) => ExtraWeaponType == pawnExtraWeaponType;
        internal void ResetExtraWeapon() => ExtraWeaponType = default;
    }
}
