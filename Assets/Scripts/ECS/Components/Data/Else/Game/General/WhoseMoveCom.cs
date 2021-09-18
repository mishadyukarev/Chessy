using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct WhoseMoveCom
    {
        internal static bool IsMainMove;
        internal static PlayerTypes PlayerType
        {
            get
            {
                if (IsMainMove) return PlayerTypes.First;
                else return PlayerTypes.Second;
            }
        }

        internal WhoseMoveCom(bool isMainMove) => IsMainMove = isMainMove;
    }
}
