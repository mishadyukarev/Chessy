using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct GameModesCom
    {
        internal static OffGameModes OffGameMode;
        internal static bool IsGameMode(OffGameModes offGameMode) => OffGameMode == offGameMode;
    }
}
