namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct WhoseMoveComp
    {
        internal bool IsMainMove;

        internal WhoseMoveComp(bool isMainMove) => IsMainMove = isMainMove;
    }
}
