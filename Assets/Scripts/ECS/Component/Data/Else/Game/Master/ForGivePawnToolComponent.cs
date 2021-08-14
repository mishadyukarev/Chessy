using Assets.Scripts.Abstractions.Enums.Cell;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGivePawnToolComponent
    {
        internal byte IdxForGivePawnTool { get; set; }
        internal PawnExtraToolTypes PawnToolType { get; set; }
    }
}
