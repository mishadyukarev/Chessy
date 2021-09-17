namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct WhoseMoveCom
    {
        internal bool IsMainMove;

        internal WhoseMoveCom(bool isMainMove) => IsMainMove = isMainMove;
    }
}
