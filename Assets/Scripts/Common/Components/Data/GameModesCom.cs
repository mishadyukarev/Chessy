using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    public struct GameModesCom
    {
        public static GameModes CurGameMode;
        public static bool IsGameMode(GameModes gameMode) => CurGameMode == gameMode;
        public static bool IsGameMode(GameModes[] gameModes)
        {
            foreach (var gameMode in gameModes) if (IsGameMode(gameMode)) return true;
            return false;
        }

        public static bool IsOfflineMode => IsGameMode(new[] { GameModes.TrainingOff, GameModes.WithFriendOff });
        public static bool IsOnlineMode => IsGameMode(new[] { GameModes.PublicOn, GameModes.WithFriendOn });
    }
}
