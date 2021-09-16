using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct GameModeTypeComp
    {
        internal static GameModeTypes GameModeType;
        internal static bool IsGameModeType(GameModeTypes gameModeType) => GameModeType == gameModeType;
        internal static bool IsGameModeType(GameModeTypes[] gameModeTypes)
        {
            foreach (var gameModeType in gameModeTypes)
            {
                if (gameModeType == GameModeType) return true;
            }
            return false;
        }

        internal GameModeTypeComp(GameModeTypes gameModeType) => GameModeType = gameModeType;
    }
}
