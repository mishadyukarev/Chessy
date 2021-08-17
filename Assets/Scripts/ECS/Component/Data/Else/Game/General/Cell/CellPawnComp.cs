//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.Abstractions.Enums.Cell;

//namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
//{
//    internal struct CellPawnComp
//    {
//        internal PawnMainThingTypes MainThingType { get; set; }
//        internal bool HaveMainTool => MainThingType != PawnMainThingTypes.None;
//        internal bool IsMainTool(PawnMainThingTypes pawnMainToolTypes) => MainThingType == pawnMainToolTypes;
//        internal void ResetMainTool() => MainThingType = default;


//        internal PawnExtraThingTypes ExtraThingType;
//        internal bool HaveExtraThing => ExtraThingType != PawnExtraThingTypes.None;
//        internal bool IsExtraTool(PawnExtraThingTypes pawnSecondToolType) => ExtraThingType == pawnSecondToolType;
//        internal void ResetExtraTool() => ExtraThingType = default;
//    }
//}
