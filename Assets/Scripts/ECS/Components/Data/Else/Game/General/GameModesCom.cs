using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct GameModesCom
    {
        internal static GameModes GameMode;
        internal static bool IsGameMode(GameModes gameMode) => GameMode == gameMode;
        internal static bool IsGameMode(GameModes[] gameModes)
        {
            foreach (var gameMode in gameModes) if (IsGameMode(gameMode)) return true;
            return false;
        }

        internal static bool IsOfflineMode => IsGameMode(new[] { GameModes.TrainingOff, GameModes.WithFriendOff });
        internal static bool IsOnlineMode => IsGameMode(new[] { GameModes.PublicOn, GameModes.WithFriendOn });
    }
}
