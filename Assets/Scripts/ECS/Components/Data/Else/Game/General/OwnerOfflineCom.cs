using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct OwnerOfflineCom
    {
        internal PlayerTypes LocalPlayerType;
        internal bool IsMainMaster
        {
            get
            {
                if (Is(PlayerTypes.First)) return true;
                else return false;
            }
            set
            {
                if (value == true) LocalPlayerType = PlayerTypes.First;
                else LocalPlayerType = PlayerTypes.Second;
            }
        }

        internal bool Is(PlayerTypes localPlayerType) => LocalPlayerType == localPlayerType;

        internal bool HaveLocalPlayer => LocalPlayerType != default;
        internal bool IsMine
        {
            get
            {
                if (WhoseMoveCom.IsMainMove)
                {
                    if (Is(PlayerTypes.First)) return true;
                    else return false;
                }
                else
                {
                    if (Is(PlayerTypes.First)) return false;
                    else return true;
                }
            }
        }
    }
}
