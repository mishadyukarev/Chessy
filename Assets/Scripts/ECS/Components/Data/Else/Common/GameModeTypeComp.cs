using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct GameModeTypeComp
    {
        internal static GameModeTypes GameModeType;
        internal static bool IsGameModeType(GameModeTypes gameModeType) => GameModeType == gameModeType;

        internal GameModeTypeComp(GameModeTypes gameModeType) => GameModeType = gameModeType;
    }
}
