using Assets.Scripts.Abstractions.Enums.Cell;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGiveExtraPawnToolComp
    {
        internal byte IdxForGivePawnTool { get; set; }
        internal PawnExtraToolTypes PawnExtraToolType { get; set; }
    }
}
