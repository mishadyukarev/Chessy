using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct OwnerOfflineCom
    {
        internal PlayerTypes LocalPlayerType;
        internal bool Is(PlayerTypes localPlayerType) => LocalPlayerType == localPlayerType;
    }
}
